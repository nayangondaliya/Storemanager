using MediatR;
using Microsoft.AspNetCore.Mvc;
using Raditap.BusinessLogic.ApiDataObjects.Jobs;
using System.Threading.Tasks;

namespace Api.BackOffice.Controllers.V1
{
    public class JobController : BaseController
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddJobRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("search/{jobId}")]
        public async Task<IActionResult> Search([FromRoute] long jobId)
        {
            return Ok(await _mediator.Send(new SearchJobRequest() { JobId = jobId}));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] EditJobRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("list")]
        public async Task<IActionResult> List([FromBody] ListJobRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("remove/{jobId}")]
        public async Task<IActionResult> Remove([FromRoute] long jobId)
        {
            return Ok(await _mediator.Send(new RemoveJobRequest() { JobId = jobId}));
        }

        [HttpGet("print/{jobId}")]
        public async Task<IActionResult> Print([FromRoute] long jobId)
        {
            return Ok(await _mediator.Send(new PrintJobRequest() { JobId = jobId }));
        }

        [HttpGet("manualNumber")]
        public async Task<IActionResult> LastManualNumber()
        {
            return Ok(await _mediator.Send(new LastJobNumberRequest()));
        }
    }
}
