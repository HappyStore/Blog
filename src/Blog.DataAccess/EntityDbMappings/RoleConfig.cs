using Blog.DataAccess.EntityModels.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.EntityDbMappings
{
    public class RoleConfig : IEntityTypeConfiguration<BlogRole>
    {
        public void Configure(EntityTypeBuilder<BlogRole> builder)
        {
            builder.HasData(
                new BlogRole {Id = 1, Name = "admin"},
                new BlogRole {Id = 2, Name = "moderator"},
                new BlogRole {Id = 3, Name = "user"}
            );
        }
    }
}