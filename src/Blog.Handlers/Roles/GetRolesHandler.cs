using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Handlers.Roles
{
    public class GetRolesHandler : IRequestHandler<GetRolesRequest, GetRolesResult>
    {
        private readonly RoleManager<BlogRole> _roleManager;

        public GetRolesHandler(RoleManager<BlogRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GetRolesResult> Handle(GetRolesRequest request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);

            return new GetRolesResult
            {
                Roles = roles
            };
        }
    }
}