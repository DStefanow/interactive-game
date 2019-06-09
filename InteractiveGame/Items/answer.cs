namespace InteractiveGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("answer")]
    public partial class Answer
    {
        public int Id { get; set; }

        [Column("question_id")]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Column("is_true")]
        public bool? IsTrue { get; set; }

        public virtual Question Question { get; set; }

        public Answer(int questionId, string description, bool isTrue)
        {
            this.QuestionId = questionId;
            this.Description = description;
            this.IsTrue = isTrue;
        }
    }
}
