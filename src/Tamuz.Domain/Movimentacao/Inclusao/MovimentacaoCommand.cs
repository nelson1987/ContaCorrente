using MediatR;
using Microsoft.Extensions.Logging;
using Tamuz.Domain.Repositories;
using Tamuz.Domain.TransferenciaInterna;

namespace Tamuz.Domain.Movimentacao.Inclusao
{
    public interface IRouterCommandFactory
    {
        TransferenciaInternaCommand MapTransferToInternalTransferAutbank(MovimentacaoCommand command);
        // InternalTransferSmartCommand MapTransferToInternalTransferSmart(TransferCommand command);
    }

    public class RouterCommandFactory : IRouterCommandFactory
    {
        public TransferenciaInternaCommand MapTransferToInternalTransferAutbank(MovimentacaoCommand command)
        {
            var result = new TransferenciaInternaCommand()
            {
                //OperationDate = command.OperationDate,
                //Description = command.Description,
                //Transfer = command.Transfer,
                //DebitorAccount = command.DebitorAccount,
                //Creditor = command.Creditor,
                //CodHistCredito = command.CodHistCredito,
                //CodHistDebito = command.CodHistDebito,
            };

            return result;
        }
    }
    public class ErroNotification
    {
        public string Excecao { get; internal set; }
        public string? PilhaErro { get; internal set; }
    }
    public class MovimentacaoIncluidaNotification : INotification
    {

    }
    public class MovimentacaoCommand : ICommand<MovimentacaoResponse>
    {
        public string Ispb { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataOperacao { get; set; }
        public int Tipo { get; set; }
       ´public TipoMovimentacao GetTransferType()
        {
            return (TipoMovimentacao)Tipo;
        }
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
        private readonly IMovimentacaoRepository contaRepository;
        private readonly IRouterCommandFactory routerCommandFactory;

        public MovimentacaoHandler(ILogger<MovimentacaoHandler> logger, IMediator mediator, IMovimentacaoRepository contaRepository, IRouterCommandFactory routerCommandFactory)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.contaRepository = contaRepository;
            this.routerCommandFactory = routerCommandFactory;
        }

        public async Task<MovimentacaoResponse> Handle(MovimentacaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("[INICIO]");

                var handlerResponse = request.GetTransferType() switch
                {
                    TipoMovimentacao.Ted => await mediator.Send(routerCommandFactory.MapTransferToInternalTransferAutbank(request)),
                    //TipoMovimentacao.Tef => await _mediator.Send(_routerCommandFactory.MapTransferToInternalTransferSmart(request)),
                    //TipoMovimentacao.Pix => ErroInterno(),
                    //TipoMovimentacao.Cheque => ErroInterno(),
                    //TipoMovimentacao.Pix => ErroInterno(),
                    _ => throw new ArgumentOutOfRangeException()
                };
                //mediator.Send(request);
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
            catch (Exception ex)
            {
                await mediator.Publish(new MovimentacaoIncluidaNotification { });
                await mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult<MovimentacaoResponse>(new MovimentacaoResponse() { });
            }
            throw new NotImplementedException();
        }

    }
}
