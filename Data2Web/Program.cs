using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Data2Web.Data.Context;
using Microsoft.Extensions.Logging;

// TODO: importa también tus repos, services y generators

internal class Program
{
    private static async Task Main(string[] args)
    {
        // 1. Crear Host con configuración y logging
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(cfg =>
            {
                cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .UseSerilog((ctx, lc) => lc
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .Enrich.FromLogContext()
            )
            .ConfigureServices((ctx, services) =>
            {
                // 2. Registro de infraestructura
                services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
                services.AddScoped<IDbConnection>(sp =>
                    sp.GetRequiredService<IDbConnectionFactory>().Create());

                // 3. TODO: agrega tus repositorios
                // services.AddScoped<IPersonaRepository, PersonaRepository>();

                // 4. TODO: agrega tus servicios
                // services.AddScoped<IPersonaService, PersonaService>();

                // 5. TODO: agrega generadores (PageGenerator, JsonExporter)
                // services.AddSingleton<IPageGenerator, PageGenerator>();
                // services.AddSingleton<IJsonExporter, JsonExporter>();
            });

        var host = builder.Build();

        // 6. Scope para probar que arranca bien
        using var scope = host.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("✅ ejecutado correctamente. Host en marcha.");

        Console.WriteLine("Data2Web listo con DI, Logging y SQL Server. 🚀");
        await Task.CompletedTask;

    }


}


