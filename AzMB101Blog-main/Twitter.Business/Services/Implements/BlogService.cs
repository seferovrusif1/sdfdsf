using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.BlogDtos;
using Twitter.Business.Dtos.TopicDtos;
using Twitter.Business.Exceptions.Common;
using Twitter.Business.Repositories.Interfaces;
using Twitter.Business.Services.Interfaces;
using Twitter.Core.Entities;

namespace Twitter.Business.Services.Implements
{
    public class BlogService : IBlogService
    {
        UserManager<AppUser> _userManager { get; }
        IBlogRepository _repo { get; }
        IMapper _mapper { get; }
        IHttpContextAccessor _contextAccessor { get; }
        readonly string? _userId;

        public BlogService(IBlogRepository repo,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            UserManager<AppUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userId = _contextAccessor.HttpContext?.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            _userManager = userManager;
        }

        public async Task CreateAsync(BlogCreateDto dto)
        {
            //sadece test ucun yazdm
            if (await _repo.IsExistAsync(r => r.Description.ToLower() == dto.Description.ToLower()))
                throw new ExistException<Blog>();
            Blog blog = new Blog
            {
                Description = dto.Description,
                UserId = _userId
            };
            await _repo.CreateAsync(blog);
            await _repo.SaveAsync();
        }

      
        public async Task RemoveAsync(int id)
        {
            var data = await _checkId(id);
            _repo.Remove(data);
            await _repo.SaveAsync();
        }

        public IEnumerable<BlogListItemDto> GetAll()
             => _mapper.Map<IEnumerable<BlogListItemDto>>(_repo.GetAll().Where(x => x.IsDeleted == false));

        public async Task<BlogListItemDto> GetByIdAsync(int id)
        {
            var data = await _checkId(id, true);
            if(data.IsDeleted==true) throw new NotFoundException<Blog>();
            return _mapper.Map<BlogListItemDto>(data);
            
        }

        public async Task UpdateAsync(int id, BlogCreateDto dto)
        {
            var data = await _checkId(id);
            if (data.IsDeleted == true) throw new NotFoundException<Blog>();
            if (dto.Description.ToLower() != data.Description.ToLower())
            {
                if (await _repo.IsExistAsync(r => r.Description.ToLower() == dto.Description.ToLower()))
                    throw new ExistException<Blog>();
                data = _mapper.Map(dto, data);
                await _repo.SaveAsync();
            }
        }
        public async Task SoftRemoveAsync(int id)
        {
            var data = await _checkId(id);
            data.IsDeleted = true;
            await _repo.SaveAsync();
        }
        async Task<Blog> _checkId(int id, bool isTrack = false)
        {
            if (id <= 0) throw new ArgumentException();
            var data = await _repo.GetByIdAsync(id, isTrack);
            if (data == null) throw new NotFoundException<Blog>();
            return data;
        }

    }
}
