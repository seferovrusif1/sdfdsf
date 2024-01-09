using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.ExternalServices.Interfaces;
using Twitter.Core.Entities;

namespace Twitter.Business.ExternalServices.Implements;

public class TokenService : ITokenService
{
    public string CreateToken(AppUser user)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));
        claims.Add(new Claim("BirthDay", user.BirthDate.ToString()));
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bjkbjkhkbnmbjmjh"));

        SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken jwt = new JwtSecurityToken(
            "https://localhost:7297/",
            "https://localhost:7297/api",
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(30),
            cred);
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwt);
        return token;
    }
}
