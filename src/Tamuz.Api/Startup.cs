using Tamuz.Domain;
using Tamuz.Domain.Repositories;
using Tamuz.Infra.Data.Repositories;

namespace Tamuz.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMediator();

           services
               .AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
        }
    }
}
