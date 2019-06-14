namespace InteractiveGame
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

    public partial class Items : DbContext
    {
        public Items()
            : base("name=Items")
        {
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<GameUser> GameUser { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<UserScore> UserScore { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.UserScore)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Topic)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GameUser>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<GameUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<GameUser>()
                .HasMany(e => e.UserScore)
                .WithRequired(e => e.GameUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answer)
                .WithRequired(e => e.Question)
                .HasForeignKey(e => e.QuestionId);

            modelBuilder.Entity<Topic>()
                .HasMany(e => e.Question)
                .WithRequired(e => e.Topic)
                .HasForeignKey(e => e.TopicId)
                .WillCascadeOnDelete(false);
        }

        public static bool SaveChangesUniqueHandler()
        {
            try
            {
                App.DbManager.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.InnerException is SqlException sqlEx &&
                    (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    App.DbManager = new Items();
                    return false;
                }
            }

            return true;
        }

        public static Answer[] GetAnswersForQuestion(int questionId)
        {
            return App.DbManager.Answer.SqlQuery("SELECT a.id, a.question_id AS QuestionId, a.description, a.is_true as isTrue" +
                    " FROM answer a WHERE question_id = @question_id " +
                    " ORDER BY NEWID()", new SqlParameter("@question_id", questionId))
                    .ToArray();
        }

        public static Question[] GetQuestionsForTopic(Topic topic)
        {
            return App.DbManager.Question.Where(x => x.TopicId == topic.Id).OrderBy(x => x.Id).ToArray();
        }

        //public Dictionary<Question, List<Answer>> GetQuestionsWithAnswers(Topic topic)
        //{

        //}
    }
}
