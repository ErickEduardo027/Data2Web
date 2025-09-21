using Data2Web.Data.Models;

namespace Data2Web.Data.Repositories.Interfaces
{
    public interface ITimelineRepository
    {
        Task<IEnumerable<TimelineEvento>> GetByPersonaIdAsync(int personaId);
    }
}
