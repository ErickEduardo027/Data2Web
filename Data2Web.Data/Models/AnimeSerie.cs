using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Data.Models
{
    public class AnimeSerie
    {
        public int AnimeSerieId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? CaratulaUrl { get; set; }
        public string? TrailerYoutubeId { get; set; }
    }
}
