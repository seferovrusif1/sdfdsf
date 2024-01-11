using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName{ get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<Blog> Blogs {  get; set; }
    }
}
