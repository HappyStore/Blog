﻿using System.Threading;
using System.Threading.Tasks;
using Blog.DataAccess.EntityModels.IdentityModels;
using Blog.Handlers.Roles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class RolesController : ApiV1ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetRolesResult>> GetRoles(CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetRolesRequest(), cancellationToken);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Role>> GetRoleById(
            int id, 
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetRoleByIdRequest { RoleId = id }, cancellationToken);

            return this.OkOrNotFound(result.Role);
        }
    }
}