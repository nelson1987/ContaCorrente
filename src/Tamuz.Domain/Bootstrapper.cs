using Microsoft.Extensions.DependencyInjection;
using Tamuz.Domain.Repositories;

namespace Tamuz.Domain
{
    public class Bootstrapper
    {
        public static void AddBootstrapper(this IServiceCollection services)
        {
            services
                .AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
        }
    }
}
