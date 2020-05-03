using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using Blog.Handlers.Users;
using Blog.TestHelpers;
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
            _targetHandler = new UpdateUserHandler(_userManagerMockWrapper.UserManagerMock.Object);
        }

        private readonly UpdateUserHandler _targetHandler;
        private readonly UserManagerMockWrapper _userManagerMockWrapper;

        private Mock<UserManager<BlogUser>> UserManagerMock => _userManagerMockWrapper.UserManagerMock;

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