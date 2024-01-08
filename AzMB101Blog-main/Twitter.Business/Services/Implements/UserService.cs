using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.AppUserDtos;
using Twitter.Business.Exceptions.AppUser;
using Twitter.Business.Services.Interfaces;
using Twitter.Core.Entities;

namespace Twitter.Business.Services.Implements
{
    public class UserService : IUserService
    {
        UserManager<AppUser> _usermanager { get; }

        IMapper _mapper { get; }
        public UserService(UserManager<AppUser> manager, IMapper mapper)
        {
            _usermanager = manager;
            _mapper = mapper;
        }


        public async Task  CreateAsync(RegisterDto dto)
        {
            AppUser user= _mapper.Map<AppUser>(dto);
            var result = await _usermanager.CreateAsync(user,dto.Password);
            if (!result.Succeeded) 
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    sb.Append(item.Description + " ");
                }
                throw new AppUserCreatedFailedException(sb.ToString().TrimEnd()+"!");
            }
        }
    }
}
