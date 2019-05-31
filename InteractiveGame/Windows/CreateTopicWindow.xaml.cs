using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            NavigateToAdminPanel();
        }

        private void InsertButtonClick(object sender, RoutedEventArgs e)
        {
            string selectedName = ((ComboBoxItem)CategoryBox.SelectedItem).Name.ToString();
            int categoryId = Int32.Parse(Regex.Match(selectedName, @"\d+").Value);

            string topicTitle = TitleBox.Text;
            string topicDescription = TopicText.Text;

            AddTopic(categoryId, topicTitle, topicDescription);

            if (!Items.SaveChangesUniqueHandler())
            {
                MessageBox.Show("Тема със заглавие: " + topicTitle + " вече е създадена!");
                return;
            }

            NavigateToAdminPanel();
        }

        private void NavigateToAdminPanel()
        {
            AdminWindow admWindow = new AdminWindow();
            this.Close();
            admWindow.Show();
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

        private void AddTopic(int categoryId, string title, string description)
        {
            Topic newTopic = new Topic(categoryId, title, description);
            App.DbManager.Topic.Add(newTopic);
        }
    }
}
