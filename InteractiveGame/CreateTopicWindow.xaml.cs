using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InteractiveGame
{
    public partial class CreateTopicWindow : Window
    {
        public CreateTopicWindow()
        {
            InitializeComponent();
        }

        public void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateToAdminPanel();
        }

        public void InsertButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateToAdminPanel();
        }

        public void NavigateToAdminPanel()
        {
            AdminWindow admWindow = new AdminWindow();
            this.Close();
            admWindow.Show();
        }
    }
}
