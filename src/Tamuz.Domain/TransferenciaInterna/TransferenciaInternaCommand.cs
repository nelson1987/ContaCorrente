using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Tamuz.Domain.Movimentacao;
using Tamuz.Domain.Movimentacao.Inclusao;
using Tamuz.Domain.Repositories;

namespace Tamuz.Domain.TransferenciaInterna
{
    public class LogEventHandler :
                            INotificationHandler<TransferenciaExternaIncluidaNotification>,
                            INotificationHandler<TransferenciaPixIncluidaNotification>,
                            INotificationHandler<TransferenciaChequeIncluidaNotification>,
                            INotificationHandler<ErroNotification>
    {
        public Task Handle(TransferenciaExternaIncluidaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                //Console.WriteLine($"CRIACAO: '{notification.Id} - {notification.Nome} - {notification.Idade} - {notification.Sexo}'");
            });
        }

        public Task Handle(TransferenciaPixIncluidaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                //Console.WriteLine($"ALTERACAO: '{notification.Id} - {notification.Nome} - {notification.Idade} - {notification.Sexo} - {notification.IsEfetivado}'");
            });
        }

        public Task Handle(TransferenciaChequeIncluidaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                //Console.WriteLine($"EXCLUSAO: '{notification.Id} - {notification.IsEfetivado}'");
            });
        }

        public Task Handle(ErroNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERRO: '{notification.Excecao} \n {notification.PilhaErro}'");
            });
        }
    }
    public class TransferenciaExternaIncluidaNotification : INotification { }
    public class TransferenciaPixIncluidaNotification : INotification { }
    public class TransferenciaChequeIncluidaNotification : INotification { }
    public class TransferenciaInternaCommand : ICommand<MovimentacaoResponse>
    {
    }
    public class TransferenciaExternaCommand : ICommand<MovimentacaoResponse>
    {
    }
    public class TransferenciaPixCommand : ICommand<MovimentacaoResponse>
    {
    }
    public class TransferenciaChequeCommand : ICommand<MovimentacaoResponse>
    {
    }
    public class TransferenciaInternaHandler : IRequestHandler<TransferenciaInternaCommand, MovimentacaoResponse>
    {
        //TODO: Se for Transferencia Interna - Apenas Banco de Dados
        private readonly ILogger<MovimentacaoHandler> logger;
        private readonly IMediator mediator;
        private readonly IMovimentacaoRepository contaRepository;

        public TransferenciaInternaHandler(ILogger<MovimentacaoHandler> logger, IMediator mediator, IMovimentacaoRepository contaRepository)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.contaRepository = contaRepository;
        }

        public async Task<MovimentacaoResponse> Handle(TransferenciaInternaCommand request, CancellationToken cancellationToken)
        {
            var movimentacao = request.Adapt<MovimentacaoModel>();
            await contaRepository.Add(movimentacao);
            return await Task.FromResult(movimentacao.Adapt<MovimentacaoResponse>());
        }
    }
    public class TransferenciaExternaHandler : IRequestHandler<TransferenciaExternaCommand, MovimentacaoResponse>
    {
        //TODO: Se for Transferencia Externa - Fila, para validar o status
        private readonly ILogger<MovimentacaoHandler> logger;
        private readonly IMediator mediator;
        private readonly IMovimentacaoRepository contaRepository;

        public TransferenciaExternaHandler(ILogger<MovimentacaoHandler> logger, IMediator mediator, IMovimentacaoRepository contaRepository)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.contaRepository = contaRepository;
        }

        public async Task<MovimentacaoResponse> Handle(TransferenciaExternaCommand request, CancellationToken cancellationToken)
        {
            var movimentacao = request.Adapt<MovimentacaoModel>();
            await mediator.Publish(new TransferenciaExternaIncluidaNotification { });
            return await Task.FromResult(movimentacao.Adapt<MovimentacaoResponse>());
            //var pessoa = new Pessoa { Nome = request.Nome, Idade = request.Idade, Sexo = request.Sexo };
            //
            //pessoa = await _repository.Add(pessoa);
            //
            //await _mediator.Publish(new PessoaCriadaNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });
        }
    }
    public class TransferenciaPixHandler : IRequestHandler<TransferenciaPixCommand, MovimentacaoResponse>
    {
        //TODO: Se for Pix - Banco de Dados + Notifica por Email
        private readonly ILogger<MovimentacaoHandler> logger;
        private readonly IMediator mediator;
        private readonly IMovimentacaoRepository contaRepository;

        public TransferenciaPixHandler(ILogger<MovimentacaoHandler> logger, IMediator mediator, IMovimentacaoRepository contaRepository)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.contaRepository = contaRepository;
        }

        public async Task<MovimentacaoResponse> Handle(TransferenciaPixCommand request, CancellationToken cancellationToken)
        {
            var movimentacao = request.Adapt<MovimentacaoModel>();
            await contaRepository.Add(movimentacao);
            await mediator.Publish(new TransferenciaPixIncluidaNotification { });
            return await Task.FromResult(movimentacao.Adapt<MovimentacaoResponse>());
        }
    }
    public class TransferenciaChequeHandler : IRequestHandler<TransferenciaChequeCommand, MovimentacaoResponse>
    {
        //TODO: Se for Cheque - Banco de Dados + Fila, para backoffice
        private readonly ILogger<MovimentacaoHandler> logger;
        private readonly IMediator mediator;
        private readonly IMovimentacaoRepository contaRepository;

        public TransferenciaChequeHandler(ILogger<MovimentacaoHandler> logger, IMediator mediator, IMovimentacaoRepository contaRepository)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.contaRepository = contaRepository;
        }

        public async Task<MovimentacaoResponse> Handle(TransferenciaChequeCommand request, CancellationToken cancellationToken)
        {
            var movimentacao = request.Adapt<MovimentacaoModel>();
            await contaRepository.Add(movimentacao);
            await mediator.Publish(new TransferenciaChequeIncluidaNotification { });
            return await Task.FromResult(movimentacao.Adapt<MovimentacaoResponse>());
        }
    }
}
