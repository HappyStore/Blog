using Blog.DataAccess.EntityModels.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.EntityDbMappings
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            
        }
    }
}