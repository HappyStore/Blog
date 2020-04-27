using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("api/v1/users/{userId:int}/roles")]
    public class UsersRolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UsersRolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> AddUserToRole(int userId)
        {
            return Ok($"Add user to role {userId}");
        }
    }
}