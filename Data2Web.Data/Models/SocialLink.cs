using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Data.Models
{
    public class SocialLink
    {
        public int SocialLinkId { get; set; }
        public int PersonaId { get; set; }
        public string IconoSvg { get; set; } = string.Empty; 
        public string RedSocial {  get; set; }
        public string Url { get; set; } = string.Empty;
       
    }
}
