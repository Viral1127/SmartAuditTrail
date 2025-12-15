using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAuditTrail.Providers
{
    public interface IUserProvider
    {
        string? GetUserId();
    }
}
