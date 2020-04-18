using System.Threading.Tasks;

namespace Blog.Services.RoleUserManager
{
    public interface IBlogRoleManager
    {
        Task<DataAccess.EntityModels.IdentityModels.Role[]> GetAll();
        
        Task<DataAccess.EntityModels.IdentityModels.Role> GetById(int id);
    }
}