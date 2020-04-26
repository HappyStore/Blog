using Blog.DataAccess.EntityModels.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Blog.Handlers.Tests.Helpers
{
    public class UserManagerMockWrapper
    {
        public Mock<UserManager<BlogUser>> UserManager { get; set; }
        
        
        
        public UserManagerMockWrapper()
        {
            var userStoreMock = new Mock<IUserStore<BlogUser>>();
            
            UserManager = new Mock<UserManager<BlogUser>>(
                userStoreMock.Object,
                null, 
                null,
                null, 
                null,
                null,
                null,
                null,
                null
            );
        }
    }
}