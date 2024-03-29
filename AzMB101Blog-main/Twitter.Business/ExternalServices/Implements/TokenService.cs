﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.AuthDtos;
using Twitter.Business.ExternalServices.Interfaces;
using Twitter.Core.Entities;

namespace Twitter.Business.ExternalServices.Implements;

public class TokenService : ITokenService
{
    public TokenDto CreateToken(TokenParamsDto dto)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, dto.user.UserName));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.user.Id));
        claims.Add(new Claim(ClaimTypes.GivenName, dto.user.FullName));
        claims.Add(new Claim("BirthDay", dto.user.BirthDate.ToString()));
        claims.Add(new Claim(ClaimTypes.Role, dto.role));
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bjkbjkhkbnmbjmjh"));

        SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        DateTime expires = DateTime.UtcNow.AddMinutes(3000);
        JwtSecurityToken jwt = new JwtSecurityToken(
            "https://localhost:7297/",
            "https://localhost:7297/api",
            claims,
            DateTime.UtcNow,
           expires,
            cred);
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwt);
        return new TokenDto 
        {
            Token = token,
            Expires=expires
        };
    }
}
