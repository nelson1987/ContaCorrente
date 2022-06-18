using MediatR;
using Microsoft.Extensions.Logging;

namespace Tamuz.Domain.Movimentacao.Inclusao
{
    public class MovimentacaoNotification: INotification
    {

    }
    public class MovimentacaoCommand : ICommand<MovimentacaoResponse>
    {
        public string Conta { get; set; }
        public decimal Valor { get; set; }
    }
    public class MovimentacaoResponse : IResponse
    {
        public string Conta { get; set; }
        public decimal Valor { get; set; }
    }
    public class MovimentacaoHandler : IRequestHandler<MovimentacaoCommand, MovimentacaoResponse>
    {
        private readonly ILogger<MovimentacaoHandler> _logger;
        private readonly IMediator _mediator;
        public MovimentacaoHandler(ILogger<MovimentacaoHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<MovimentacaoResponse> Handle(MovimentacaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("[INICIO]");
                _mediator.Send(request);
                //var pessoa = new Pessoa { Nome = request.Nome, Idade = request.Idade, Sexo = request.Sexo };
                //
                //pessoa = await _repository.Add(pessoa);
                //
                //await _mediator.Publish(new PessoaCriadaNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });

                return await Task.FromResult<MovimentacaoResponse>(new MovimentacaoResponse() { });
            }
            catch
            {
                //await _mediator.Publish(new PessoaCriadaNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });
                //await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                //return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
            throw new NotImplementedException();
        }

    }
}
