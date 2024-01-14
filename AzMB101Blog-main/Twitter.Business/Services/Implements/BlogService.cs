using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Twitter.Business.Dtos.BlogDtos;
using Twitter.Business.Exceptions.Common;
using Twitter.Business.Repositories.Interfaces;
using Twitter.Business.Services.Interfaces;

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
            var entity = _mapper.Map<Blog>(dto);
            entity.UserId = _userId;
            await _repo.CreateAsync(entity);
            await _repo.SaveAsync();
        }

      
        public async Task RemoveAsync(int id)
        {
            var data = await _checkId(id);
            _repo.Remove(data);
            await _repo.SaveAsync();
        }

        public IEnumerable<BlogListItemDto> GetAll()
        {
            return _mapper.Map<IEnumerable<BlogListItemDto>>(_repo.GetAll().Where(x => x.IsDeleted == false));
        }

        public async Task<BlogDetailDto> GetByIdAsync(int id)
        {
            var data = await _checkId(id, true);
            if(data.IsDeleted==true) throw new NotFoundException<Blog>();
            return _mapper.Map<BlogDetailDto>(data);
            
        }

        public async Task UpdateAsync(int id, BlogCreateDto dto)
        {
            var data = await _checkId(id);
            if (data.IsDeleted == true) throw new NotFoundException<Blog>();
            if (data.UserId != _userId) throw new Exception("User has no access");
            data = _mapper.Map(dto, data);
            data.LastUpdateTime = DateTime.UtcNow;
            data.UpdateCount++;
            await _repo.SaveAsync();
            }
        
        public async Task SoftRemoveAsync(int id)
        {
            var data = await _checkId(id);
            data.IsDeleted = true;
            await _repo.SaveAsync();
        }
        public async Task ReverseSoftRemoveAsync(int id)
        {
            var data = await _checkId(id);
            data.IsDeleted = false;
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
