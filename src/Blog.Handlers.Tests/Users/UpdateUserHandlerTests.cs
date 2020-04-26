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
    public class UpdateUserHandlerTests
    {
        public UpdateUserHandlerTests()
        {
            _userManagerMockWrapper = new UserManagerMockWrapper();
            _targetHandler = new UpdateUserHandler(_userManagerMockWrapper.UserManager.Object);
        }

        private readonly UpdateUserHandler _targetHandler;
        private readonly UserManagerMockWrapper _userManagerMockWrapper;

        private Mock<UserManager<BlogUser>> UserManagerMock => _userManagerMockWrapper.UserManager;

        [Fact]
        public async Task Handle_ifUpdateIsNotSucceed_returnsErrorMsg()
        {
            var failedIdentityResult = IdentityResult
                .Failed(new IdentityError {Code = "error code", Description = "Error description"});

            UserManagerMock
                .Setup(f => f.UpdateAsync(It.IsAny<BlogUser>()))
                .ReturnsAsync(failedIdentityResult);

            var request = new UpdateUserRequest
            {
                UserToUpdate = new BlogUser
                {
                    Id = 1,
                    Email = "",
                    FullName = "",
                    PasswordHash = "",
                    UserName = ""
                }
            };

            var result = await _targetHandler.Handle(request, CancellationToken.None);

            Assert.NotNull(result.ErrorMsg);
        }
    }
}