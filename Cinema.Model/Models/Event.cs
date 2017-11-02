using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Model.Models
{
    [Table("Event")]
    public class Event
    {
        public const int MAX_LENGTH_NAME = 100;
        public const int MAX_LENGTH_TIME = 50;
        public const int MAX_LENGTH_LINK_IMAGE = 200;
        public const int MAX_LENGTH_INTRO = int.MaxValue;

        public const string FOREIGNKEY_CINEMA_CHAIN = "CinemaChainID";
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { set; get; }
        
        [Required]
        [MaxLength(MAX_LENGTH_NAME)]
        public string Name { set; get; }

        [MaxLength(MAX_LENGTH_TIME)]
        public string Time { set; get; }

        public DateTime? EndTime { set; get; }


        [MaxLength(MAX_LENGTH_LINK_IMAGE)]
        public string LinkImage { set; get; }
        
        [MaxLength(MAX_LENGTH_INTRO)]
        public string Intro { set; get; }

        [Required]
        [MaxLength(CinemaChain.MAX_LENGTH_ID)]
        [Column(FOREIGNKEY_CINEMA_CHAIN)]
        public string CinemaChainID { set; get; }

        [ForeignKey(FOREIGNKEY_CINEMA_CHAIN)]
        public virtual CinemaChain CinemaChain { set; get; }
    }
}