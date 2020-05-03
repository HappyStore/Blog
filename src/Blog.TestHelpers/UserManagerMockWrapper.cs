using Blog.DataAccess.EntityModels.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Blog.TestHelpers
{
    public class UserManagerMockWrapper
    {
        public UserManagerMockWrapper()
        {
            var userStoreMock = new Mock<IUserStore<BlogUser>>();

            UserManagerMock = new Mock<UserManager<BlogUser>>(
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

        public Mock<UserManager<BlogUser>> UserManagerMock { get; set; }
    }
}