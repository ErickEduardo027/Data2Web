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
    public class TimelineService : ITimelineService
    {
        private readonly ITimelineRepository _timelineRepo;

        public TimelineService(ITimelineRepository timelineRepo)
        {
            _timelineRepo = timelineRepo;
        }

        public async Task<IEnumerable<TimelineEventoDTO>> GetByPersonaIdAsync(int personaId)
        {
            var entities = await _timelineRepo.GetByPersonaIdAsync(personaId);

            return entities.Select(t => new TimelineEventoDTO
            {
                TimelineEventoId = t.EventoId,
                PersonaId = t.PersonaId,
                Fecha = t.Fecha,
                Descripcion = t.Descripcion
            });
        }
    }
}
