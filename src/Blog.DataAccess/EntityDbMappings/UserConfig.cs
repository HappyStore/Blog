using Blog.DataAccess.EntityModels.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.EntityDbMappings
{
    public class UserConfig : IEntityTypeConfiguration<BlogUser>
    {
        public void Configure(EntityTypeBuilder<BlogUser> builder)
        {
        }
    }
}