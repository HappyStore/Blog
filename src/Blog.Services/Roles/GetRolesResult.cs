using System.Collections;
using System.Collections.Generic;
using Blog.DataAccess.EntityModels.IdentityModels;

namespace Blog.Services.Roles
{
    public class GetRolesResult
    {
        public IEnumerable<Role> Roles { get; set; }
    }
}