using Data.Entities.Identity;

namespace WebAuction.Abstract
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
