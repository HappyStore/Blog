using Microsoft.AspNetCore.Identity;

namespace Blog.DataAccess.EntityModels.IdentityModels
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual Role Role { get; set; }
        
        public virtual User User { get; set; }
    }
}