using Dapper;
using Data2Web.Data.Models;
using Data2Web.Data.Context;
using System.Data;

namespace Data2Web.Data.Repositories
{

    public interface IPersonaRepository
    {
        Task<Persona?> GetPrincipalAsync();
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<Persona?> GetByIdAsync(int id);
    }
  

    public class PersonaRepository : IPersonaRepository
    {
        private readonly IDbConnectionFactory _factory;

        public PersonaRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<Persona?> GetPrincipalAsync()
        {
            using var conn = _factory.Create();
            string sql = @"SELECT TOP 1 PersonaId, Nombres, Apellidos, FechaNacimiento, EsPrincipal
                           FROM Personas
                           WHERE EsPrincipal = 1";
            return await conn.QueryFirstOrDefaultAsync<Persona>(sql);
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            using var conn = _factory.Create();
            string sql = @"SELECT PersonaId, Nombres, Apellidos, FechaNacimiento, EsPrincipal
                           FROM Personas";
            return await conn.QueryAsync<Persona>(sql);
        }

        public async Task<Persona?> GetByIdAsync(int id)
        {
            using var conn = _factory.Create();
            string sql = @"SELECT PersonaId, Nombres, Apellidos, FechaNacimiento, EsPrincipal
                           FROM Personas
                           WHERE PersonaId = @id";
            return await conn.QueryFirstOrDefaultAsync<Persona>(sql, new { id });
        }
    }
}



