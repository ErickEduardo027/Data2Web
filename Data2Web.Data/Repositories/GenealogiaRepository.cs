using Data2Web.Data.Context;
using Data2Web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Data2Web.Data.Repositories
{
    public interface IGenealogiaRepository
    {
        Task<IEnumerable<Genealogia>> GetByPersonaIdAsync(int personaId);
    }

    public class GenealogiaRepository : IGenealogiaRepository
    {
        private readonly IDbConnectionFactory _factory;

        public GenealogiaRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Genealogia>> GetByPersonaIdAsync(int personaId)
        {
            using var conn = _factory.Create();
            string sql = @"
                SELECT G.GenealogiaId,
                       G.PersonaId,
                       G.ParentescoId,
                       G.Nombre,
                       G.FotoUrl,
                       P.Nombre AS Parentesco
                FROM Genealogia G
                INNER JOIN Parentescos P ON G.ParentescoId = P.ParentescoId
                WHERE G.PersonaId = @personaId";

            return await conn.QueryAsync<Genealogia>(sql, new { personaId });
        }
    }
}
