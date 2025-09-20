using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Data.Models
{
    public class YouTuber
    {
        public int YouTuberId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string CanalUrl { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
    }
}
