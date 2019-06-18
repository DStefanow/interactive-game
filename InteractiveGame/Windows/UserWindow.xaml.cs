using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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
            PopulateUserScoreBox(currentUser.Id);
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            App.NavigateToLoginWindow(this);
            return;
        }

        private void StartTopicClick(object sender, RoutedEventArgs e)
        {
            string selectedName = ((ComboBoxItem)TopicBox.SelectedItem).Name.ToString();
            int topicId = Int32.Parse(Regex.Match(selectedName, @"\d+").Value);

            TopicWindow topicWindow = new TopicWindow(topicId, currentUser);
            this.Close();
            topicWindow.Show();
            return;
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

        private void PopulateUserInfo()
        {
            UsernameLabel.Content = this.currentUser.Username;
            FullNameLabel.Content = this.currentUser.FullName;
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

        private void PopulateUserScoreBox(int userId)
        {
            List<Category> allCategories = App.DbManager.Category.Select(x => x).ToList();

            int currentScore = 0;
            string resultData;
            foreach (Category cat in allCategories)
            {
                UserScore currentResultByCategory = App.DbManager.UserScore.
                    FirstOrDefault(x => x.UserId == userId && x.CategoryId == cat.Id);

                if (currentResultByCategory == null ||
                     currentResultByCategory.Score == 0 ||
                     currentResultByCategory.Score == null)
                {
                    currentScore = 0;
                }
                else
                {
                    currentScore = (int)currentResultByCategory.Score;
                }

                resultData = cat.CategoryName +  " - " + currentScore;

                ListBoxItem item = new ListBoxItem
                {
                    Name = "Id" + cat.Id,
                    Content = resultData
                };

                PointOnCategory.Items.Add(item);
            }
        }
    }
}
