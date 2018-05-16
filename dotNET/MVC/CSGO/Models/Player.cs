using CSGO.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace CSGO.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        [Required, StringLength(50, MinimumLength = 3), RegularExpression(@"^[A-Z]+[a-zA-Ząęźżółć''-'\s]*$")]
        public string Name { get; set; }
        [Required, StringLength(50, MinimumLength = 3), RegularExpression(@"^[A-Z]+[a-zA-Ząęźżółć''-'\s]*$")]
        public string Surname { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        [NicknameCheck, No666Check]
        public string Nickname { get; set; }
        [Range(0, int.MaxValue)]
        public int Salary { get; set; }
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [BornCheck]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
