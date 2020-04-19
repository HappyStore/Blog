using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;

namespace Blog.Handlers.Users
{
    public class UpdateUserRequest : IRequest<UpdateUserResult>
    {
        public BlogUser UserToUpdate { get; set; }
    }
}