using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5Company.CQRS.Commands.PermissionCommands;
using N5Company.CQRS.Queries.PermissionQueries;
using System.Net;

namespace N5Company.Controllers
{
    [Route("api/v1")]
    public class PermissionController : N5ControllerBase
    {
        private readonly ISender _mediator;

        public PermissionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[Controller]")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ErrorOr.ErrorOr))]
        public async Task<IActionResult> GetPermissions()
        {
            var query = new GetPermissionQuery();
            var result = await _mediator.Send(query);
            return result.Match(resp => StatusCode((int)HttpStatusCode.OK, resp), errors => Problem(errors));
        }

        [HttpPost]
        [Route("[Controller]")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ErrorOr.ErrorOr))]
        public async Task<IActionResult> RequestPermissions([FromBody] RequestPermissionCommand request)
        {
            var result = await _mediator.Send(request);
            return result.Match(resp => StatusCode((int)HttpStatusCode.OK, resp),
                errors => Problem(errors));
        }

        [HttpPut]
        [Route("[Controller]")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ErrorOr.ErrorOr))]
        public async Task<IActionResult> ModifyPermission([FromBody] ModifyPermissionCommand request)
        {
            var result = await _mediator.Send(request);
            return result.Match(resp => StatusCode((int)HttpStatusCode.OK, resp),
                errors => Problem(errors));
        }
    }
}
