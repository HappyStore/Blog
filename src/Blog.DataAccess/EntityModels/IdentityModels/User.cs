using Microsoft.AspNetCore.Identity;

namespace Blog.DataAccess.EntityModels.IdentityModels
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}
