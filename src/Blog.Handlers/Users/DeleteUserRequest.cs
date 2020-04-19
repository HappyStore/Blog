using MediatR;

namespace Blog.Handlers.Users
{
    public class DeleteUserRequest : IRequest<DeleteUserResult>
    {
        public int UserId { get; set; }
    }
}