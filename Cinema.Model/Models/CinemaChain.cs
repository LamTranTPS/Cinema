using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Model.Models
{
    [Table("CinemaChain")]
    public class CinemaChain
    {
        public const int MAX_LENGTH_ID = 20;
        public const int MAX_LENGTH_NAME = 50;

        [Key]
        [MaxLength(MAX_LENGTH_ID)]
        public string ID { set; get; }

        [Required]
        [MaxLength(MAX_LENGTH_NAME)]
        public string Name { set; get; }


        public virtual IEnumerable<Cinema> Cinemas { set; get; }
    }
}