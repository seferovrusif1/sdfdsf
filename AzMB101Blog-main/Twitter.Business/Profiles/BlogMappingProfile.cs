using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.BlogDtos;
using Twitter.Business.Dtos.TopicDtos;
using Twitter.Core.Entities;

namespace Twitter.Business.Profiles
{
    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap< Blog, BlogCreateDto> ().ReverseMap();
            CreateMap<Blog, BlogListItemDto> ().ReverseMap();
            CreateMap<Blog, BlogDetailDto> ().ReverseMap();
        }
    }
}
