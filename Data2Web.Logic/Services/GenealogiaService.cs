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
    public class GenealogiaService : IGenealogiaService
    {
        private readonly IGenealogiaRepository _genealogiaRepo;

        public GenealogiaService(IGenealogiaRepository genealogiaRepo)
        {
            _genealogiaRepo = genealogiaRepo;
        }

        public async Task<IEnumerable<GenealogiaDTO>> GetByPersonaIdAsync(int personaId)
        {
            var entities = await _genealogiaRepo.GetByPersonaIdAsync(personaId);

            return entities.Select(g => new GenealogiaDTO
            {
                GenealogiaId = g.GenealogiaId,
                PersonaId = g.PersonaId,
                ParentescoId = g.ParentescoId,
                Nombre = g.Nombre,
                FotoUrl = g.FotoUrl
            });
        }
    }
}
