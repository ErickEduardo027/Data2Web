using Data2Web.Data.Repositories;
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
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepo;
        private readonly IGenealogiaRepository _genealogiaRepo;
        private readonly ITimelineRepository _timelineRepo;
        private readonly ISocialLinksRepository _socialRepo;

        public PersonaService(
            IPersonaRepository personaRepo,
            IGenealogiaRepository genealogiaRepo,
            ITimelineRepository timelineRepo,
            ISocialLinksRepository socialRepo)
        {
            _personaRepo = personaRepo;
            _genealogiaRepo = genealogiaRepo;
            _timelineRepo = timelineRepo;
            _socialRepo = socialRepo;
        }

        public async Task<PersonaDTO?> GetPersonaPrincipalAsync()
        {
            var persona = await _personaRepo.GetPrincipalAsync();
            if (persona == null)
                return null;

            var genealogia = await _genealogiaRepo.GetByPersonaIdAsync(persona.PersonaId);
            var timeline = await _timelineRepo.GetByPersonaIdAsync(persona.PersonaId);
            var redes = await _socialRepo.GetByPersonaIdAsync(persona.PersonaId);

            return new PersonaDTO
            {
                Datos = persona,
                Genealogia = genealogia,
                Timeline = timeline,
                RedesSociales = redes
            };
        }
    }
}
