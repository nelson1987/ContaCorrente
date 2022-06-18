using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tamuz.Domain.Movimentacao.Inclusao;
using Tamuz.Domain.Movimentacao.Pesquisa;

namespace Tamuz.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferenciaInternaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransferenciaInternaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> Pesquisar([FromQuery] MovimentacaoQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
            //if (result.IsSuccess)
            //{
            //    return StatusCode((int)result.StatusCode, result.Data);
            //}

            //return StatusCode((int)result.StatusCode, new { erros = result.Errors });
        }

        [HttpPost]
        public async Task<IActionResult> Incluir([FromBody] MovimentacaoCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);

            //if (result.IsSuccess)
            //{
            //    return StatusCode((int)result.StatusCode, result.Data);
            //}

            //return StatusCode((int)result.StatusCode, new { erros = result.Errors });
        }
    }
}