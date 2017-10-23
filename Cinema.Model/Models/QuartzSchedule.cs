using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Model.Models
{
    [Table("QuartzSchedule")]
    public class QuartzSchedule
    {
        public const int MAX_LENGTH_NAME = 200;
        public const int MAX_LENGTH_TIME = 30;
        public const string FOREIGNKEY_JOB = "JobID";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(MAX_LENGTH_NAME)]
        public string Name { set; get; }

        [MaxLength(MAX_LENGTH_TIME)]
        public string TimeExpression { set; get; }

        [Required]
        [Column(FOREIGNKEY_JOB)]
        public int JobID { set; get; }
        
        [ForeignKey(FOREIGNKEY_JOB)]
        public virtual QuartzJob Job { set; get; }
    }
}