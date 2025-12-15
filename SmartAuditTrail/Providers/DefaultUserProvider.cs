using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAuditTrail.Providers
{
    public class DefaultUserProvider : IUserProvider
    {
        public string? GetUserId()
        {
            return "System";
        }
    }
}
