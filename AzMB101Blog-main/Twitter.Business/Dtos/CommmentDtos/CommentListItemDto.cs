using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.AppUserDtos;

namespace Twitter.Business.Dtos.CommmentDtos
{
    public class CommentListItemDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public AppUserListDto AppUser { get; set; }
        public int ParentCommentId { get; set; }
    }
}
