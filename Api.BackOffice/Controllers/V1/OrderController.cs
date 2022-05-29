using MediatR;
using Microsoft.AspNetCore.Mvc;
using Raditap.BusinessLogic.ApiDataObjects.Orders;
using System.Threading.Tasks;

namespace Api.BackOffice.Controllers.V1
{
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddOrderRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("search/{orderId}")]
        public async Task<IActionResult> Search([FromRoute] long orderId)
        {
            return Ok(await _mediator.Send(new SearchOrderRequest() { OrderId = orderId}));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] EditOrderRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("list")]
        public async Task<IActionResult> List([FromBody] ListOrderRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("remove/{orderId}")]
        public async Task<IActionResult> Remove([FromRoute] long orderId)
        {
            return Ok(await _mediator.Send(new RemoveOrderRequest() { OrderId = orderId}));
        }

        [HttpGet("manualNumber")]
        public async Task<IActionResult> LastManualNumber()
        {
            return Ok(await _mediator.Send(new LastOrderNumberRequest()));
        }
    }
}
