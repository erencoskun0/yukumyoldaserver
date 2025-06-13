using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yukumyolda.Application.Features.Commands.LoadCommands;
using yukumyolda.Application.Features.Commands.VehicleCommands;
using yukumyolda.Application.Features.Queries.LoadQueries;
using yukumyolda.Application.Features.Queries.LoadStatusQueries;
using yukumyolda.Application.Features.Queries.VehicleQueries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace yukumyolda.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoadsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoadsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoads()
        {
            var value = await _mediator.Send(new GetAllLoadsQuery());
            return Ok(value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoadsNotLogin()
        {
            var value = await _mediator.Send(new GetAllLoadsNotLoginQuery());
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLoadStates()
        {
            var value = await _mediator.Send(new LoadStatusQuery());
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdLoad(Guid loadid)
        {
            var value = await _mediator.Send(new GetByIdLoadQuery {LoadId = loadid });
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdUserLoads(Guid loadid)
        {
            var value = await _mediator.Send(new GetByIdLoadQuery { LoadId = loadid });
            return Ok(value);
        }

        [HttpPost]

        public async Task<IActionResult> CreateLoad(CreateLoadCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ekleme İşlemi Başarılı");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateLoad(UpdateLoadCommand command)
        {
            await _mediator.Send(command);
            return Ok("Güncelleme İşlemi Başarılı");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateLoadState(UpdateLoadStateCommand command)
        {
            await _mediator.Send(command);
            return Ok("Güncelleme İşlemi Başarılı");
        }
        [HttpPut]

        public async Task<IActionResult> RemoveLoad(RemoveLoadCommand command)
        {
            await _mediator.Send(command);
            return Ok("Güncelleme İşlemi Başarılı");
        }
    }
}
