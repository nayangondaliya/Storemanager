using MediatR;
using Microsoft.AspNetCore.Mvc;
using Raditap.BusinessLogic.ApiDataObjects.Users;
using System.Threading.Tasks;

namespace Api.BackOffice.Controllers.V1
{
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("search/{userId}")]
        public async Task<IActionResult> Search([FromRoute] long userId)
        {
            return Ok(await _mediator.Send(new SearchUserRequest() { UserId = userId}));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] EditUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("list")]
        public async Task<IActionResult> List([FromBody] ListUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("remove/{userId}")]
        public async Task<IActionResult> Remove([FromRoute] long userId)
        {
            return Ok(await _mediator.Send(new RemoveUserRequest() { UserId = userId }));
        }
    }
}
