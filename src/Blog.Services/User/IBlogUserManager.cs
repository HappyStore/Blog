using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Services.User
{
    public interface IBlogUserManager
    {
        Task<IEnumerable<BlogUser>> GetAll(int pageNumber, int pageSize);

        Task<BlogUser> GetById(int id);

        Task Create(BlogUser newUser);

        Task Delete(int id);

        Task Update(BlogUser user);
    }
}