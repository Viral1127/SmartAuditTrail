using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAuditTrail.Attributes;

namespace SmartAuditTrailDemo
{
    [Auditable]
    public class DemoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
