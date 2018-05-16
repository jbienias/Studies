using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSGO.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
