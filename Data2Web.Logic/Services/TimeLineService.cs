using Data2Web.Data.Models;
using Data2Web.Data.Repositories.Interfaces;
using Data2Web.Logic.DTOs;
using Data2Web.Logic.Services.InterfacesDeServicios;
using System.Collections.Generic;
using System.Linq;
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

        // 🔹 Devuelve DTOs ya con imágenes incluidas
        public async Task<IEnumerable<TimelineEventoDTO>> GetByPersonaIdAsync(int personaId)
        {
            var eventos = await _timelineRepo.GetByPersonaIdAsync(personaId);

            var lista = new List<TimelineEventoDTO>();

            foreach (var evento in eventos)
            {
                // Traemos las imágenes desde el repo
                var imagenes = await _timelineRepo.GetImagenesByEventoIdAsync(evento.EventoId);

                lista.Add(new TimelineEventoDTO
                {
                    TimelineEventoId = evento.EventoId,
                    PersonaId = evento.PersonaId,
                    Fecha = evento.Fecha,
                    Titulo = evento.Titulo,
                    Descripcion = evento.Descripcion,
                    Imagenes = imagenes.ToList()
                });
            }

            return lista;
        }
    }
}

