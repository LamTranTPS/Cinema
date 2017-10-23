using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Model.Models
{
    [Table("Film")]
    public class Film
    {
        public const int MAX_LENGTH_NAME = 100;
        public const int MAX_LENGTH_TIME = 50;
        public const int MAX_LENGTH_GENRE = 50;
        public const int MAX_LENGTH_LINK_TRAILER = 200;
        public const int MAX_LENGTH_LINK_POSTER = 200;
        public const int MAX_LENGTH_LINK_IMAGE = 200;
        public const int MAX_LENGTH_INTRO = 4000;
        public const int MAX_LENGTH_ACTOR = 100;
        public const int MAX_LENGTH_DIRECTOR = 200;
        public const int MAX_LENGTH_COUNTRY = 50;
        public const int MAX_LENGTH_CLASSIFICATION = 20;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { set; get; }

        [Required]
        [MaxLength(MAX_LENGTH_NAME)]
        public string Name { set; get; }

        public DateTime? Premiere { set; get; }

        [MaxLength(MAX_LENGTH_TIME)]
        public string Time { set; get; }

        [MaxLength(MAX_LENGTH_GENRE)]
        public string Genre { set; get; }

        [MaxLength(MAX_LENGTH_LINK_TRAILER)]
        public string LinkTrailer { set; get; }

        [MaxLength(MAX_LENGTH_LINK_POSTER)]
        public string LinkPoster { set; get; }

        [MaxLength(MAX_LENGTH_LINK_IMAGE)]
        public string LinkImage { set; get; }

        [MaxLength(MAX_LENGTH_INTRO)]
        public string Intro { set; get; }

        [MaxLength(MAX_LENGTH_ACTOR)]
        public string Actor { set; get; }

        [MaxLength(MAX_LENGTH_DIRECTOR)]
        public string Director { set; get; }

        [MaxLength(MAX_LENGTH_COUNTRY)]
        public string Country { set; get; }

        [MaxLength(MAX_LENGTH_CLASSIFICATION)]
        public string Classification { set; get; }

        public decimal? IMDB { set; get; }

        public bool? IsHot { set; get; }

        public IEnumerable<Schedule> Schedules { set; get; }
    }
}