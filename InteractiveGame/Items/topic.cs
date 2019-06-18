namespace InteractiveGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("topic")]
    public partial class Topic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Topic()
        {
            Question = new HashSet<Question>();
        }

        public Topic(int categoryId, string title, string description)
        {
            this.CategoryId = categoryId;
            this.Category = App.DbManager.Category.First(c => c.Id == categoryId);
            this.Title = title;
            this.Description = description;

            Question = new HashSet<Question>();
        }

        public int Id { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(64)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Description { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Question { get; set; }

        public static List<Topic> GetAllTopicsForGivenCategory(int currentCategoryId)
        {
            return App.DbManager.Topic.Where(x => x.CategoryId == currentCategoryId).ToList();
        }
    }
}
