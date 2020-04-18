using System.Threading.Tasks;
using Blog.Services.RoleUserManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.Role
{
    public class BlogRoleManager : IBlogRoleManager
    {
        private readonly RoleManager<DataAccess.EntityModels.IdentityModels.Role> _roleManager;
        
        public BlogRoleManager(RoleManager<DataAccess.EntityModels.IdentityModels.Role> roleManager)
        {
            _roleManager = roleManager;
        }
        
        public Task<DataAccess.EntityModels.IdentityModels.Role[]> GetAll()
        {
            return _roleManager.Roles.ToArrayAsync();
        }

        public Task<DataAccess.EntityModels.IdentityModels.Role> GetById(int id)
        { 
            return _roleManager
                .Roles
                .SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}