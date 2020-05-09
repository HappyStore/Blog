using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Web.Contracts.Users
{
    public class LoginResultDto
    {
        public BlogUser User { get; set; }

        public string AccessToken { get; set; }
    }
}