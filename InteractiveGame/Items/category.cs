namespace InteractiveGame
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category(string categoryName)
        {
            this.CategoryName = categoryName;

            UserScore = new HashSet<UserScore>();
            Topic = new HashSet<Topic>();
        }

        public Category()
        {
            UserScore = new HashSet<UserScore>();
            Topic = new HashSet<Topic>();
        }

        public int Id { get; set; }

        [Required]
        [Column("category_name")]
        [Index(IsUnique = true)]
        [StringLength(64)]
        public string CategoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserScore> UserScore { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> Topic { get; set; }

        public static List<Category> GetAllCategories()
        {
            return App.DbManager.Category.ToList();
        }
        
        public static List<Category> GetAllCategoriesWithTopics()
        {
            return App.DbManager.Category.SqlQuery("SELECT c.id, c.category_name as CategoryName FROM category c " +
                    "JOIN topic t " +
                    "ON c.id = t.category_id " +
                    "GROUP BY c.id, c.category_name").
                    ToList();
        }
    }
}
