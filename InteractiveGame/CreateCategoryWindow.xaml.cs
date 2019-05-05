using System;
using System.Windows;
using System.Windows.Controls;

namespace InteractiveGame
{
    public partial class CreateCategoryWindow : Window
    {
        private const short CATEGORY_NAME_LENGTH = 4;

        public CreateCategoryWindow()
        {
            InitializeComponent();
        }

        public void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateToAdminPanel();
        }

        public void InsertButtonClick(object sender, RoutedEventArgs e)
        {
            string category = CategoryNameBox.Text;

            if (category.Length < CATEGORY_NAME_LENGTH)
            {
                MessageBox.Show("Името на категорията трябва да е поне " + CATEGORY_NAME_LENGTH + " символа!");
                return;
            }

            Category newCategory = new Category(category);
            App.DbManager.Category.Add(newCategory);

            CategoryNameBox.Clear();

            if (!Items.SaveChangesUniqueHandler())
            {
                MessageBox.Show("Категория " + category + " вече същестува!");
                return;
            }

            MessageBox.Show("Категория: " + category + " добавена към списъка с категории.");
            NavigateToAdminPanel();
        }

        public void NavigateToAdminPanel()
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }
    }
}
