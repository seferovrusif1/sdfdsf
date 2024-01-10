using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        IBlogRepository _repo { get; }
        IMapper _mapper { get; }


        public BlogService(IBlogRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateAsync(BlogCreateDto dto)
        {
            //sadece test ucun yazdm
            if (await _repo.IsExistAsync(r => r.Description.ToLower() == dto.Description.ToLower()))
                throw new ExistException<Blog>();
            await _repo.CreateAsync(_mapper.Map<Blog>(dto));
            await _repo.SaveAsync();
        }

      
        public async Task RemoveAsync(int id)
        {
            var data = await _checkId(id);
            _repo.Remove(data);
            await _repo.SaveAsync();
        }

        public IEnumerable<BlogCreateDto> GetAll()
             => _mapper.Map<IEnumerable<BlogCreateDto>>(_repo.GetAll());

        public async Task<BlogCreateDto> GetByIdAsync(int id)
        {
            var data = await _checkId(id, true);
            return _mapper.Map<BlogCreateDto>(data);
        }

        public async Task UpdateAsync(int id, BlogCreateDto dto)
        {
            var data = await _checkId(id);
            if (dto.Description.ToLower() != data.Description.ToLower())
            {
                if (await _repo.IsExistAsync(r => r.Description.ToLower() == dto.Description.ToLower()))
                    throw new ExistException<Blog>();
                data = _mapper.Map(dto, data);
                await _repo.SaveAsync();
            }
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
