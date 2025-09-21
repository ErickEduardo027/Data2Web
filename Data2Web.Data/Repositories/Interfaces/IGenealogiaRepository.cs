using Data2Web.Data.Models;

namespace Data2Web.Data.Repositories.Interfaces
{
    public interface IGenealogiaRepository
    {
        Task<IEnumerable<Genealogia>> GetByPersonaIdAsync(int personaId);
    }
}
