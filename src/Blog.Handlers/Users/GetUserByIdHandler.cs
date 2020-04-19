using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Handlers.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResult>
    {
        private readonly UserManager<BlogUser> _userManager;

        public GetUserByIdHandler(UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUserByIdResult> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            return new GetUserByIdResult
            {
                User = user
            };
        }
    }
}