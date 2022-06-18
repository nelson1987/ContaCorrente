using MediatR;
using Microsoft.Extensions.Logging;
using Tamuz.Domain.Repositories;

namespace Tamuz.Domain.Movimentacao.Inclusao
{
    public class ErroNotification
    {
        public string Excecao { get; internal set; }
        public string? PilhaErro { get; internal set; }
    }
    public class MovimentacaoIncluidaNotification: INotification
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
        private readonly ILogger<MovimentacaoHandler> logger;
        private readonly IMediator mediator;
        private readonly IContaRepository contaRepository;
        public MovimentacaoHandler(ILogger<MovimentacaoHandler> logger, IMediator mediator, IContaRepository contaRepository)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.contaRepository = contaRepository;
        }

        public async Task<MovimentacaoResponse> Handle(MovimentacaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("[INICIO]");
                mediator.Send(request);
                var movimentacao = new MovimentacaoModel() { };
                await contaRepository.Add(movimentacao);
                await mediator.Publish(new MovimentacaoIncluidaNotification { });
                //var pessoa = new Pessoa { Nome = request.Nome, Idade = request.Idade, Sexo = request.Sexo };
                //
                //pessoa = await _repository.Add(pessoa);
                //
                //await _mediator.Publish(new PessoaCriadaNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });

                return await Task.FromResult<MovimentacaoResponse>(new MovimentacaoResponse() { });
            }
            catch(Exception ex)
            {
                await mediator.Publish(new MovimentacaoIncluidaNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });
                await mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult<MovimentacaoResponse>(new MovimentacaoResponse() { });
            }
            throw new NotImplementedException();
        }

    }
}
