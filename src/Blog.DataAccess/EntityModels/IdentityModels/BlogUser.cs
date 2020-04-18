using Microsoft.AspNetCore.Identity;

namespace Blog.DataAccess.EntityModels.IdentityModels
{
    public class BlogUser : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}