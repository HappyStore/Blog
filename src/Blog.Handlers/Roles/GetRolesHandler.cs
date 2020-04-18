using System.Threading;
using System.Threading.Tasks;
using Blog.Services.RoleUserManager;
using MediatR;

namespace Blog.Handlers.Roles
{
    public class GetRolesHandler : IRequestHandler<GetRolesRequest, GetRolesResult>
    {
        private readonly IBlogRoleManager _roleManager;

        public GetRolesHandler(IBlogRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GetRolesResult> Handle(GetRolesRequest request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.GetAllAsync();

            cancellationToken.ThrowIfCancellationRequested();

            return new GetRolesResult
            {
                Roles = roles
            };
        }
    }
}