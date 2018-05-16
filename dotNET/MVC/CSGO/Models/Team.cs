using CSGO.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSGO.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        [Display(Name = "Team name")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        //dziala 🡣
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [DataType(DataType.Date)]
        [DateCheck]
        [Display(Name = "Date of founding")]
        public DateTime DateOfFounding { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}

