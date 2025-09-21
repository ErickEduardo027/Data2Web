using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.DTOs
{
    public class GenealogiaDto
    {
        public int GenealogiaId { get; set; }
        public int PersonaId { get; set; }
        public int ParentescoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string FotoUrl { get; set; } = string.Empty;
    }
}
