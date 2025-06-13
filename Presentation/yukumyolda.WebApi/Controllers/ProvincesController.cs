using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using yukumyolda.Application.Features.Queries.ProvinceQueries;
using Microsoft.AspNetCore.Http.HttpResults;
using yukumyolda.Application.Features.Commands.ProvinceCommands;
namespace yukumyolda.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvincesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProvince()
        {
            var value = await _mediator.Send(new GetProvinceQuery());
            return Ok(value);   
        }

        [HttpPost]

        public async Task<IActionResult> CreateProvince(CreateProvinceCommand command)
        {
          await  _mediator.Send(command);
            return Ok("Ekleme İşlemi Başarılı");
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteProvince(int id)
        {
           await _mediator.Send(new RemoveProvinceCommand(id));
            return Ok("Silme İşlemi Başarılı");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateProvince(UpdateProvinceCommand command)
        {
           await _mediator.Send(command);
            return Ok("Güncelleme İşlemi Başarılı");
        }
    }
}
