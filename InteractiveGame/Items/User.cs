using System;

namespace InteractiveGame.Items
{
    class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
