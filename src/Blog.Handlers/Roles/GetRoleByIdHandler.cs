using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Handlers.Roles
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdRequest, GetRoleByIdResult>
    {
        private readonly RoleManager<BlogRole> _roleManager;

        public GetRoleByIdHandler(RoleManager<BlogRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GetRoleByIdResult> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);

            if (role == null)
            {
                return new GetRoleByIdResult
                {
                    BlogRole = null
                };
            }

            return new GetRoleByIdResult
            {
                BlogRole = role
            };
        }
    }
}