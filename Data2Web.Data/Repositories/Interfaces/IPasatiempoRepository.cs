using Data2Web.Data.Models;

namespace Data2Web.Data.Repositories.Interfaces
{
    public interface IPasatiempoRepository
    {
        Task<IEnumerable<Pasatiempo>> GetByPersonaIdAsync(int personaId);
        Task<IEnumerable<Pasatiempo>> GetAllAsync();
    }
}
