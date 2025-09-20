using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Data.Models
{
    public class Genealogia
    {
        public int GenealogiaId { get; set; }
        public int PersonaId { get; set; }
        public int ParentescoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }

        // Para mostrar el nombre del parentesco (ej. "Abuelo Paterno")
        public string Parentesco { get; set; } = string.Empty;
    }
}
