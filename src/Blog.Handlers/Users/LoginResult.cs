using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Handlers.Users
{
    public class LoginResult
    {
        public LoginStatus Status { get; set; }

        public string? AccessToken { get; set; }

        public BlogUser? User { get; set; }
    }
}