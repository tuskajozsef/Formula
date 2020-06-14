using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Formula1.Models
{

    [Table("Teams")]
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Team name")]
        public string Name { get; set; }

        [Required]
        [Range(1800, 2020)]
        [DisplayName("Year of foundation")]
        public int FoundedIn { get; set; }

        [Required]
        [DisplayName("Number of titles")]
        public int ChampionshipsWon { get; set; }

        [DisplayName("Paid")]
        public bool CheckPaid { get; set; }

    }
}
