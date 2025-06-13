using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yukumyolda.Application.Features.Commands.ProvinceCommands;
using yukumyolda.Application.Features.Commands.VehicleCommands;
using yukumyolda.Application.Features.Queries.ProvinceQueries;
using yukumyolda.Application.Features.Queries.VehicleBodyQueries;
using yukumyolda.Application.Features.Queries.VehicleQueries;
using yukumyolda.Application.Features.Queries.VehicleTypeQueries;

namespace yukumyolda.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var value = await _mediator.Send(new GetAllVehicleQuery());
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVehiclesNotNull()
        {
            var value = await _mediator.Send(new GetAllVehicleNotLoginQuery());
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVehicleBodies()
        {
            var value = await _mediator.Send(new VehicleBodyQueries());
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVehicleTypes()
        {
            var value = await _mediator.Send(new VehicleTypeQuery());
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdVehicle(Guid vehicleId)
        {
            var value = await _mediator.Send(new GetByIdVehicleQuery { VehicleId = vehicleId});
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetByUserIdVehicle(Guid userId)
        {
            var value = await _mediator.Send(new GetByUserIdVehicleQuery { UserId = userId });
            return Ok(value);
        }

        [HttpPost]

        public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ekleme İşlemi Başarılı");
        }

        [HttpPut]

        public async Task<IActionResult> UpdateVehicle(UpdateVehicleCommand command)
        {
            await _mediator.Send(command);
            return Ok("Güncelleme İşlemi Başarılı");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateVehicleState(UpdateVehicleStateCommand command)
        {
            await _mediator.Send(command);
            return Ok("Güncelleme İşlemi Başarılı");
        }
    }
}
