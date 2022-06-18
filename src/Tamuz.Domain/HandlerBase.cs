using MediatR;

namespace Tamuz.Domain
{
    public interface ICommand<V> : IRequest<V> where V : IResponse
    {
    }
    public interface IResponse
    {
    }
}
