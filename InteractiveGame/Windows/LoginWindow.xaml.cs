using System;
using System.Collections.Generic;
using System.Data.Common;
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

namespace InteractiveGame
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginClickEvent(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            GameUser user = GetUser(username, password);

            if (!IsUserValid(user, password))
            {
                return;
            }

            bool isAdmin = user.IsAdmin ?? false;

            if (isAdmin)
            {
                AdminWindow admWindow = new AdminWindow();
                this.Close();
                admWindow.Show();
                return;
            }
            else
            {
                UserWindow userWindow = new UserWindow(user);
                this.Close();
                userWindow.Show();
                return;
            }
        }

        private void RegisterClickEvent(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWindow = new RegisterWindow();
            this.Close();
            regWindow.Show();
        }

        public bool IsUserValid(GameUser user, string password)
        {
            if (user == null)
            {
                MessageBox.Show("Не същестува такъв потребител!");
                return false;
            }

            if (user.Password != password)
            {
                MessageBox.Show("Грешна парола!");
                return false;
            }

            return true;
        }
        
        public GameUser GetUser(string username, string password)
        {
            try
            {
                return App.DbManager.GameUser.FirstOrDefault(u => u.Username == username);
            }
            catch (DbException)
            {
                return null;
            }
        }
    }
}
