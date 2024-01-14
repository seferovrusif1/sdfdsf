using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.BlogDtos;
using Twitter.Business.Dtos.CommmentDtos;
using Twitter.Business.Dtos.TopicDtos;

namespace Twitter.Business.Services.Interfaces
{
    public interface ICommentService
    {
        public IEnumerable<CommentListItemDto> GetAll();
        public Task<CommentListItemDto> GetByIdAsync(int id);
        public Task CreateAsync(CommentCreateDto dto);
        public Task RemoveAsync(int id);
        public Task SoftRemoveAsync(int id);
        public Task ReverseSoftDelete(int id);
    }
}
