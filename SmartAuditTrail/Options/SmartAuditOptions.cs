using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAuditTrail.Options
{
    public class SmartAuditOptions
    {
        /// <summary>
        /// Provides current user id (JWT, Cookie, Custom)
        /// </summary>
        public Func<string?>? UserIdProvider { get; set; }

        /// <summary>
        /// Optional application name for audit records
        /// </summary>
        public string? ApplicationName { get; set; }
    }
}
