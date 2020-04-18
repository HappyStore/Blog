using MediatR;

namespace Blog.Handlers.Roles
{
    public class GetRoleByIdRequest : IRequest<GetRoleByIdResult>
    {
        public int RoleId { get; set; }
    }
}