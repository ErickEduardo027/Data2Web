using Data2Web.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data2Web.Data.Repositories.Interfaces
{
    public interface ITimelineRepository
    {
        Task<IEnumerable<TimelineEvento>> GetByPersonaIdAsync(int personaId);

        // 🔹 Nuevo método para traer imágenes de un evento
        Task<IEnumerable<string>> GetImagenesByEventoIdAsync(int eventoId);
    }
}

