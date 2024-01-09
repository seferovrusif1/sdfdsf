using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.Entities
{
    public class Blog: BaseEntity
    {
        public string Description { get; set; }
        public int UpdateCount { get; set; }
        public int UserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

    }
}
