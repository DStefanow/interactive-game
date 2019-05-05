namespace InteractiveGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("game_user")]
    public partial class GameUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GameUser(string username, string password, string fullName)
        {
            this.Username = username;
            this.Password = password;
            this.FullName = fullName;

            UserScore = new HashSet<UserScore>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique =true)]
        [StringLength(16)]
        public string Username { get; set; }

        [Required]
        [StringLength(256)]
        public string Password { get; set; }

        [Required]
        [Column("full_name")]
        [StringLength(64)]
        public string FullName { get; set; }

        [Column("is_admin")]
        public bool? IsAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserScore> UserScore { get; set; }
    }
}
