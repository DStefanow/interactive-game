namespace InteractiveGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class game_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public game_user()
        {
            user_score = new HashSet<user_score>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(16)]
        public string username { get; set; }

        [Required]
        [StringLength(256)]
        public string password { get; set; }

        [Required]
        [StringLength(64)]
        public string full_name { get; set; }

        public bool? is_admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_score> user_score { get; set; }
    }
}
