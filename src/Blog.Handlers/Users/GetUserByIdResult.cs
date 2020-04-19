using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Handlers.Users
{
    public class GetUserByIdResult
    {
        public BlogUser? User { get; set; }
    }
}