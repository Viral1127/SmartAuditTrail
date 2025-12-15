using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAuditTrail.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IgnoreAuditAttribute:Attribute
    {
    }
}
