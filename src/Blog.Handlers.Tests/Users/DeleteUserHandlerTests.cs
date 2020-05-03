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
    public class DeleteUserHandlerTests
    {
        public DeleteUserHandlerTests()
        {
            _userManagerMockWrapper = new UserManagerMockWrapper();
            _targetHandler = new DeleteUserHandler(_userManagerMockWrapper.UserManagerMock.Object);
        }

        private readonly DeleteUserHandler _targetHandler;
        private readonly UserManagerMockWrapper _userManagerMockWrapper;

        private Mock<UserManager<BlogUser>> UserManagerMock => _userManagerMockWrapper.UserManagerMock;

        [Fact]
        public async Task Handle_ifDeleteIsNotSucceeded_returnsUserFailedResult()
        {
            var failedIdentityResult = IdentityResult.Failed(new IdentityError {Code = "1", Description = "failed"});

            UserManagerMock
                .Setup(f => f.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new BlogUser {Id = 1});

            UserManagerMock
                .Setup(f => f.DeleteAsync(
                    It.IsAny<BlogUser>()))
                .ReturnsAsync(failedIdentityResult);

            var request = new DeleteUserRequest
            {
                UserId = 1
            };

            var result = await _targetHandler.Handle(request, CancellationToken.None);

            Assert.Equal(DeleteUserStatus.DeleteUserFailed, result.Status);
            Assert.NotNull(result.StatusMessage);
        }

        [Fact]
        public async Task Handle_ifUserDoesntExists_returnsUserNotFound()
        {
            UserManagerMock
                .Setup(f => f.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<BlogUser>(null));

            var request = new DeleteUserRequest();

            var result = await _targetHandler.Handle(request, CancellationToken.None);

            Assert.Equal(DeleteUserStatus.UserNotFound, result.Status);
        }
    }
}