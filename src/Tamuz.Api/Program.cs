using MassTransit;
using System.Reflection;
using Tamuz.Domain;
using Tamuz.Domain.Movimentacao.Inclusao;
using Tamuz.Domain.Repositories;
using Tamuz.Domain.TransferenciaInterna;
using Tamuz.Infra.Data.Repositories;

//private IConfiguration Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddCustomMediator();

builder.Services
    .AddScoped<IMovimentacaoRepository, MovimentacaoRepository>()
    .AddSingleton<IRouterCommandFactory, RouterCommandFactory>();

builder.Services.AddMassTransit(MassT =>
{
    MassT.UsingRabbitMq((Context, Configure) =>
    {
        Configure.Host(new Uri(builder.Configuration["Rabbit:Host"]), host =>
        {
            host.Username(builder.Configuration["Rabbit:UserName"]);
            host.Password(builder.Configuration["Rabbit:Password"]);
            host.Heartbeat(ushort.Parse(builder.Configuration["Rabbit:HeartBeat"]));
        });

        Configure.Message<TransferenciaExternaIncluidaNotification>(message =>
        {
            message.SetEntityName("Tamuz.Notification.TransferenciaExternaIncluida");
        });
        Configure.Message<TransferenciaPixIncluidaNotification>(message =>
        {
            message.SetEntityName("Tamuz.Notification.TransferenciaPixIncluida");
        });
        Configure.Message<TransferenciaChequeIncluidaNotification>(message =>
        {
            message.SetEntityName("Tamuz.Notification.TransferenciaChequeIncluida");
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
