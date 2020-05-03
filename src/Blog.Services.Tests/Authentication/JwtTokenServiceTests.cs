using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using Blog.Services.Authentication;
using Blog.TestHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace Blog.Services.Tests.Authentication
{
    public class JwtTokenServiceTests
    {
        public JwtTokenServiceTests()
        {
            _userManagerMockWrapper = new UserManagerMockWrapper();
            
            _tokenOptions = new JwtTokenOptions
            {
                Issuer = "testIssuer",
                SigningSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes("a random, long, sequence of characters that only the server knows")),
                ExpiresInMinutes = 10,
                RefreshExpiresInMinutes = 5
            };

            var optionsMock = new Mock<IOptions<JwtTokenOptions>>();
            optionsMock
                .SetupGet(g => g.Value)
                .Returns(_tokenOptions);

            _tokenService = new TokenService(
                _userManagerMockWrapper.UserManagerMock.Object, optionsMock.Object
            );
        }
        
        private readonly UserManagerMockWrapper _userManagerMockWrapper;
        private readonly TokenService _tokenService;
        private readonly JwtTokenOptions _tokenOptions;
        
        private Mock<UserManager<BlogUser>> UserManagerMock => _userManagerMockWrapper.UserManagerMock;

        [Fact]
        public async Task GetRoleClaimsAsync_whenNoRoles_returnsEmptyArr()
        {
            UserManagerMock
                .Setup(f => f.GetRolesAsync(It.IsAny<BlogUser>()))
                .ReturnsAsync((IList<string>) null);

            var claims = await _tokenService.GetRoleClaimsAsync(new BlogUser());
            
            Assert.NotNull(claims);
            Assert.Empty(claims);
        }
        
        [Fact]
        public async Task GetBasicClaimsAsync_returnsValidClaims()
        {
            var user = new BlogUser
            {
                Id = 1,
                UserName = "username"
            };

            var claims = await _tokenService.GetBasicClaimsAsync(user);
            
            Assert.NotNull(claims);
            Assert.NotEmpty(claims);
            Assert.Collection(claims, 
                claim => Assert.Equal(user.UserName, claim.Value),
                claim => Assert.Equal(user.Id.ToString(), claim.Value)
            );
        }

        [Fact]
        public async Task GenerateUserTokenAsync_returnsValidToken()
        {
            var userRoles = new List<string>
            {
                "admin"
            };
            
            var user = new BlogUser
            {
                Id = 1,
                UserName = "username"
            };

            UserManagerMock
                .Setup(f => f.GetRolesAsync(It.IsAny<BlogUser>()))
                .ReturnsAsync(userRoles);

            var tokenEncrypted = await _tokenService.GenerateUserTokenAsync(user);
            
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.ReadToken(tokenEncrypted) as JwtSecurityToken;

            Assert.True(jwtHandler.CanReadToken(tokenEncrypted));
            Assert.NotEmpty(token.Claims);
        }
    }
}