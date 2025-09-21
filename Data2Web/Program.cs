using Data2Web.Data.Context;
using Data2Web.Data.Repositories;
using Data2Web.Data.Repositories.Interfaces;
using Data2Web.Logic.DTOs;
using Data2Web.Logic.Generators;
using Data2Web.Logic.Services;
using Data2Web.Logic.Services.InterfacesDeServicios;
using Data2Web.Logic.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Data;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Host con configuración y logging
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
                // Infraestructura
                services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
                services.AddScoped<IDbConnection>(sp =>
                    sp.GetRequiredService<IDbConnectionFactory>().Create());

                // Repositorios
                services.AddScoped<IPersonaRepository, PersonaRepository>();
                services.AddScoped<IPasatiempoRepository, PasatiempoRepository>();
                services.AddScoped<IYouTuberRepository, YouTuberRepository>();
                services.AddScoped<IAnimeSerieRepository, AnimeSerieRepository>();
                services.AddScoped<IGenealogiaRepository, GenealogiaRepository>();
                services.AddScoped<ITimelineRepository, TimelineRepository>();
                services.AddScoped<ISocialLinksRepository, SocialLinksRepository>();

                // Servicios
                services.AddScoped<IPersonaService, PersonaService>();
                services.AddScoped<IPasatiempoService, PasatiempoService>();
                services.AddScoped<IYouTuberService, YouTuberService>();
                services.AddScoped<IAnimeSerieService, AnimeSerieService>();
                services.AddScoped<IGenealogiaService, GenealogiaService>();
                services.AddScoped<ITimelineService, TimelineService>();
                services.AddScoped<ISocialLinksService, SocialLinksService>();

                // Utilidades
                services.AddHttpClient<ImageDownloader>();
                services.AddSingleton<JsonExporter>();
                services.AddSingleton<PageGenerator>();
            });

        var host = builder.Build();

        using var scope = host.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        string exportDir = Path.Combine(AppContext.BaseDirectory, "Export");
        string pagesDir = Path.Combine(AppContext.BaseDirectory, "Pages");

        Directory.CreateDirectory(exportDir);
        Directory.CreateDirectory(pagesDir);

        try
        {
            // Servicios
            var personaService = scope.ServiceProvider.GetRequiredService<IPersonaService>();
            var pasatiempoService = scope.ServiceProvider.GetRequiredService<IPasatiempoService>();
            var ytService = scope.ServiceProvider.GetRequiredService<IYouTuberService>();
            var animeService = scope.ServiceProvider.GetRequiredService<IAnimeSerieService>();
            var timelineService = scope.ServiceProvider.GetRequiredService<ITimelineService>();
            var socialService = scope.ServiceProvider.GetRequiredService<ISocialLinksService>();

            var jsonExp = scope.ServiceProvider.GetRequiredService<JsonExporter>();
            var pageGen = scope.ServiceProvider.GetRequiredService<PageGenerator>();

            // Persona principal
            var personaDTO = await personaService.GetPersonaPrincipalAsync();
            if (personaDTO?.Datos == null)
            {
                Console.WriteLine("⚠️ No se encontró la persona principal.");
                return;
            }

            await jsonExp.ExportAsync(new[] { personaDTO }, Path.Combine(exportDir, "persona.json"));
            await pageGen.GenerateAsync(
                Path.Combine(AppContext.BaseDirectory, "Templates", "SobreMi.hbs"),
                personaDTO,
                Path.Combine(pagesDir, "sobre-mi.html")
            );

            // Pasatiempos
            var pasatiempos = await pasatiempoService.GetByPersonaIdAsync(personaDTO.Datos.PersonaId);
            await jsonExp.ExportAsync(pasatiempos, Path.Combine(exportDir, "pasatiempos.json"));
            await pageGen.GenerateAsync(
                Path.Combine(AppContext.BaseDirectory, "Templates", "Pasatiempos.hbs"),
                new { Items = pasatiempos },
                Path.Combine(pagesDir, "pasatiempos.html")
            );

            // YouTubers
            var youtubers = await ytService.GetAllAsync();
            await jsonExp.ExportAsync(youtubers, Path.Combine(exportDir, "youtubers.json"));
            await pageGen.GenerateAsync(
                Path.Combine(AppContext.BaseDirectory, "Templates", "YouTubers.hbs"),
                new { Items = youtubers },
                Path.Combine(pagesDir, "youtubers.html")
            );

            // AnimeSeries
            var animes = await animeService.GetByPersonaIdAsync(personaDTO.Datos.PersonaId);
            await jsonExp.ExportAsync(animes, Path.Combine(exportDir, "animeseries.json"));
            await pageGen.GenerateAsync(
                Path.Combine(AppContext.BaseDirectory, "Templates", "AnimeSeries.hbs"),
                new { Items = animes },
                Path.Combine(pagesDir, "animeseries.html")
            );

            // Timeline
            var timeline = await timelineService.GetByPersonaIdAsync(personaDTO.Datos.PersonaId);
            await jsonExp.ExportAsync(timeline, Path.Combine(exportDir, "timeline.json"));
            await pageGen.GenerateAsync(
                Path.Combine(AppContext.BaseDirectory, "Templates", "Timeline.hbs"),
                new { Items = timeline },
                Path.Combine(pagesDir, "timeline.html")
            );

            // Redes Sociales
            var redes = await socialService.GetByPersonaIdAsync(personaDTO.Datos.PersonaId);
            await jsonExp.ExportAsync(redes, Path.Combine(exportDir, "sociallinks.json"));
            await pageGen.GenerateAsync(
                Path.Combine(AppContext.BaseDirectory, "Templates", "SocialLinks.hbs"),
                new { Items = redes },
                Path.Combine(pagesDir, "sociallinks.html")
            );

            // Index
            await pageGen.GenerateAsync(
                Path.Combine(AppContext.BaseDirectory, "Templates", "Index.hbs"),
                new { },
                Path.Combine(pagesDir, "index.html")
            );

            // Contacto
            await pageGen.GenerateAsync(
                Path.Combine(AppContext.BaseDirectory, "Templates", "Contacto.hbs"),
                new { },
                Path.Combine(pagesDir, "contacto.html")
            );

            // Copiar Assets
            string assetsSource = Path.Combine(AppContext.BaseDirectory, "Assets");
            string assetsDest = Path.Combine(pagesDir, "Assets");

            if (Directory.Exists(assetsDest))
                Directory.Delete(assetsDest, true);

            CopyDirectory(assetsSource, assetsDest);

            Console.WriteLine("✅ Todos los JSON, HTML y Assets copiados correctamente.");
            logger.LogInformation("🚀 Data2Web ejecutado correctamente.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error durante la ejecución.");

            var pageGen = scope.ServiceProvider.GetRequiredService<PageGenerator>();
            string errorPath = Path.Combine(pagesDir, "error.html");

            await pageGen.GenerateAsync(
                Path.Combine(AppContext.BaseDirectory, "Templates", "Error.hbs"),
                new { Mensaje = ex.Message },
                errorPath
            );

            Console.WriteLine($"❌ Error: {ex.Message} — Se generó error.html");
        }
    }

    // Copiar directorios recursivamente (incluye CSS, JS, Img)
    private static void CopyDirectory(string sourceDir, string destDir)
    {
        Directory.CreateDirectory(destDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string targetFile = Path.Combine(destDir, Path.GetFileName(file));
            File.Copy(file, targetFile, true);
        }

        foreach (string dir in Directory.GetDirectories(sourceDir))
        {
            string targetSubDir = Path.Combine(destDir, Path.GetFileName(dir));
            CopyDirectory(dir, targetSubDir);
        }
    }
}
