using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yukumyolda.Application.Features.Commands.AuthCommands;
using FluentValidation;
namespace yukumyolda.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterCommand request)
        {
            try
            {
                await mediator.Send(request);
                return StatusCode(StatusCodes.Status201Created, new { message = "Kullanıcı başarıyla kaydedildi." });
            }
            catch (Exception ex)
            {
                if (ex is ValidationException vex && vex.Errors != null)
                {
                    var errorMessages = vex.Errors.Select(e => e.ErrorMessage).ToArray();
                    return BadRequest(new { errors = errorMessages });
                }
                return BadRequest(new { errors = new[] { ex.Message } });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand request)
        {
            try
            {
                var response = await mediator.Send(request);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand request)
        {
            try
            {
                var response = await mediator.Send(request);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
       
    }
}
