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
    public interface IPasatiempoRepository
    {
        Task<IEnumerable<Pasatiempo>> GetByPersonaIdAsync(int personaId);
        Task<IEnumerable<Pasatiempo>> GetAllAsync();
    }

    public class PasatiempoRepository : IPasatiempoRepository
    {
        private readonly IDbConnectionFactory _factory;

        public PasatiempoRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Pasatiempo>> GetByPersonaIdAsync(int personaId)
        {
            using var conn = _factory.Create();
            string sql = @"
                SELECT P.PasatiempoId, P.Titulo, P.Descripcion, P.ImagenUrl
                FROM Pasatiempos P
                INNER JOIN PersonaPasatiempo PP ON P.PasatiempoId = PP.PasatiempoId
                WHERE PP.PersonaId = @personaId";
            return await conn.QueryAsync<Pasatiempo>(sql, new { personaId });
        }

        public async Task<IEnumerable<Pasatiempo>> GetAllAsync()
        {
            using var conn = _factory.Create();
            string sql = @"SELECT PasatiempoId, Titulo, Descripcion, ImagenUrl
                           FROM Pasatiempos";
            return await conn.QueryAsync<Pasatiempo>(sql);
        }
    }
}
