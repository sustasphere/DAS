using DAS.GoT.Behaviour.Consumers;
using DAS.GoT.Behaviour.Filters;
using DAS.GoT.Behaviour.Services;
using DAS.GoT.Types.Utils;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DAS.GoT.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        (var builder, var srvSetup) = (WebApplication.CreateBuilder(args), new ServerSetup());

        var withConnString = builder.Configuration.GetConnectionString("DevDb");
        Action<DbContextOptionsBuilder> withOptionsBuilder = ob => ob.UseSqlite(withConnString);

        builder.Configuration.Bind(ServerSetup.Key, srvSetup);
        builder.Services.Configure<ServerSetup>(builder.Configuration.GetSection(ServerSetup.Key));

        builder.Services.AddDbContext<PersonContext>(withOptionsBuilder);
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<ICoreStore, CoreStore>();
        builder.Services.AddHostedService<DataBackgroundService>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMediator(cfg => {
            cfg.AddConsumer<AddCharacterRequestConsumer>();
            cfg.AddConsumer<AddCharacterNotificationConsumer>();
            cfg.ConfigureMediator((mctx, mcfg) => {
                mcfg.UseSendFilter(typeof(AddCharacterRequestFilter<>), mctx);
            });
        });

        var app = builder.Build();

        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
