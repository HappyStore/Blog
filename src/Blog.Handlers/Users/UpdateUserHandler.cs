using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blog.Handlers.Users
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResult>
    {
        private readonly UserManager<BlogUser> _userManager;

        public UpdateUserHandler(UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UpdateUserResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userManager.UpdateAsync(request.UserToUpdate);

            if (!result.Succeeded)
            {
                return new UpdateUserResult
                {
                    ErrorMsg = result.ToString()
                };
            }

            return new UpdateUserResult
            {
                ErrorMsg = null
            };
        }
    }
}