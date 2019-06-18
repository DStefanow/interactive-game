using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InteractiveGame
{
    public partial class CreateQAWindow : Window
    {
        private int topicId;

        public CreateQAWindow()
        {
            InitializeComponent();
            PopulateCategoryBox();
        }

        public void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateToAdminWindow();
            return;
        }

        public void InsertButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (TextBox t in App.FindVisualChildren<TextBox>(this))
            {
                if (t.Text == "")
                {
                    MessageBox.Show("Моля попълнете всички полета");
                    return;
                }
            }

            this.topicId = (int)((ComboBoxItem)TopicBox.SelectedItem).Tag;

            // Delete all questions with answer for the given topic (update option)
            Question.DeleteAllQuestionsByTopicId(this.topicId);
            App.DbManager.SaveChanges();

            // Insert Questions and Answers
            for (int i = 0; i < 4; i++)
            {
                InsertQuestionAnswers(i);
            }

            MessageBox.Show("Въпросите са добавени успешно!");
            NavigateToAdminWindow();
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

        private void InsertQuestionAnswers(int index)
        {
            string[] propNames = new string[] { "First", "Second", "Third", "Fourth" };
            string wpfName = propNames[index];

            string questionBoxName = wpfName + "Question";
            string pointsBoxName = wpfName + "Points";

            // Get question info
            string question = (this.FindName(questionBoxName) as TextBox).Text;
            int points = Int32.Parse((this.FindName(pointsBoxName) as ComboBox).Text);
           
            // Insert question
            Question questionObj = new Question(this.topicId, question, points);
            App.DbManager.Question.Add(questionObj);
            App.DbManager.SaveChanges();

            // Get answers info
            if (index < 3)
            {
                string correct = (this.FindName(wpfName + "Correct") as TextBox).Text;
                string firstWrong = (this.FindName(wpfName + "WrongOne") as TextBox).Text;
                string secondWrong = (this.FindName(wpfName + "WrongTwo") as TextBox).Text;
                string thirdWrong = (this.FindName(wpfName + "WrongThree") as TextBox).Text;

                // Insert answers
                App.DbManager.Answer.Add(new Answer(questionObj.Id, correct, true));
                App.DbManager.Answer.Add(new Answer(questionObj.Id, firstWrong, false));
                App.DbManager.Answer.Add(new Answer(questionObj.Id, secondWrong, false));
                App.DbManager.Answer.Add(new Answer(questionObj.Id, thirdWrong, false));
            }
            else
            {
                string correct = (this.FindName(wpfName + "Answer") as TextBox).Text;
                App.DbManager.Answer.Add(new Answer(questionObj.Id, correct, true));
            }

            App.DbManager.SaveChanges();
        }
        
        private void NavigateToAdminWindow()
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }
    }
}
