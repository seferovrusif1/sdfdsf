using Twitter.Core.Entities;

namespace Twitter.Business.ExternalServices.Interfaces;
public interface ITokenService
{
    string CreateToken(AppUser user);
}
