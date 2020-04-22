using MediatR;

namespace Blog.Handlers.Users
{
    public class ChangePasswordRequest : IRequest<ChangePasswordResult>
    {
        public int UserId { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}