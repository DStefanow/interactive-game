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

        public void InsertCategoryClick(object sender, RoutedEventArgs e)
        {
            CreateCategoryWindow createCategoryWindow = new CreateCategoryWindow();
            this.Close();
            createCategoryWindow.Show();
        }

        public void InsertTopicClick(object sender, RoutedEventArgs e)
        {
            CreateTopicWindow createTopicWindow = new CreateTopicWindow();
            this.Close();
            createTopicWindow.Show();
        }

        public void InsertQAClick(object sender, RoutedEventArgs e)
        {
            CreateQAWindow createQaWindow = new CreateQAWindow();
            this.Close();
            createQaWindow.Show();
        }

        public void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            LoginWindow logWindow = new LoginWindow();
            this.Close();
            logWindow.Show();
        }

        public void DeleteUserClick(object sender, RoutedEventArgs e)
        {
            DeleteByUsername(((ComboBoxItem)UserBox.SelectedItem).Content.ToString());
            PopulateUserBox();
        }

        public List<string> GetAllUsernames()
        {
            return App.DbManager.GameUser.Where(u => u.IsAdmin == null || u.IsAdmin == false).
                Select(u => u.Username).
                ToList();
        }

        public void DeleteByUsername(string username)
        {
            GameUser user = App.DbManager.GameUser.FirstOrDefault(u => u.Username == username);
            App.DbManager.GameUser.Remove(user);
            App.DbManager.SaveChanges();
        }
    }
}
