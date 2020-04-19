using MediatR;

namespace Blog.Handlers.Users
{
    public class GetUserByIdRequest : IRequest<GetUserByIdResult>
    {
        public int UserId { get; set; }
    }
}