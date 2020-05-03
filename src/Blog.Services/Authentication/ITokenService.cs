using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Services.Authentication
{
    public interface ITokenService
    {
        Task<string> GenerateUserTokenAsync(BlogUser user);

        Task<IEnumerable<Claim>> GetBasicClaimsAsync(BlogUser user);

        Task<IEnumerable<Claim>> GetRoleClaimsAsync(BlogUser user);
    }
}