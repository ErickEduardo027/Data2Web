using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Data.Models
{
    public class TimelineEvento
    {
        public int EventoId { get; set; }
        public int PersonaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        // 🔹 Relación con las imágenes
        public List<string> Imagenes { get; set; } = new();
    }
}

