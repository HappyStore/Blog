using MediatR;

namespace Blog.Handlers.Users
{
    public class AddUserToRoleRequest : IRequest<AddUserToRoleResult>
    {
        public int UserId { get; set; }

        public string RoleName { get; set; }
    }
}