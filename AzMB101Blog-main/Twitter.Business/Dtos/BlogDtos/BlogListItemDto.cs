using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Entities;

namespace Twitter.Business.Dtos.BlogDtos
{
    public class BlogListItemDto
    {
        public string Description { get; set; }
        public AppUser User { get; set; }
      

    }
   
}
