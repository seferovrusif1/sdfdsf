﻿using Twitter.Business.Dtos.BlogDtos;

namespace Twitter.Business.Services.Interfaces
{
    public interface IBlogService
    {
        public Task CreateAsync(BlogCreateDto dto);
        public Task RemoveAsync(int id);
        public Task SoftRemoveAsync(int id);
        public IEnumerable<BlogListItemDto> GetAll();
        public Task<BlogListItemDto> GetByIdAsync(int id);
        public Task UpdateAsync(int id, BlogCreateDto dto);
    }
}
