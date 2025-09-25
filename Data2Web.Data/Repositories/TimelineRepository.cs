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

    public class TimelineRepository : ITimelineRepository
    {
        private readonly IDbConnectionFactory _factory;

        public TimelineRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<TimelineEvento>> GetByPersonaIdAsync(int personaId)
        {
            using var conn = _factory.Create();
            string sql = @"
                SELECT EventoId, PersonaId, Fecha, Titulo, Descripcion
                FROM TimelineEventos
                WHERE PersonaId = @personaId
                ORDER BY Fecha ASC";
            return await conn.QueryAsync<TimelineEvento>(sql, new { personaId });
        }

        public async Task<IEnumerable<string>> GetImagenesByEventoIdAsync(int eventoId)
        {
            using var conn = _factory.Create(); // 👈 Abrir conexión
            var sql = "SELECT Url FROM TimelineImagenes WHERE EventoId = @EventoId";
            return await conn.QueryAsync<string>(sql, new { EventoId = eventoId });
        }

    }
}
