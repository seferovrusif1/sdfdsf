using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.AuthDtos;
using Twitter.Business.Exceptions.Auth;
using Twitter.Business.ExternalServices.Interfaces;
using Twitter.Business.Services.Interfaces;
using Twitter.Core.Entities;

namespace Twitter.Business.Services.Implements
{
    public class AuthService : IAuthService
    {
        UserManager<AppUser> _userManager { get; }
        ITokenService _tokenService { get; }

        public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> Login(LoginDto dto)
        {
            AppUser? user = null;
            if (dto.UsernameOrEmail.Contains("@"))
            {
                user =await _userManager.FindByEmailAsync(dto.UsernameOrEmail);
            }
            else
            {
                user=await _userManager.FindByNameAsync(dto.UsernameOrEmail);
            }
            if (user == null) throw new UsernameOrEmailOrPaswordWrongException();
            var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!result) throw new UsernameOrEmailOrPaswordWrongException();
            var role = (await _userManager.GetRolesAsync(user)).First();
            return _tokenService.CreateToken(new TokenParamsDto
            {
                user = user,
                role = role
            });
        }
    }
}
