using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Model.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        public const int MAX_LENGTH_ID = 30;
        public const int MAX_LENGTH_LINK_TICKET = 200;
        public const int MAX_LENGTH_TYPE = 20;

        public const string FOREIGNKEY_CINEMA = "CinemaID";
        public const string FOREIGNKEY_FILM = "FilmID";

        [Key]
        [MaxLength(MAX_LENGTH_ID)]
        public string ID { set; get; }

        [MaxLength(MAX_LENGTH_LINK_TICKET)]
        public string LinkTicket { set; get; }

        public DateTime DateTime { set; get; }

        [MaxLength(MAX_LENGTH_TYPE)]
        public string Type { set; get; }

        [Required]
        [Column(FOREIGNKEY_CINEMA)]
        public int CinemaID { set; get; }

        [Required]
        [Column(FOREIGNKEY_FILM)]
        public int FilmID { set; get; }

        [ForeignKey(FOREIGNKEY_CINEMA)]
        public virtual Cinema Cinema { set; get; }

        [ForeignKey(FOREIGNKEY_FILM)]
        public virtual Film Film { set; get; }
    }
}