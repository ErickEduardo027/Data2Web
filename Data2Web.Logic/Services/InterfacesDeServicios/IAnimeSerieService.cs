using Data2Web.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.Services.InterfacesDeServicios
{
    public interface IAnimeSerieService
    {
        Task<IEnumerable<AnimeSerieDTO>> GetByPersonaIdAsync(int personaId);
    }
}
