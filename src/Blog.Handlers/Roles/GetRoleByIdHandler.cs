using System.Threading;
using System.Threading.Tasks;
using Blog.Services.RoleUserManager;
using MediatR;

namespace Blog.Handlers.Roles
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdRequest, GetRoleByIdResult>
    {
        private readonly IBlogRoleManager _roleManager;

        public GetRoleByIdHandler(IBlogRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GetRoleByIdResult> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.GetById(request.RoleId);

            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                return new GetRoleByIdResult
                {
                    Role = null
                };
            }

            return new GetRoleByIdResult
            {
                Role = role
            };
        }
    }
}