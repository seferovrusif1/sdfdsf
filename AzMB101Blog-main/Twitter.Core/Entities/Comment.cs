using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.Entities
{
    public class Comment:BaseEntity
    {
        public string  Content { get; set; }
        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
        public int? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }    
        public ICollection<Comment>? ChildComments { get; set;}
    }
}
