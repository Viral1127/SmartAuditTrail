using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SmartAuditTrail.Models;
using SmartAuditTrail.Options;

namespace SmartAuditTrail.Helpers
{
    internal static class AuditConverter
    {
        public static AuditLog ToAuditLog(
            AuditEntry entry,
            SmartAuditOptions? options,
            string? userId)
        {
            var changes = new
            {
                OldValues = entry.OldValues,
                NewValues = entry.NewValues
            };

            return new AuditLog
            {
                EntityName = entry.EntityName,
                EntityId = entry.EntityId,
                Action = entry.Action,
                Changes = JsonSerializer.Serialize(changes),
                UserId = userId,
                ApplicationName = options?.ApplicationName
            };
        }
    }
}
