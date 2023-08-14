using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5Company.CQRS.Queries.PermissionTypeQueries;
using System.Net;

namespace N5Company.Controllers
{
    [Route("api/v1")]
    public class PermissionTypeController : N5ControllerBase
    {
        private readonly ISender _mediator;

        public PermissionTypeController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[Controller]")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ErrorOr.ErrorOr))]
        public async Task<IActionResult> GetPermissionTypes()
        {
            var query = new GetPermissionTypeQuery();
            var result = await _mediator.Send(query);
            return result.Match(resp => StatusCode((int)HttpStatusCode.OK, resp), errors => Problem(errors));
        }
    }
}
