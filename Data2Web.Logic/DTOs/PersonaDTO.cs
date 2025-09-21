using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data2Web.Data.Models;
namespace Data2Web.Logic.DTOs
{
    public class PersonaDTO
    {
        public Persona? Datos { get; set; }
        public IEnumerable<Genealogia> Genealogia { get; set; } = new List<Genealogia>();
        public IEnumerable<TimelineEvento> Timeline { get; set; } = new List<TimelineEvento>();
        public IEnumerable<SocialLink> RedesSociales { get; set; } = new List<SocialLink>();
    }
}
