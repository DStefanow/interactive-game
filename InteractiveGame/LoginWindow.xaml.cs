using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InteractiveGame.Items;

namespace InteractiveGame
{
    public partial class LoginWindow : Window
    {
        // TODO: Temporary list for register users
        List<User> users = new List<User>();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginClickEvent(object sender, RoutedEventArgs e)
        {
            // TODO ...
        }

        private void RegisterClickEvent(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWindow = new RegisterWindow();
            this.Close();
            regWindow.Show();
        }
    }
}
