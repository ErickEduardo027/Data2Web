using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.DTOs
{
    public class SocialLinkDTO
    {
        public int SocialLinkId { get; set; }
        public int PersonaId { get; set; }
        public string Plataforma { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
