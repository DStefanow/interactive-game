namespace InteractiveGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("question")]
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Answer = new HashSet<Answer>();
        }

        public Question(int topicId, string description, int points)
        {
            this.TopicId = topicId;
            this.Description = description;
            this.Points = points;
        }

        public int Id { get; set; }

        [Column("topic_id")]
        public int TopicId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answer { get; set; }

        public virtual Topic Topic { get; set; }

        public int Points { get; set; }

        public static void DeleteAllQuestionsByTopicId(int topicId)
        {
            App.DbManager.Question.RemoveRange(
                App.DbManager.Question.Where(
                    x => x.TopicId == topicId
                )
            );
        }

        public static bool HasQuestionsForTopic(Topic topic)
        {
            return App.DbManager.Question.Count(x => x.TopicId == topic.Id) == 4;
        }
    }
}
