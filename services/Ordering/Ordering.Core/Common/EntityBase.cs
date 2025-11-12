using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Common
{
    public abstract class EntityBase
    {
        public int Id { get;protected set; }
        public string? CreateBy { get;set; }
        public DateTime? CreateDate { get;set; }
        public string? UpdateBy { get;set; }
        public DateTime? UpdateDate { get;set; }

    }
}
