namespace Tamuz.Domain
{
    public interface ICommand
    {
    }
    public interface IResponse
    {
    }
    public interface IHandlerBase<T, V> where T : ICommand where V : IResponse
    {
        Task<T> Handle(T command, CancellationToken cancellationToken);
    }
}
