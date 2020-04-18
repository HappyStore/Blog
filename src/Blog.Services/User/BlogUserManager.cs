using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Services.User
{
    public class BlogUserManager : IBlogUserManager
    {
        private readonly UserManager<BlogUser> _userManager;
        
        public BlogUserManager(UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IEnumerable<BlogUser>> GetAll(int pageNumber, int pageSize)
        {
            throw new Exception();
        }

        public Task<BlogUser> GetById(int id)
        {
            throw new Exception();
        }

        public Task Create(BlogUser newUser)
        {
            throw new Exception();
        }

        public Task Delete(int id)
        {
            throw new Exception();
        }

        public Task Update(BlogUser user)
        {
            throw new Exception();
        }
    }
}