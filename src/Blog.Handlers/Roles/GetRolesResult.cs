using System.Collections.Generic;
using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Handlers.Roles
{
    public class GetRolesResult
    {
        public IEnumerable<Role> Roles { get; set; }
    }
}