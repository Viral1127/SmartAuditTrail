using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SmartAuditTrail.Models;
using SmartAuditTrail.Options;
using SmartAuditTrail.Providers;

namespace SmartAuditTrail.Interceptors
{
    public class SmartAuditInterceptor : SaveChangesInterceptor
    {
        private readonly IUserProvider? _userProvider;
        private readonly SmartAuditOptions? _options;

        public SmartAuditInterceptor(IUserProvider? userProvider=null, SmartAuditOptions? options=null)
        {
            _userProvider = userProvider;
            _options = options;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context == null) return base.SavingChanges(eventData, result);

            var entries = context.ChangeTracker.Entries()
                .Where(e => e.Entity.GetType().GetCustomAttributes(typeof(Attributes.AuditableAttribute), true).Any())
                .ToList();

            foreach (var entry in entries)
            {
                ProcessEntry(entry);
            }

            return base.SavingChanges(eventData, result);
        }

        private void ProcessEntry(EntityEntry entry)
        {
            var auditEntry = new AuditEntry
            {
                EntityName = entry.Entity.GetType().Name,
                Action = entry.State.ToString(),
                EntityId = GetPrimaryKey(entry)
            };

            foreach (var prop in entry.Properties)
            {
                if (Attribute.IsDefined(prop.Metadata.PropertyInfo, typeof(Attributes.IgnoreAuditAttribute)))
                    continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                        break;

                    case EntityState.Modified:
                        if (!Equals(prop.OriginalValue, prop.CurrentValue))
                        {
                            auditEntry.OldValues[prop.Metadata.Name] = prop.OriginalValue;
                            auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                        }
                        break;

                    case EntityState.Deleted:
                        auditEntry.OldValues[prop.Metadata.Name] = prop.OriginalValue;
                        break;
                }
            }

            SaveAudit(entry.Context!, auditEntry);
        }

        private void SaveAudit(DbContext context, AuditEntry entry)
        {
            var userId = _userProvider?.GetUserId();
            var auditLog = Helpers.AuditConverter.ToAuditLog(entry, _options, userId);

            context.Set<AuditLog>().Add(auditLog);
        }

        private static string GetPrimaryKey(EntityEntry entry)
        {
            var key = entry.Metadata.FindPrimaryKey();
            if (key == null) return string.Empty;

            var values = key.Properties
                .Select(p => entry.Property(p.Name).CurrentValue)
                .ToArray();

            return string.Join(",", values);
        }


    }
}
