using MediatR;

namespace Blog.Handlers.Users
{
    public class LoginRequest : IRequest<LoginResult>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}