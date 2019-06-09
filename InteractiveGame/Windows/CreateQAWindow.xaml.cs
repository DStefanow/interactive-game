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

        public void InsertButtonClick(object sender, RoutedEventArgs e)
        {
            string selectedName = ((ComboBoxItem)TopicBox.SelectedItem).Name.ToString();
            int topicId = Int32.Parse(Regex.Match(selectedName, @"\d+").Value);

            // Delete all questions with answer for the given topic (update option)
            App.DbManager.Question.RemoveRange(
                App.DbManager.Question.Where(
                    x => x.TopicId == topicId
                )
            );
            App.DbManager.SaveChanges();


            InsertFirstQuestionAnswers(topicId);
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

        private void InsertFirstQuestionAnswers(int topicId)
        {
            // First Question
            string firstQuestion = this.FirstQuestion.Text;
            int firstPoints = Int32.Parse(this.FirstPoints.Text);

            // First Answers
            string firstCorrect = this.FirstCorrect.Text;
            string firstWrongOne = this.FirstWrongOne.Text;
            string firstWrongTwo = this.FirstWrongTwo.Text;
            string firstWrongThree = this.FirstWrongThree.Text;

            Question question = new Question(topicId, firstQuestion, firstPoints);
            App.DbManager.Question.Add(question);
            App.DbManager.SaveChanges();

            // Add answers
            App.DbManager.Answer.Add(new Answer(question.Id, firstCorrect, true));
            App.DbManager.Answer.Add(new Answer(question.Id, firstWrongOne, false));
            App.DbManager.Answer.Add(new Answer(question.Id, firstWrongTwo, false));
            App.DbManager.Answer.Add(new Answer(question.Id, firstWrongThree, false));

            App.DbManager.SaveChanges();
        }

        private List<Topic> GetAllTopicsForGivenCategory(int currentCategoryId)
        {
            return App.DbManager.Topic.Where(x => x.CategoryId == currentCategoryId).ToList();
        }
    }
}
