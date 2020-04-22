using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Handlers.Users
{
    public class GetUsersByFilterHandler : IRequestHandler<GetUsersByFilterRequest, GetUsersByFilterResult>
    {
        private readonly UserManager<BlogUser> _userManager;

        public GetUsersByFilterHandler(UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUsersByFilterResult> Handle(GetUsersByFilterRequest request,
            CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;

            var users = await _userManager.Users
                .Where(u => u.UserName.Contains(request.UserNameFilter))
                .Skip(skip)
                .Take(request.PageNumber)
                .ToArrayAsync(cancellationToken);

            return new GetUsersByFilterResult {Users = users};
        }
    }
}