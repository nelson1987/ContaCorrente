using MediatR;
using Microsoft.Extensions.Logging;

namespace Tamuz.Domain.Movimentacao.Inclusao
{
    public class MovimentacaoCommand : ICommand
    {
        public string Conta { get; set; }
        public decimal Valor { get; set; }
    }
    public class MovimentacaoResponse : IResponse
    {
        public string Conta { get; set; }
        public decimal Valor { get; set; }
    }
    public class MovimentacaoHandler : IHandlerBase<MovimentacaoCommand, MovimentacaoResponse>
    {
        private readonly ILogger<MovimentacaoHandler> _logger;
        private readonly IMediator _mediator;
        public MovimentacaoHandler(ILogger<MovimentacaoHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public Task<MovimentacaoCommand> Handle(MovimentacaoCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("[INICIO]");
                _mediator.Send(command);
            }
            catch { 
            }
            throw new NotImplementedException();
        }
    }
}
