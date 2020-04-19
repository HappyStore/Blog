using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blog.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResult>
    {
        private readonly UserManager<BlogUser> _userManager;

        public CreateUserHandler(UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(request.UserToCreate);

            if (!result.Succeeded)
            {
                return new CreateUserResult
                {
                    CreatedUser = null,
                    ErrorMsg = result.ToString()
                };
            }

            var createdUser = await _userManager.FindByNameAsync(request.UserToCreate.UserName);

            return new CreateUserResult
            {
                CreatedUser = createdUser,
                ErrorMsg = null
            };
        }
    }
}