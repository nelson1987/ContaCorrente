using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Tamuz.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Bloqueio([FromBody] MovimentacaoCommand request, [FromHeader] string partnerId)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
            {
                return StatusCode((int)result.StatusCode, result.Data);
            }

            return StatusCode((int)result.StatusCode, new { erros = result.Errors });
        }
    }
    public class MovimentacaoCommand 
    {
        public string Conta { get; set; }
        public decimal Valor { get; set; }
    }
}