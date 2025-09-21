using Data2Web.Data.Models;

namespace Data2Web.Data.Repositories.Interfaces
{
    public interface IPersonaRepository
    {
        Task<Persona?> GetPrincipalAsync();
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<Persona?> GetByIdAsync(int id);
    }
}



