using System.Threading;
using System.Threading.Tasks;
using Blog.Handlers.Roles;
using Blog.Web.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Blog.Web.Tests.Controllers
{
    public class RolesControllerTests
    {
        public RolesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _targetController = new RolesController(_mediatorMock.Object);
        }

        private readonly Mock<IMediator> _mediatorMock;
        private readonly RolesController _targetController;

        [Fact]
        public async Task GetRoleById_WhenRoleIsNull_ReturnsNotFound()
        {
            _mediatorMock
                .Setup(f => f.Send(It.IsAny<GetRoleByIdRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetRoleByIdResult {BlogRole = null});

            var res = await _targetController.GetRoleById(1);

            Assert.Null(res.Value);
            Assert.IsAssignableFrom<NotFoundResult>(res.Result);
        }
    }
}