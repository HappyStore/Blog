using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blog.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResult>
    {
        private readonly UserManager<BlogUser> _userManager;

        public DeleteUserHandler(UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DeleteUserResult> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userManager.DeleteAsync(new BlogUser {Id = request.UserId});

            if (!result.Succeeded)
            {
                return new DeleteUserResult
                {
                    ErrorMsg = result.ToString()
                };
            }

            return new DeleteUserResult
            {
                ErrorMsg = null
            };
        }
    }
}