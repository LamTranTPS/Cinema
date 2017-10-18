using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Model.Models
{
    [Table("Error")]
    public class Error
    {
        public const int MAX_LENGTH_MESSAGE = 200;
        public const int MAX_LENGTH_STACK_TRACE = 500;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(MAX_LENGTH_MESSAGE)]
        public string Message { set; get; }

        [MaxLength(MAX_LENGTH_STACK_TRACE)]
        public string StackTrace { set; get; }

        public DateTime? CreatedDate { set; get; }
    }
}