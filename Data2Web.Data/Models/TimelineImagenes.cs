using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Data.Models
{
    public class TimelineImagen
    {
        public int ImagenId { get; set; }      // PK
        public int EventoId { get; set; }       // FK a TimelineEvento
        public string Url { get; set; } = string.Empty; // Ruta/URL de la imagen
    }
}
