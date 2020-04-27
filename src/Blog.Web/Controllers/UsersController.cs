using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using Blog.Handlers.Users;
using Blog.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BlogUser>> GetUserById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var request = new GetUserByIdRequest
            {
                UserId = id
            };

            var result = await _mediator.Send(request, cancellationToken);

            if (result.User == null) return NotFound();

            return Ok(result.User);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BlogUser>>> GetUsers(
            [FromQuery] string userNameFilter = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1,
            CancellationToken cancellationToken = default)
        {
            var request = new GetUsersByFilterRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserNameFilter = userNameFilter
            };

            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result.Users);
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(
            [FromRoute] int id,
            [FromBody] BlogUser user,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateUserRequest
            {
                UserToUpdate = user
            };

            var result = await _mediator.Send(request, cancellationToken);

            if (result.ErrorMsg != null) return BadRequest(result.ErrorMsg);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BlogUser>> CreateUser([FromBody] BlogUser user)
        {
            var request = new CreateUserRequest
            {
                UserToCreate = user
            };

            var result = await _mediator.Send(request);

            if (result.ErrorMsg != null || result.CreatedUser == null) return BadRequest(result.ErrorMsg);

            return CreatedAtAction(
                nameof(GetUserById),
                new {id = result.CreatedUser.Id},
                result.CreatedUser
            );
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var request = new DeleteUserRequest
            {
                UserId = id
            };

            var result = await _mediator.Send(request, cancellationToken);

            return this.ProduceEnumResult(
                result.Status,
                (DeleteUserStatus.Success, Ok),
                (DeleteUserStatus.UserNotFound, () => NotFound(result.StatusMessage)),
                (DeleteUserStatus.DeleteUserFailed, () => BadRequest(result.StatusMessage))
            );
        }

        [HttpPost("id:int")]
        [Route("password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword(
            [FromRoute] int id,
            [FromBody] ChangePasswordPayload payload,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ChangePasswordRequest
            {
                UserId = id,
                CurrentPassword = payload.CurrentPassword,
                NewPassword = payload.NewPassword
            };

            var result = await _mediator.Send(request, cancellationToken);

            return this.ProduceEnumResult(
                result.Status,
                (ChangePasswordStatus.Success, Ok),
                (ChangePasswordStatus.PasswordChangeFailed, () => BadRequest(result.StatusMessage)),
                (ChangePasswordStatus.UserNotFound, () => NotFound(result.StatusMessage))
            );
        }
    }
}