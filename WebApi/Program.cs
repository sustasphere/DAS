using DAS.GoT.Behaviour.Consumers;
using DAS.GoT.Behaviour.Filters;
using DAS.GoT.Behaviour.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DAS.GoT.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DevDb");

        builder.Services.AddDbContext<PersonContext>(cfg => cfg.UseSqlite(connectionString));
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
