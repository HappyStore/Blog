using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.Roles
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdRequest, GetRoleByIdResult>
    {
        private readonly RoleManager<Role> _roleManager;

        public GetRoleByIdHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        
        public async Task<GetRoleByIdResult> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
        {
            var role = await _roleManager
                .Roles
                .SingleOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken: cancellationToken);
            
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