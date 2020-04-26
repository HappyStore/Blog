using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using Blog.Handlers.Tests.Helpers;
using Blog.Handlers.Users;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Blog.Handlers.Tests.Users
{
    public class CreateUserHandlerTests
    {
        public CreateUserHandlerTests()
        {
            _userManagerMockWrapper = new UserManagerMockWrapper();
            _targetHandler = new CreateUserHandler(_userManagerMockWrapper.UserManager.Object);
        }

        private readonly CreateUserHandler _targetHandler;
        private readonly UserManagerMockWrapper _userManagerMockWrapper;

        private Mock<UserManager<BlogUser>> UserManagerMock => _userManagerMockWrapper.UserManager;

        [Fact]
        public async Task Handle_ifNotSucceeded_returnsNullUserAndErrorStr()
        {
            var failedIdentityResult = IdentityResult
                .Failed(new IdentityError {Code = "error code", Description = "Error description"});

            UserManagerMock
                .Setup(f => f.CreateAsync(It.IsAny<BlogUser>()))
                .ReturnsAsync(failedIdentityResult);

            var request = new CreateUserRequest
            {
                UserToCreate = new BlogUser
                {
                    Email = "",
                    FullName = "",
                    PasswordHash = "",
                    UserName = ""
                }
            };

            var result = await _targetHandler.Handle(request, CancellationToken.None);

            Assert.Null(result.CreatedUser);
            Assert.NotNull(result.ErrorMsg);
        }
    }
}