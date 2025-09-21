using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.DTOs
{
    public class AnimeSerieDTO
    {
        public int AnimeSerieId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string CaratulaUrl { get; set; } = string.Empty;
        public string TrailerYoutubeId { get; set; } = string.Empty;
    }
}
