using Data2Web.Data.Models;

namespace Data2Web.Data.Repositories.Interfaces
{
    public interface ISocialLinksRepository
    {
        Task<IEnumerable<SocialLink>> GetByPersonaIdAsync(int personaId);
    }
}
