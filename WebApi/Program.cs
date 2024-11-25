using DAS.GoT.Behaviour.Services;
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
