namespace InteractiveGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("answer")]
    public partial class answer
    {
        public int id { get; set; }

        public int question_id { get; set; }

        [Required]
        [StringLength(256)]
        public string description { get; set; }

        public int points { get; set; }

        public bool? is_true { get; set; }

        public virtual question question { get; set; }
    }
}
