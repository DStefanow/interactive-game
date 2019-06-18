using InteractiveGame.Windows;
using System.Windows;
using System.Windows.Controls;

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

            GameUser user = GameUser.GetUser(username, password);

            if (!IsUserValid(user, password))
            {
                return;
            }

            bool isAdmin = user.IsAdmin ?? false;

            if (isAdmin)
            {
                App.SwitchToWindow(this, "admin");
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
            return;
        }

        private void TopFiveClickEvent(object sender, RoutedEventArgs e)
        {
            TopFiveWindow topFiveWindow = new TopFiveWindow();
            this.Close();
            topFiveWindow.Show();
            return;
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
    }
}
