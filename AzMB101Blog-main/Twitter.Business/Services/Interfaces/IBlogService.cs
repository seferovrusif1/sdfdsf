using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.BlogDtos;
using Twitter.Business.Dtos.TopicDtos;

namespace Twitter.Business.Services.Interfaces
{
    public interface IBlogService
    {
        public Task CreateAsync(BlogCreateDto dto);
        public Task RemoveAsync(int id);
        public IEnumerable<BlogCreateDto> GetAll();
        public Task<BlogCreateDto> GetByIdAsync(int id);
        public Task UpdateAsync(int id, BlogCreateDto dto);
    }
}
