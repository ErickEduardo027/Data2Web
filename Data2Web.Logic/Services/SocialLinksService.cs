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
    public class SocialLinksService : ISocialLinksService
    {
        private readonly ISocialLinksRepository _socialRepo;

        public SocialLinksService(ISocialLinksRepository socialRepo)
        {
            _socialRepo = socialRepo;
        }

        public async Task<IEnumerable<SocialLinkDTO>> GetByPersonaIdAsync(int personaId)
        {
            var entities = await _socialRepo.GetByPersonaIdAsync(personaId);

            return entities.Select(s => new SocialLinkDTO
            {
                SocialLinkId = s.SocialLinkId,
                PersonaId = s.PersonaId,
                Plataforma = s.RedSocial,
                Url = s.Url,
                
            });
        }
    }
}
