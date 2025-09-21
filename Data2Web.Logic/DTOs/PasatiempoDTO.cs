using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.DTOs
{
    public class PasatiempoDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string ImagenLocalPath { get; set; } = string.Empty; // ruta en /assets/img/pasatiempos/
    }
}
