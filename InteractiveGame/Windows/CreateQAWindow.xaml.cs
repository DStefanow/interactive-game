using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace InteractiveGame
{
    public partial class CreateQAWindow : Window
    {
        public CreateQAWindow()
        {
            InitializeComponent();
            PopulateCategoryBox();
        }

        public void BackButtonClick(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
            return;
        }

        private void PopulateCategoryBox()
        {
            CategoryBox.Items.Clear();

            List<Category> categories = GetAllCategoriesWithTopics();

            CategoryBox.SelectedIndex = 0;
            foreach (Category category in categories)
            {
                CategoryBox.Items.Add(new ComboBoxItem { Content = category.CategoryName, Name = "Id" + category.Id });
            }
        }
        private void CategoryChange(object sender, SelectionChangedEventArgs e)
        {
            TopicBox.Items.Clear();

            string selectedName = ((ComboBoxItem)CategoryBox.SelectedItem).Name.ToString();
            int categoryId = Int32.Parse(Regex.Match(selectedName, @"\d+").Value);

            List<Topic> topicsForCategory = GetAllTopicsForGivenCategory(categoryId);

            TopicBox.SelectedIndex = 0;
            foreach (Topic topic in topicsForCategory)
            {
                TopicBox.Items.Add(new ComboBoxItem { Content = topic.Title, Name = "Id" + topic.Id });
            }
        }

        private List<Category> GetAllCategoriesWithTopics()
        {
            return App.DbManager.Category.SqlQuery("SELECT c.id, c.category_name as CategoryName FROM category c " + 
                    "JOIN topic t " +
                    "ON c.id = t.category_id " +
                    "GROUP BY c.id, c.category_name").
                    ToList();
        }

        private List<Topic> GetAllTopicsForGivenCategory(int currentCategoryId)
        {
            return App.DbManager.Topic.Where(x => x.CategoryId == currentCategoryId).ToList();
        }
    }
}
