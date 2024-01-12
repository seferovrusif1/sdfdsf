using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.BlogDtos;
using Twitter.Business.Dtos.CommmentDtos;

namespace Twitter.Business.Services.Interfaces
{
    public interface ICommentService
    {
        public IEnumerable<CommentListItemDto> GetAll();
        //public Task Create();
        //public Task Delete(int id);
        //public Task SoftDelete(int id);
        //public Task ReverseSoftDelete(int id);
    }
}
