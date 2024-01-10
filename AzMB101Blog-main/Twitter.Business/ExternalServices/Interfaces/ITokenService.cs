using Twitter.Business.Dtos.AuthDtos;
using Twitter.Core.Entities;

namespace Twitter.Business.ExternalServices.Interfaces;
public interface ITokenService
{
    string CreateToken(TokenParamsDto dto);
}
