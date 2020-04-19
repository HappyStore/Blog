using Blog.DataAccess.EntityModels.IdentityModels;
using MediatR;

namespace Blog.Handlers.Users
{
    public class CreateUserRequest : IRequest<CreateUserResult>
    {
        public BlogUser UserToCreate { get; set; }
    }
}