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

        public virtual DbSet<answer> answer { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<game_user> game_user { get; set; }
        public virtual DbSet<question> question { get; set; }
        public virtual DbSet<topic> topic { get; set; }
        public virtual DbSet<user_score> user_score { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<category>()
                .HasMany(e => e.user_score)
                .WithRequired(e => e.category)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<category>()
                .HasMany(e => e.topic)
                .WithRequired(e => e.category)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<game_user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<game_user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<game_user>()
                .HasMany(e => e.user_score)
                .WithRequired(e => e.game_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<question>()
                .HasMany(e => e.answer)
                .WithRequired(e => e.question)
                .HasForeignKey(e => e.question_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<topic>()
                .HasMany(e => e.question)
                .WithRequired(e => e.topic)
                .HasForeignKey(e => e.topic_id)
                .WillCascadeOnDelete(false);
        }
    }
}
