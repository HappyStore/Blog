using MediatR;

namespace Blog.Services.Roles
{
    public class GetRoleByIdRequest : IRequest<GetRoleByIdResult>
    {
        public int RoleId { get; set; }
    }
}