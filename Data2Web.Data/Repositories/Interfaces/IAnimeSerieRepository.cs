using Data2Web.Data.Models;

namespace Data2Web.Data.Repositories.Interfaces
{
    public interface IAnimeSerieRepository
    {
        Task<IEnumerable<AnimeSerie>> GetByPersonaIdAsync(int personaId);
        Task<IEnumerable<AnimeSerie>> GetAllAsync();
    }
}
