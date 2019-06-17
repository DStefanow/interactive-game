namespace InteractiveGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

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

        public UserScore(GameUser user, Topic topic, ushort points)
        {
            this.GameUser = user;
            this.Category = topic.Category;
            this.Score = (int)points;
        }

        public UserScore() { }

        public static void UpdateUserScore(GameUser user, Topic topic, ushort points)
        {
            List<UserScore> allUserScores = App.DbManager.UserScore.Where(u => u.UserId == user.Id).ToList();
            List<Category> categoriesForUser = allUserScores.Select(c => c.Category).ToList();

            bool isUpdate = false;
            foreach (Category cat in categoriesForUser)
            {
                if (cat.Id == topic.CategoryId)
                {
                    UserScore userScore = allUserScores.First(c => c.CategoryId == topic.CategoryId);
                    userScore.Score += points;
                    isUpdate = true;
                }
            }

            // First result for that user and that category
            if (!isUpdate)
            {
                UserScore userResult = new UserScore(user, topic, points);
                App.DbManager.UserScore.Add(userResult);
            }

            App.DbManager.SaveChanges();
        }
    }
}
