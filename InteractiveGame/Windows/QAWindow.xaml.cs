using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InteractiveGame
{
    public partial class QAWindow : Window
    {
        GameUser currentUser;
        Topic currentTopic;
        Question[] questions;
        Dictionary<Question, List<Answer>> questionWithAnswers;
        List<RadioButton> correctAnswers = new List<RadioButton>();

        public QAWindow(GameUser currentUser, Topic currentTopic)
        {
            this.currentUser = currentUser;
            this.currentTopic = currentTopic;
            this.questionWithAnswers = Answer.GetQuestionsWithAnswers(currentTopic);
            this.questions = this.questionWithAnswers.Keys.ToArray();

            InitializeComponent();

            TopicTitleLabel.Content = currentTopic.Title.ToString();
            PopulateQuestionsAndAnswers();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            RedirectToUserProfile();
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            ushort sumPoints = SumQuestionRadioPoints();
            sumPoints += CheckOpenQuestion();

            SavePointsFromTopic(sumPoints);

            MessageBox.Show("Общо точки: " + sumPoints);
            RedirectToUserProfile();
        }

        private void PopulateQuestionsAndAnswers()
        {
            QuestionOneLabel.Content = questions[0].Description.ToString();
            QuestionTwoLabel.Content = questions[1].Description.ToString();
            QuestionThreeLabel.Content = questions[2].Description.ToString();
            QuestionFourthLabel.Content = questions[3].Description.ToString();

            for (int i = 0; i < 3; i++)
            {
                PopulateAnswerBoxes(questions[i], i);
            }
        }

        private void PopulateAnswerBoxes(Question question, int index)
        {
            string[] indexStr = new string[] { "First", "Second", "Third", "Fourth" };
            string wpfName = indexStr[index];

            List<Answer> answers = Answer.GetAnswersForQuestionRnd(question.Id);

            string answerIndex;
            string btnName;
            for (int i = 0; i < answers.Count; i++)
            {
                answerIndex = indexStr[i];
                btnName = wpfName + "Answer" + answerIndex;
                RadioButton radioBtn = this.FindName(btnName) as RadioButton;
                
                radioBtn.Content = answers[i].Description;

                if ((bool)answers[i].IsTrue)
                {
                    // Save question points
                    radioBtn.Tag = question.Points;
                    correctAnswers.Add(radioBtn);
                }
            }
        }

        private ushort SumQuestionRadioPoints()
        {
            ushort result = 0;

            foreach (RadioButton rnd in correctAnswers)
            {
                if ((bool)rnd.IsChecked)
                {
                    result += UInt16.Parse(rnd.Tag.ToString());
                }
            }

            return result;
        }

        private ushort CheckOpenQuestion()
        {
            ushort points = 0;

            string answer = AnswerFourthBox.Text.ToLower();
            string correctAnswer = questionWithAnswers.Last().Value.Last().Description;

            if (String.Equals(answer, correctAnswer))
            {
                points = (ushort)questionWithAnswers.Last().Key.Points;
            }

            return points;
        }

        public void SavePointsFromTopic(ushort points)
        {
            UserScore.UpdateUserScore(this.currentUser, this.currentTopic, points);
        }

        private void RedirectToUserProfile()
        {
            UserWindow userWindow = new UserWindow(currentUser);
            this.Close();
            userWindow.Show();
            return;
        }
    }
}
