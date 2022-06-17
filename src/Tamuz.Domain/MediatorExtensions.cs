using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Tamuz.Domain
{
    public static class MediatorExtensions
    {
        public static void AddCustomMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
