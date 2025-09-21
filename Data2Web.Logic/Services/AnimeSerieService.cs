using Data2Web.Data.Repositories.Interfaces;
using Data2Web.Logic.DTOs;
using Data2Web.Logic.Services.InterfacesDeServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.Services
{
    public class AnimeSerieService : IAnimeSerieService
    {
        private readonly IAnimeSerieRepository _animeRepo;

        public AnimeSerieService(IAnimeSerieRepository animeRepo)
        {
            _animeRepo = animeRepo;
        }

        public async Task<IEnumerable<AnimeSerieDTO>> GetAllAsync()
        {
            var entities = await _animeRepo.GetAllAsync();

            return entities.Select(a => new AnimeSerieDTO
            {
                AnimeSerieId = a.AnimeSerieId,
                Titulo = a.Titulo,
                Descripcion = a.Descripcion,
                CaratulaUrl = a.CaratulaUrl,
                TrailerYoutubeId = a.TrailerYoutubeId
            });
        }
    }
}
