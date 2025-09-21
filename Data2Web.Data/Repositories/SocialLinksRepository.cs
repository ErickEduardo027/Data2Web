using Dapper;
using Data2Web.Data.Context;
using Data2Web.Data.Models;
using Data2Web.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Data.Repositories
{

    public class SocialLinksRepository : ISocialLinksRepository
    {
        private readonly IDbConnectionFactory _factory;

        public SocialLinksRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<SocialLink>> GetByPersonaIdAsync(int personaId)
        {
            using var conn = _factory.Create();
            string sql = @"
                SELECT SocialLinkId, PersonaId, RedSocial, Url
                FROM SocialLinks
                WHERE PersonaId = @personaId";
            return await conn.QueryAsync<SocialLink>(sql, new { personaId });
        }
    }
}
