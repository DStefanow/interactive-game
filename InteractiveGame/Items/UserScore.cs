namespace InteractiveGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user_score")]
    public partial class UserScore
    {
        [Key]
        [Column("user_id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? UserId { get; set; }

        [Key]
        [Column("category_id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? CategoryId { get; set; }

        [Column("score")]
        public int? Score { get; set; }

        public virtual Category Category { get; set; }

        public virtual GameUser GameUser { get; set; }
    }
}
