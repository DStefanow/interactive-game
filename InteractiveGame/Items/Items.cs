namespace InteractiveGame
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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
                .HasForeignKey(e => e.QuestionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Topic>()
                .HasMany(e => e.Question)
                .WithRequired(e => e.Topic)
                .HasForeignKey(e => e.TopicId)
                .WillCascadeOnDelete(false);
        }
    }
}
