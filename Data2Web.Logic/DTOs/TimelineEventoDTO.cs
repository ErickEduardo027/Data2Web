using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.DTOs
{
    public class TimelineEventoDTO
    {
        public int TimelineEventoId { get; set; }
        public int PersonaId { get; set; }
        public string Fecha { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        // 🔹 Lista de imágenes asociadas al evento
        public List<string> Imagenes { get; set; } = new();
    }
}
