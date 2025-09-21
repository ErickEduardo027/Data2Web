using Data2Web.Data.Repositories;
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
    public class PasatiempoService : IPasatiempoService
    {
        private readonly IPasatiempoRepository _repo;
        private readonly ImageDownloader _downloader;

        public PasatiempoService(IPasatiempoRepository repo, ImageDownloader downloader)
        {
            _repo = repo;
            _downloader = downloader;
        }

        public async Task<IEnumerable<PasatiempoDTO>> GetByPersonaIdAsync(int personaId)
        {
            var pasatiempos = await _repo.GetByPersonaIdAsync(personaId);
            var list = new List<PasatiempoDTO>();

            foreach (var p in pasatiempos)
            {
                // nombre seguro para archivo local
                var fileName = $"{p.PasatiempoId}_{p.Titulo.Replace(" ", "_")}.jpg";
                var localPath = Path.Combine("Assets", "img", "pasatiempos", fileName);

                // descarga la imagen si existe una URL
                if (!string.IsNullOrWhiteSpace(p.ImagenUrl))
                {
                    await _downloader.DownloadImageAsync(p.ImagenUrl, localPath);
                }

                list.Add(new PasatiempoDTO
                {
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    ImagenLocalPath = localPath.Replace("\\", "/")
                });
            }

            return list;
        }
    }
}
