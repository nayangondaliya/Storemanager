using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raditap.BusinessLogic.ApiDataObjects.Users.Login;
using System.Threading.Tasks;

namespace Api.BackOffice.Controllers.V1
{
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
