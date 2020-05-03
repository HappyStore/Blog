using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<BlogUser> _userManager;
        private readonly JwtTokenOptions _jwtTokenOptions;
        
        public TokenService(UserManager<BlogUser> userManager, 
            IOptions<JwtTokenOptions> jwtTokenOptions)
        {
            _userManager = userManager;
            _jwtTokenOptions = jwtTokenOptions.Value;
        }

        public async Task<string> GenerateUserTokenAsync(BlogUser user)
        {
            var basicClaims = await GetBasicClaimsAsync(user);
            var roleClaims = await GetRoleClaimsAsync(user);
            var claims = basicClaims.Union(roleClaims);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtTokenOptions.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(_jwtTokenOptions.ExpiresInMinutes),
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Convert.FromBase64String(_jwtTokenOptions.SigningSecret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);
        }

        public Task<IEnumerable<Claim>> GetBasicClaimsAsync(BlogUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            
            return Task.FromResult<IEnumerable<Claim>>(claims);
        }

        public async Task<IEnumerable<Claim>> GetRoleClaimsAsync(BlogUser user)
        { 
            return (await _userManager.GetRolesAsync(user))
                .Select(role => new Claim(ClaimTypes.Role, role)) ?? new Claim[0];
        }
    }
}