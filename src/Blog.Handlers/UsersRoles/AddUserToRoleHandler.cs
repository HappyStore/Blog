using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using Blog.Handlers.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blog.Handlers.UsersRoles
{
    public class AddUserToRoleHandler : IRequestHandler<AddUserToRoleRequest, AddUserToRoleResult>
    {
        private readonly UserManager<BlogUser> _userManager;

        public AddUserToRoleHandler(UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddUserToRoleResult> Handle(AddUserToRoleRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
                return new AddUserToRoleResult
                {
                    Status = AddUserToRoleStatus.UserNotFound,
                    StatusMessage = "User was not found"
                };

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);

            if (!result.Succeeded)
                return new AddUserToRoleResult
                {
                    Status = AddUserToRoleStatus.AddToRoleFailed,
                    StatusMessage = result.ToString()
                };

            return new AddUserToRoleResult
            {
                Status = AddUserToRoleStatus.Success
            };
        }
    }
}