using Data2Web.Data.Context;
using Data2Web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Data2Web.Data.Repositories.Interfaces;

namespace Data2Web.Data.Repositories
{
    public class YouTuberRepository : IYouTuberRepository
    {
        private readonly IDbConnectionFactory _factory;

        public YouTuberRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<YouTuber>> GetAllAsync()
        {
            using var conn = _factory.Create();
            string sql = @"SELECT YouTuberId, Nombre, Descripcion, UrlCanal, LogoUrl
                           FROM YouTubers";
            return await conn.QueryAsync<YouTuber>(sql);
        }

        public async Task<YouTuber?> GetByIdAsync(int id)
        {
            using var conn = _factory.Create();
            string sql = @"SELECT YouTuberId, Nombre, Descripcion, UrlCanal, LogoUrl
                           FROM YouTubers
                           WHERE YouTuberId = @id";
            return await conn.QueryFirstOrDefaultAsync<YouTuber>(sql, new { id });
        }
    }
}
