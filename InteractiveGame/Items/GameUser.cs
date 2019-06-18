namespace InteractiveGame
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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

        public GameUser(){ }

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

        public static GameUser GetUser(string username, string password)
        {
            try
            {
                return App.DbManager.GameUser.FirstOrDefault(u => u.Username == username);
            }
            catch (System.Data.Common.DbException)
            {
                return null;
            }
        }

        public static GameUser GetUserById(int id)
        {
            return App.DbManager.GameUser.FirstOrDefault(u => u.Id == id);
        }

        public static List<string> GetAllUsernames()
        {
            return App.DbManager.GameUser.Where(u => u.IsAdmin == null || u.IsAdmin == false).
                Select(u => u.Username).
                ToList();
        }

        public static void DeleteByUsername(string username)
        {
            GameUser user = App.DbManager.GameUser.FirstOrDefault(u => u.Username == username);
            App.DbManager.GameUser.Remove(user);
            App.DbManager.SaveChanges();
        }
    }
}
