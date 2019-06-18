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
            PopulateCategoryBox();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            App.SwitchToWindow(this, "admin");
        }

        private void InsertButtonClick(object sender, RoutedEventArgs e)
        {
            int categoryId = (int)((ComboBoxItem)CategoryBox.SelectedItem).Tag;

            string topicTitle = TitleBox.Text;
            string topicDescription = TopicText.Text;
            
            if (topicTitle == "" || topicDescription == "")
            {
                MessageBox.Show("Моля добавете заглавие и описание на темата.");
                return;
            }

            if (App.DbManager.Topic.Any(t => t.Title == topicTitle))
            {
                MessageBox.Show("Тема със заглавие: " + topicTitle + " за дадената категория вече е създадена!");
                return;
            }

            AddTopic(categoryId, topicTitle, topicDescription);
            App.DbManager.SaveChanges();

            App.SwitchToWindow(this, "admin");
        }

        private void PopulateCategoryBox()
        {
            CategoryBox.Items.Clear();

            List<Category> categories = Category.GetAllCategories();

            CategoryBox.SelectedIndex = 0;
            foreach (Category category in categories)
            {
                CategoryBox.Items.Add(new ComboBoxItem { Content = category.CategoryName, Tag = category.Id });
            }
        }

        private void AddTopic(int categoryId, string title, string description)
        {
            Topic newTopic = new Topic(categoryId, title, description);
            App.DbManager.Topic.Add(newTopic);
        }
    }
}
