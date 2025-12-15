using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAuditTrail.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string EntityName { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;

        public string Changes { get; set; } = string.Empty;

        public string? UserId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? ApplicationName { get; set; }
    }
}
