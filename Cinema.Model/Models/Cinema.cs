using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Model.Models
{
    [Table("Cinema")]
    public class Cinema
    {
        public const int MAX_LENGTH_ID = 20;
        public const int MAX_LENGTH_NAME = 50;
        public const int MAX_LENGTH_LINK_IMAGE = 200;
        public const int MAX_LENGTH_PHONE_NUMBER = 30;
        public const int MAX_LENGTH_ADDRESS = 300;
        public const int MAX_LENGTH_INTRO = 4000;

        public const string FOREIGNKEY_LOCATION = "LocationID";
        public const string FOREIGNKEY_CINEMACHAIN = "CinemaChainID";

        [Key]
        [MaxLength(MAX_LENGTH_ID)]
        public string ID { set; get; }

        [Required]
        [MaxLength(MAX_LENGTH_NAME)]
        public string Name { set; get; }

        [MaxLength(MAX_LENGTH_LINK_IMAGE)]
        public string LinkImage { set; get; }

        [MaxLength(MAX_LENGTH_PHONE_NUMBER)]
        public string PhoneNumber { set; get; }

        [MaxLength(MAX_LENGTH_ADDRESS)]
        public string Address { set; get; }

        [MaxLength(MAX_LENGTH_INTRO)]
        public string Intro { set; get; }

        public decimal? Longitude { set; get; }

        public decimal? Latitude { set; get; }

        [Required]
        [MaxLength(Location.MAX_LENGTH_ID)]
        [Column(FOREIGNKEY_LOCATION)]
        public string LocationID { set; get; }

        [Required]
        [MaxLength(CinemaChain.MAX_LENGTH_ID)]
        [Column(FOREIGNKEY_CINEMACHAIN)]
        public string CinemaChainID { set; get; }

        [ForeignKey(FOREIGNKEY_LOCATION)]
        public virtual Location Location { set; get; }

        [ForeignKey(FOREIGNKEY_CINEMACHAIN)]
        public virtual CinemaChain GetCinemaChain { set; get; }

        public virtual IEnumerable<Schedule> Schedules { set; get; }
    }
}