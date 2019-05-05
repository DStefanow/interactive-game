using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InteractiveGame
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            PopulateUserBox();
        }

        public void PopulateUserBox()
        {
            UserBox.Items.Clear();

            List<string> usernames = GetAllUsernames();

            UserBox.SelectedIndex = 0;
            foreach (string username in usernames)
            {
                UserBox.Items.Add(new ComboBoxItem { Content = username });
            }
        }

        public void DeleteUserClick(object sender, RoutedEventArgs e)
        {
            DeleteByUsername(((ComboBoxItem)UserBox.SelectedItem).Content.ToString());
            PopulateUserBox();
        }

        public List<string> GetAllUsernames()
        {
            return App.DbManager.GameUser.Where(u => u.IsAdmin == null || u.IsAdmin == false).Select(u => u.Username).ToList();
        }

        public void DeleteByUsername(string username)
        {
            GameUser user = App.DbManager.GameUser.FirstOrDefault(u => u.Username == username);
            App.DbManager.GameUser.Remove(user);
            App.DbManager.SaveChanges();
        }
    }
}
