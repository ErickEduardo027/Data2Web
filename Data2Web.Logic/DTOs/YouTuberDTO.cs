using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.DTOs
{
    public class YouTuberDTO
    {
        public int YouTuberId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UrlCanal { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string LogoPath { get; set; } = string.Empty; // ruta local de la imagen
    }
}
