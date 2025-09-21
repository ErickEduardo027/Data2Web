using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

using Data2Web.Data.Context;
using Data2Web.Data.Repositories;
using Data2Web.Data.Repositories.Interfaces;

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

                // 3. Repositorios
                services.AddScoped<IPersonaRepository, PersonaRepository>();
                services.AddScoped<IPasatiempoRepository, PasatiempoRepository>();
                services.AddScoped<IYouTuberRepository, YouTuberRepository>();
                services.AddScoped<IAnimeSerieRepository, AnimeSerieRepository>();
                services.AddScoped<IGenealogiaRepository, GenealogiaRepository>();
                services.AddScoped<ITimelineRepository, TimelineRepository>();
                services.AddScoped<ISocialLinksRepository, SocialLinksRepository>();

            });

        var host = builder.Build();

        // 4. Scope para probar que arranca bien
        using var scope = host.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            var personaRepo = scope.ServiceProvider.GetRequiredService<IPersonaRepository>();
            var pasatiempoRepo = scope.ServiceProvider.GetRequiredService<IPasatiempoRepository>();
            var ytRepo = scope.ServiceProvider.GetRequiredService<IYouTuberRepository>();
            var animeRepo = scope.ServiceProvider.GetRequiredService<IAnimeSerieRepository>();
            var genealogiaRepo = scope.ServiceProvider.GetRequiredService<IGenealogiaRepository>();
            var timelineRepo = scope.ServiceProvider.GetRequiredService<ITimelineRepository>();
            var socialRepo = scope.ServiceProvider.GetRequiredService<ISocialLinksRepository>();

            var persona = await personaRepo.GetPrincipalAsync();
            var animes = await animeRepo.GetByPersonaIdAsync(persona.PersonaId);
            var youtubers = await ytRepo.GetAllAsync();
            var familiares = await genealogiaRepo.GetByPersonaIdAsync(persona.PersonaId);
            var eventos = await timelineRepo.GetByPersonaIdAsync(persona.PersonaId);
            var redes = await socialRepo.GetByPersonaIdAsync(persona.PersonaId);


            if (persona != null)
            {
                Console.WriteLine($"👤 Persona principal: {persona.Nombres} {persona.Apellidos} (Nacido: {persona.FechaNacimiento:dd/MM/yyyy})");

                var pasatiempos = await pasatiempoRepo.GetByPersonaIdAsync(persona.PersonaId);

                Console.WriteLine("🎨 Pasatiempos:");
                foreach (var p in pasatiempos)
                {
                    Console.WriteLine($" - {p.Titulo}: {p.Descripcion}");
                }

                Console.WriteLine("📺 YouTubers favoritos:");
                foreach (var yt in youtubers)
                {
                    Console.WriteLine($" - {yt.Nombre} ({yt.UrlCanal})");
                    Console.WriteLine($"   {yt.Descripcion}");
                }

                Console.WriteLine("🎬 Animes / Series favoritas:");
                foreach (var anime in animes)
                {
                    Console.WriteLine($" - {anime.Titulo}");
                    Console.WriteLine($"   {anime.Descripcion}");
                    Console.WriteLine($"   Carátula: {anime.CaratulaUrl}");
                    Console.WriteLine($"   Trailer: https://www.youtube.com/watch?v={anime.TrailerYoutubeId}");
                }

                Console.WriteLine("👨‍👩‍👦 Genealogía:");
                foreach (var fam in familiares)
                {
                    Console.WriteLine($" - {fam.Parentesco}: {fam.Nombre} (Foto: {fam.FotoUrl})");
                }

                Console.WriteLine("🗓️ Línea de tiempo:");
                foreach (var ev in eventos)
                {
                    Console.WriteLine($" - {ev.Fecha:dd/MM/yyyy}: {ev.Titulo} ({ev.Descripcion})");
                }

                Console.WriteLine("🌐 Redes sociales:");
                foreach (var s in redes)
                {
                    Console.WriteLine($" - {s.RedSocial}: {s.Url}");
                }


            }
            else
            {
                Console.WriteLine("⚠️ No se encontró la persona principal en la BD.");
            }

            logger.LogInformation("✅ Data2Web ejecutado correctamente.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error durante la ejecución.");
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}


