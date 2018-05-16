using System.ComponentModel.DataAnnotations.Schema;

namespace CSGO.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string SteamProfile { get; set; }

        public int TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; }
    }
}
