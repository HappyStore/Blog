using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using Blog.Services.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blog.Handlers.Users
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResult>
    {
        private readonly UserManager<BlogUser> _userManager;
        private readonly SignInManager<BlogUser> _signInManager;
        private readonly ITokenService _tokenService;
        
        public LoginHandler(
            ITokenService tokenService, 
            UserManager<BlogUser> userManager,
            SignInManager<BlogUser> signInManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<LoginResult> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return new LoginResult
                {
                    Status = LoginStatus.UserNotFound
                };
            }

            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(user, request.Password, false);

            if (!signInResult.Succeeded)
            {
                return new LoginResult
                {
                    Status = LoginStatus.SignInFailed
                };
            }

            var accessToken = await _tokenService.GenerateUserTokenAsync(user);
            
            return new LoginResult
            {
                Status = LoginStatus.Success,
                AccessToken = accessToken,
                User = user
            };
        }
    }
}