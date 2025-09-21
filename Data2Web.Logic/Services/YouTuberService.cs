using Data2Web.Data.Repositories.Interfaces;
using Data2Web.Logic.DTOs;
using Data2Web.Logic.Services.InterfacesDeServicios;
using Data2Web.Logic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.Services
{
    public class YouTuberService : IYouTuberService
    {
        private readonly IYouTuberRepository _ytRepo;
        private readonly ImageDownloader _downloader;

        public YouTuberService(IYouTuberRepository ytRepo, ImageDownloader downloader)
        {
            _ytRepo = ytRepo;
            _downloader = downloader;
        }

        public async Task<IEnumerable<YouTuberDTO>> GetAllAsync()
        {
            var result = new List<YouTuberDTO>();
            var entities = await _ytRepo.GetAllAsync();

            foreach (var yt in entities)
            {
                // Ruta local donde guardaremos el logo
                var localPath = $"assets/img/youtubers/{yt.Nombre}.png";

                if (!string.IsNullOrEmpty(yt.LogoUrl))
                {
                    await _downloader.DownloadImageAsync(yt.LogoUrl, localPath);
                }

                result.Add(new YouTuberDTO
                {
                    YouTuberId = yt.YouTuberId,
                    Nombre = yt.Nombre,
                    UrlCanal = yt.UrlCanal,
                    Descripcion = yt.Descripcion,
                    LogoPath = localPath
                });
            }

            return result;
        }
    }
}
