using System.Collections.Generic;
using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Handlers.Users
{
    public class GetUsersByFilterResult
    {
        public IEnumerable<BlogUser> Users { get; set; }
    }
}