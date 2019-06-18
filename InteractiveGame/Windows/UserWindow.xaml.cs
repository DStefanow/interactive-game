using System.Collections.Generic;
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
            App.SwitchToWindow(this, "login");
            return;
        }

        private void StartTopicClick(object sender, RoutedEventArgs e)
        { 
            int topicId = (int)((ComboBoxItem)TopicBox.SelectedItem).Tag;

            TopicWindow topicWindow = new TopicWindow(topicId, currentUser);
            this.Close();
            topicWindow.Show();
            return;
        }

        private void CategoryChange(object sender, SelectionChangedEventArgs e)
        {
            TopicBox.Items.Clear();
            
            int categoryId = (int)((ComboBoxItem)CategoryBox.SelectedItem).Tag;

            List<Topic> topicsForCategory = Topic.GetAllTopicsForGivenCategory(categoryId);

            TopicBox.SelectedIndex = 0;
            foreach (Topic topic in topicsForCategory)
            {
                TopicBox.Items.Add(new ComboBoxItem { Content = topic.Title, Tag = topic.Id });
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

            List<Category> categories = Category.GetAllCategoriesWithTopics();

            CategoryBox.SelectedIndex = 0;
            foreach (Category category in categories)
            {
                CategoryBox.Items.Add(new ComboBoxItem { Content = category.CategoryName, Tag = category.Id });
            }
        }

        private void PopulateUserScoreBox(int userId)
        {
            List<Category> allCategories = Category.GetAllCategories();

            int currentScore = 0;
            string resultData;
            foreach (Category cat in allCategories)
            {
                UserScore currentResultByCategory = UserScore.GetUserScoreData(this.currentUser, cat);

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
                
                PointOnCategory.Items.Add(new ListBoxItem {
                    Name = "Id" + cat.Id,
                    Content = resultData
                });
            }
        }
    }
}
