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
using System.Windows.Shapes;

namespace InteractiveGame
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BackClickEvent(object sender, RoutedEventArgs e)
        {
            LoginWindow logWindow = new LoginWindow();
            this.Close();
            logWindow.Show();
        }

        private void RegisterClickEvent(object sender, RoutedEventArgs e)
        {

        }
    }
}
