using System.Threading.Tasks;
using Blog.Services.RoleUserManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.Role
{
    public class BlogRoleManager : IBlogRoleManager
    {
        private readonly RoleManager<DataAccess.EntityModels.IdentityModels.BlogRole> _roleManager;
        
        public BlogRoleManager(RoleManager<DataAccess.EntityModels.IdentityModels.BlogRole> roleManager)
        {
            _roleManager = roleManager;
        }
        
        public Task<DataAccess.EntityModels.IdentityModels.BlogRole[]> GetAllAsync()
        {
            return _roleManager.Roles.ToArrayAsync();
        }

        public Task<DataAccess.EntityModels.IdentityModels.BlogRole> GetByIdAsync(int id)
        { 
            return _roleManager
                .Roles
                .SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}