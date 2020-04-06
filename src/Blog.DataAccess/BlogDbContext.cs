using Blog.DataAccess.EntityModels.IdentityModels;
using Blog.DataAccess.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess {
    class BlogDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>> {
        
        public BlogDbContext(DbContextOptions options)
            : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyAllConfigurations();
        }
    }
}