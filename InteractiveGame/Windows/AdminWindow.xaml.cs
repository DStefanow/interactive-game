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

            List<string> usernames = GameUser.GetAllUsernames();

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
            return;
        }

        public void InsertTopicClick(object sender, RoutedEventArgs e)
        {
            CreateTopicWindow createTopicWindow = new CreateTopicWindow();
            this.Close();
            createTopicWindow.Show();
            return;
        }

        public void InsertQAClick(object sender, RoutedEventArgs e)
        {
            CreateQAWindow createQaWindow = new CreateQAWindow();
            this.Close();
            createQaWindow.Show();
            return;
        }

        public void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            App.SwitchToWindow(this, "login");
            return;
        }

        public void DeleteUserClick(object sender, RoutedEventArgs e)
        {
            GameUser.DeleteByUsername(((ComboBoxItem)UserBox.SelectedItem).Content.ToString());
            PopulateUserBox();
        }
    }
}
