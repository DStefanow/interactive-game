using System.Linq;
using System.Windows;

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
            App.SwitchToWindow(this, "admin");
        }

        public void InsertButtonClick(object sender, RoutedEventArgs e)
        {
            string category = CategoryNameBox.Text;

            if (category.Length < CATEGORY_NAME_LENGTH)
            {
                MessageBox.Show("Името на категорията трябва да е поне " + CATEGORY_NAME_LENGTH + " символа!");
                return;
            }

            if (App.DbManager.Category.Any(c => c.CategoryName == category))
            {
                MessageBox.Show("Категория " + category + " вече същестува!");
                return;
            }

            Category newCategory = new Category(category);
            App.DbManager.Category.Add(newCategory);
            App.DbManager.SaveChanges();

            MessageBox.Show("Категория: " + category + " добавена към списъка с категории.");
            App.SwitchToWindow(this, "admin");
        }
    }
}
