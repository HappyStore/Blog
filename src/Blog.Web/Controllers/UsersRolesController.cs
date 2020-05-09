using System.Threading.Tasks;
using Blog.Handlers.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/users/{userId:int}/roles")]
    public class UsersRolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UsersRolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> AddUserToRole(
            [FromRoute] int userId,
            [FromBody] string roleName)
        {
            var request = new AddUserToRoleRequest
            {
                UserId = userId,
                RoleName = roleName
            };

            var result = await _mediator.Send(request);

            return this.ProduceEnumResult(
                result.Status,
                (AddUserToRoleStatus.Success, Ok),
                (AddUserToRoleStatus.UserNotFound, () => NotFound(result.StatusMessage)),
                (AddUserToRoleStatus.AddToRoleFailed, () => BadRequest(result.StatusMessage))
            );
        }
    }
}