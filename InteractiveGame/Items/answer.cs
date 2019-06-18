namespace InteractiveGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;

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

        public Answer() { }

        public static List<Answer> GetAnswersForQuestionRnd(int questionId)
        {
            return App.DbManager.Answer.SqlQuery("SELECT a.id, a.question_id AS QuestionId, a.description, a.is_true as isTrue" +
                    " FROM answer a WHERE question_id = @question_id " +
                    " ORDER BY NEWID()", new SqlParameter("@question_id", questionId))
                    .ToList();
        }

        public static List<Answer> GetAnswersForQuestion(Question question)
        {
            return App.DbManager.Answer.Where(x => x.QuestionId == question.Id).ToList();
        }

        public static Dictionary<Question, List<Answer>> GetQuestionsWithAnswers(Topic topic)
        {
            Dictionary<Question, List<Answer>> questionsWithAnswers = new Dictionary<Question, List<Answer>>();

            Question[] questions = Question.GetQuestionsForTopic(topic);

            for (int i = 0; i < 4; i++)
            {
                questionsWithAnswers.Add(questions[i], GetAnswersForQuestion(questions[i]));
            }

            return questionsWithAnswers;
        }
    }
}
