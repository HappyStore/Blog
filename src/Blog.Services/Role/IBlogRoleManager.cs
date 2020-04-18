using System.Threading.Tasks;

namespace Blog.Services.RoleUserManager
{
    public interface IBlogRoleManager
    {
        Task<DataAccess.EntityModels.IdentityModels.BlogRole[]> GetAllAsync();
        
        Task<DataAccess.EntityModels.IdentityModels.BlogRole> GetByIdAsync(int id);
    }
}