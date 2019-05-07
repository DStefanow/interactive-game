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
    public partial class UserWindow : Window
    {
        private GameUser currentUser;

        public UserWindow(GameUser user)
        {
            this.currentUser = user;

            InitializeComponent();

            PopulateUserInfo();
            PopulateCategoryBox();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            LoginWindow logWindow = new LoginWindow();
            this.Close();
            logWindow.Show();
        }

        private void StartTopicClick(object sender, RoutedEventArgs e)
        {

        }

        private void CategoryChange(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine();
        }

        private void PopulateUserInfo()
        {
            UsernameLabel.Content = this.currentUser.Username;
            FullNameLabel.Content = this.currentUser.FullName;
        }

        private void PopulateCategoryBox()
        {
            CategoryBox.Items.Clear();

            List<Category> categories = GetAllCategories();

            CategoryBox.SelectedIndex = 0;
            foreach (Category category in categories)
            {
                CategoryBox.Items.Add(new ComboBoxItem { Content = category.CategoryName, Name = "Id" + category.Id });
            }
        }

        private List<Category> GetAllCategories()
        {
            return App.DbManager.Category.Select(x => x).ToList();
        }
    }
}
