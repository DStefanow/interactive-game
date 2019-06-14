using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InteractiveGame
{
    public partial class QAWindow : Window
    {
        GameUser currentUser;
        Topic currentTopic;
        Question[] questions;

        public QAWindow(GameUser currentUser, Topic currentTopic)
        {
            this.currentUser = currentUser;
            this.currentTopic = currentTopic;

            InitializeComponent();
            PopulateQuestionsAndAnswers();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow(currentUser);
            this.Close();
            userWindow.Show();
            return;
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {

        }

        private void PopulateQuestionsAndAnswers()
        {
            TopicTitleLabel.Content = currentTopic.Title.ToString();

            this.questions = Items.GetQuestionsForTopic(currentTopic);

            QuestionOneLabel.Content = questions[0].Description.ToString();
            QuestionTwoLabel.Content = questions[1].Description.ToString();
            QuestionThreeLabel.Content = questions[2].Description.ToString();
            QuestionFourthLabel.Content = questions[3].Description.ToString();

            for (int i = 0; i < 4; i++)
            {
                PopulateAnswerBoxes(questions[i], i);
            }
        }

        private void PopulateAnswerBoxes(Question question, int index)
        {

        }
    }
}
