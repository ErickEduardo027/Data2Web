using Data2Web.Data.Models;

namespace Data2Web.Data.Repositories.Interfaces
{
    public interface IYouTuberRepository
    {
        Task<IEnumerable<YouTuber>> GetAllAsync();
        Task<YouTuber?> GetByIdAsync(int id);
    }
}
