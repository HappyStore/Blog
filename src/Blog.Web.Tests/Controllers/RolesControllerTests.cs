using System.Threading.Tasks;
using Blog.Handlers.Roles;
using Blog.Web.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Blog.Web.Tests.Controllers
{
    public class RolesControllerTests
    {
        private readonly IMediator _mediator;
        private readonly RolesController _targetController;
        
        public RolesControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _targetController = new RolesController(_mediator);
        }
        
        [Fact]
        public async Task GetRoleById_WhenRoleIsNull_ReturnsNotFound()
        {
            _mediator
                .Send(Arg.Any<GetRoleByIdRequest>())
                .Returns(new GetRoleByIdResult { BlogRole = null });

            var res = await _targetController.GetRoleById(1);

            Assert.Null(res.Value);
            Assert.IsAssignableFrom<NotFoundResult>(res.Result);
        }
    }
}