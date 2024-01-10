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
using Twitter.Core.Enums;

namespace Twitter.Business.Services.Implements
{
    public class UserService : IUserService
    {
        UserManager<AppUser> _usermanager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IMapper _mapper { get; }

        public UserService(UserManager<AppUser> manager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _usermanager = manager;
            _mapper = mapper;
            RoleManager = roleManager;
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
            var roleResult= await _usermanager.AddToRoleAsync(user, nameof(Roles.Member));
            if(!roleResult.Succeeded) 
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    sb.Append(item.Description + " ");
                }
                //TODO: create exception 
                throw new Exception(sb.ToString().TrimEnd() + "!");
            }
        }
    }
}
