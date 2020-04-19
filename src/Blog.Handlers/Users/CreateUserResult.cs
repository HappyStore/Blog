using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Handlers.Users
{
    public class CreateUserResult
    {
        public BlogUser? CreatedUser { get; set; }

        public string? ErrorMsg { get; set; }
    }
}