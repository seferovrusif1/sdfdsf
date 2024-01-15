using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.BlogDtos;
using Twitter.Business.Dtos.CommmentDtos;
using Twitter.Business.Dtos.TopicDtos;
using Twitter.Business.Exceptions.Common;
using Twitter.Business.Repositories.Interfaces;
using Twitter.Business.Services.Interfaces;

namespace Twitter.Business.Services.Implements
{
    public class CommentService:ICommentService
    {
        UserManager<AppUser> _userManager { get; }
        ICommentRepository _repo {  get; set; }
        IHttpContextAccessor _contextAccessor { get; }
        readonly string userId;
        IMapper _mapper { get; set; }

        public CommentService(IMapper mapper, ICommentRepository repo, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _repo = repo;
            _contextAccessor = contextAccessor;
            if (_contextAccessor.HttpContext.User.Claims.Any())
            {
                userId = _contextAccessor.HttpContext?.User?.Claims?.First(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException();
            }
        }

        public IEnumerable<CommentListItemDto> GetAll() 
             => _mapper.Map<IEnumerable<CommentListItemDto>>(_repo.GetAll().Where(x => x.IsDeleted == false));

        public async Task CreateAsync(CommentCreateDto dto)
        {
            var entity = _mapper.Map<Comment>(dto);
            entity.AppUserId= userId;
            await _repo.CreateAsync(entity);
            await _repo.SaveAsync();
        }

        public async Task<CommentListItemDto> GetByIdAsync(int id)
        {
            var data = await _checkId(id, true);
            return _mapper.Map<CommentListItemDto>(data);
        }

        public async Task RemoveAsync(int id)
        {
            var data = await _checkId(id);
            _repo.Remove(data);
            await _repo.SaveAsync();
        }
        public async Task ReverseSoftDelete(int id)
        {
            var data = await _checkId(id);
            data.IsDeleted = false;
            await _repo.SaveAsync();
        }
        public async Task SoftRemoveAsync(int id)
        {
            var data = await _checkId(id);
            data.IsDeleted = true;
            await _repo.SaveAsync();
        }

        async Task<Comment> _checkId(int id, bool isTrack = false)
        {
            if (id <= 0) throw new ArgumentException();
            var data = await _repo.GetByIdAsync(id, isTrack);
            if (data == null) throw new NotFoundException<Comment>();
            return data;
        }

    }
}
