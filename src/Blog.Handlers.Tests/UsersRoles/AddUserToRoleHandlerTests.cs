using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using Blog.Handlers.Tests.Helpers;
using Blog.Handlers.Users;
using Blog.Handlers.UsersRoles;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Blog.Handlers.Tests.Users
{
    public class AddUserToRoleHandlerTests
    {
        public AddUserToRoleHandlerTests()
        {
            _userManagerMockWrapper = new UserManagerMockWrapper();
            _targetHandler = new AddUserToRoleHandler(_userManagerMockWrapper.UserManager.Object);
        }

        private readonly AddUserToRoleHandler _targetHandler;
        private readonly UserManagerMockWrapper _userManagerMockWrapper;

        private Mock<UserManager<BlogUser>> UserManagerMock => _userManagerMockWrapper.UserManager;

        [Fact]
        public async Task Handle_ifAddToRoleIsNotSucceeded_returnsAddToRoleFailed()
        {
            var failedIdentityResult = IdentityResult.Failed(new IdentityError {Code = "1", Description = "failed"});

            UserManagerMock
                .Setup(f => f.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new BlogUser {Id = 1});

            UserManagerMock
                .Setup(f => f.AddToRoleAsync(It.IsAny<BlogUser>(), It.IsAny<string>()))
                .ReturnsAsync(failedIdentityResult);

            var request = new AddUserToRoleRequest
            {
                UserId = 1,
                RoleName = "anyRoleName"
            };

            var result = await _targetHandler.Handle(request, CancellationToken.None);

            Assert.Equal(AddUserToRoleStatus.AddToRoleFailed, result.Status);
        }

        [Fact]
        public async Task Handle_ifUserDoesntExist_returnsUserNotFound()
        {
            UserManagerMock
                .Setup(f => f.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<BlogUser>(null));

            var request = new AddUserToRoleRequest
            {
                UserId = 1
            };

            var result = await _targetHandler.Handle(request, CancellationToken.None);

            Assert.Equal(AddUserToRoleStatus.UserNotFound, result.Status);
        }
    }
}