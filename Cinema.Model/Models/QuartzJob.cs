using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Model.Models
{
    [Table("QuartzJob")]
    public class QuartzJob
    {
        public const int MAX_LENGTH_ACTION = 200;
        public const int MAX_LENGTH_NAME = 200;
        public const int MAX_LENGTH_STACK_TRACE = 500;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(MAX_LENGTH_ACTION)]
        public string Action { set; get; }

        [MaxLength(MAX_LENGTH_NAME)]
        public string Name { set; get; }

        public virtual IEnumerable<QuartzSchedule> Schedules { set; get; }
    }
}