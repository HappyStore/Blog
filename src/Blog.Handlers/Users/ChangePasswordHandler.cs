using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blog.Handlers.Users
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, ChangePasswordResult>
    {
        private readonly UserManager<BlogUser> _userManager;

        public ChangePasswordHandler(UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ChangePasswordResult> Handle(ChangePasswordRequest request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
                return new ChangePasswordResult
                {
                    Status = ChangePasswordStatus.UserNotFound,
                    StatusMessage = "User was not found"
                };

            var result = await _userManager
                .ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
                return new ChangePasswordResult
                {
                    Status = ChangePasswordStatus.PasswordChangeFailed,
                    StatusMessage = result.ToString()
                };

            return new ChangePasswordResult
            {
                Status = ChangePasswordStatus.Success
            };
        }
    }
}