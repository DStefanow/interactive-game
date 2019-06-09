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
        private int topicId;

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
            foreach (TextBox t in App.FindVisualChildren<TextBox>(this))
            {
                if (t.Text == "")
                {
                    MessageBox.Show("Моля попълнете всички полета");
                    return;
                }
            }

            string selectedName = ((ComboBoxItem)TopicBox.SelectedItem).Name.ToString();
            int topicId = Int32.Parse(Regex.Match(selectedName, @"\d+").Value);
            this.topicId = topicId;

            // Delete all questions with answer for the given topic (update option)
            App.DbManager.Question.RemoveRange(
                App.DbManager.Question.Where(
                    x => x.TopicId == topicId
                )
            );
            App.DbManager.SaveChanges();

            // Insert Questions and Answers
            InsertFirstQuestionAnswers();
            InsertSecondQuestionAnswers();
            InsertThirdQuestionAnswers();
            InsertFourthQuestionAnswer();
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

        private void InsertFirstQuestionAnswers()
        {
            // First Question
            string firstQuestion = this.FirstQuestion.Text;
            int firstPoints = Int32.Parse(this.FirstPoints.Text);

            // First Answers
            string firstCorrect = this.FirstCorrect.Text;
            string firstWrongOne = this.FirstWrongOne.Text;
            string firstWrongTwo = this.FirstWrongTwo.Text;
            string firstWrongThree = this.FirstWrongThree.Text;

            Question question = new Question(this.topicId, firstQuestion, firstPoints);
            App.DbManager.Question.Add(question);
            App.DbManager.SaveChanges();

            // Add answers
            App.DbManager.Answer.Add(new Answer(question.Id, firstCorrect, true));
            App.DbManager.Answer.Add(new Answer(question.Id, firstWrongOne, false));
            App.DbManager.Answer.Add(new Answer(question.Id, firstWrongTwo, false));
            App.DbManager.Answer.Add(new Answer(question.Id, firstWrongThree, false));

            App.DbManager.SaveChanges();
        }

        private void InsertSecondQuestionAnswers()
        {
            // Second Question
            string secondQuestion = this.SecondQuestion.Text;
            int secondPoints = Int32.Parse(this.SecondPoints.Text);

            // Second Answers
            string secondCorrect = this.SecondCorrect.Text;
            string secondWrongOne = this.SecondWrongOne.Text;
            string secondWrongTwo = this.SecondWrongTwo.Text;
            string secondWrongThree = this.SecondWrongThree.Text;

            Question question = new Question(this.topicId, secondQuestion, secondPoints);
            App.DbManager.Question.Add(question);
            App.DbManager.SaveChanges();

            // Add answers
            App.DbManager.Answer.Add(new Answer(question.Id, secondCorrect, true));
            App.DbManager.Answer.Add(new Answer(question.Id, secondWrongOne, false));
            App.DbManager.Answer.Add(new Answer(question.Id, secondWrongTwo, false));
            App.DbManager.Answer.Add(new Answer(question.Id, secondWrongThree, false));

            App.DbManager.SaveChanges();
        }

        private void InsertThirdQuestionAnswers()
        {
            // Third Question
            string thirdQuestion = this.ThirdQuestion.Text;
            int thirdPoints = Int32.Parse(this.ThirdPoints.Text);

            // Third Answers
            string thirdCorrect = this.ThirdCorrect.Text;
            string thirdWrongOne = this.ThirdWrongOne.Text;
            string thirdWrongTwo = this.ThirdWrongTwo.Text;
            string thirdWrongThree = this.ThirdWrongThree.Text;

            Question question = new Question(this.topicId, thirdQuestion, thirdPoints);
            App.DbManager.Question.Add(question);
            App.DbManager.SaveChanges();

            // Add answers
            App.DbManager.Answer.Add(new Answer(question.Id, thirdCorrect, true));
            App.DbManager.Answer.Add(new Answer(question.Id, thirdWrongOne, false));
            App.DbManager.Answer.Add(new Answer(question.Id, thirdWrongTwo, false));
            App.DbManager.Answer.Add(new Answer(question.Id, thirdWrongThree, false));

            App.DbManager.SaveChanges();
        }

        private void InsertFourthQuestionAnswer()
        {
            // Fourth Question
            string fourthQuestion = this.FourthQuestion.Text;
            int fourthPoints = Int32.Parse(this.FourthPoints.Text);

            // Fourth Answers
            string fourthCorrect = this.FourthAnswer.Text;

            Question question = new Question(this.topicId, fourthQuestion, fourthPoints);
            App.DbManager.Question.Add(question);
            App.DbManager.SaveChanges();

            // Add answer
            App.DbManager.Answer.Add(new Answer(question.Id, fourthCorrect, true));
            App.DbManager.SaveChanges();
        }

        private List<Topic> GetAllTopicsForGivenCategory(int currentCategoryId)
        {
            return App.DbManager.Topic.Where(x => x.CategoryId == currentCategoryId).ToList();
        }
    }
}
