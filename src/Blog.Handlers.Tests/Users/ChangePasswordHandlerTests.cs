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
    public class ChangePasswordHandlerTests
    {
        public ChangePasswordHandlerTests()
        {
            _userManagerMockWrapper = new UserManagerMockWrapper();
            _targetHandler = new ChangePasswordHandler(_userManagerMockWrapper.UserManagerMock.Object);
        }

        private readonly ChangePasswordHandler _targetHandler;
        private readonly UserManagerMockWrapper _userManagerMockWrapper;

        private Mock<UserManager<BlogUser>> UserManagerMock => _userManagerMockWrapper.UserManagerMock;

        [Fact]
        public async Task Handle_ifChangePasswordIsNotSucceeded_returnsUserFailedResult()
        {
            var failedIdentityResult = IdentityResult.Failed(new IdentityError {Code = "1", Description = "failed"});

            UserManagerMock
                .Setup(f => f.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new BlogUser {Id = 1});

            UserManagerMock
                .Setup(f => f.ChangePasswordAsync(
                    It.IsAny<BlogUser>(),
                    It.IsAny<string>(),
                    It.IsAny<string>())
                )
                .ReturnsAsync(failedIdentityResult);

            var request = new ChangePasswordRequest
            {
                CurrentPassword = "anyString",
                NewPassword = "anyString",
                UserId = 1
            };

            var result = await _targetHandler.Handle(request, CancellationToken.None);

            Assert.Equal(ChangePasswordStatus.PasswordChangeFailed, result.Status);
            Assert.NotNull(result.StatusMessage);
        }

        [Fact]
        public async Task Handle_ifUserDoesntExists_returnsUserNotFound()
        {
            UserManagerMock
                .Setup(f => f.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<BlogUser>(null));

            var request = new ChangePasswordRequest();

            var result = await _targetHandler.Handle(request, CancellationToken.None);

            Assert.Equal(ChangePasswordStatus.UserNotFound, result.Status);
        }
    }
}