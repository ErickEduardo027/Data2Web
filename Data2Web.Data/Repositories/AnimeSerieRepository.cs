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

    public class AnimeSerieRepository : IAnimeSerieRepository
    {
        private readonly IDbConnectionFactory _factory;

        public AnimeSerieRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<AnimeSerie>> GetByPersonaIdAsync(int personaId)
        {
            using var conn = _factory.Create();
            string sql = @"
                SELECT A.AnimeSerieId, A.Titulo, A.Descripcion, A.CaratulaUrl, A.TrailerYoutubeId
                FROM AnimeSeries A
                INNER JOIN PersonaAnimeSerie PA ON A.AnimeSerieId = PA.AnimeSerieId
                WHERE PA.PersonaId = @personaId";
            return await conn.QueryAsync<AnimeSerie>(sql, new { personaId });
        }

        public async Task<IEnumerable<AnimeSerie>> GetAllAsync()
        {
            using var conn = _factory.Create();
            string sql = @"SELECT AnimeSerieId, Titulo, Descripcion, CaratulaUrl, TrailerYoutubeId
                           FROM AnimeSeries";
            return await conn.QueryAsync<AnimeSerie>(sql);
        }
    }
}
