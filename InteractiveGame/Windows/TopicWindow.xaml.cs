using System.Linq;
using System.Windows;

namespace InteractiveGame
{
    public partial class TopicWindow : Window
    {
        GameUser currentUser;
        Topic currentTopic;

        public TopicWindow(int topicId, GameUser user)
        {
            currentUser = user;
            currentTopic = App.DbManager.Topic.FirstOrDefault(t => t.Id == topicId);

            InitializeComponent();
            PopulateTopicData();
        }
        
        private void ToUserWindow(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow(currentUser);
            this.Close();
            userWindow.Show();
            return;
        }

        private void StartQASession(object sender, RoutedEventArgs e)
        {
            if (!Question.HasQuestionsForTopic(currentTopic))
            {
                MessageBox.Show("Липсват въпроси за тема: " + currentTopic.Title);
                return;
            }

            QAWindow qaWindow = new QAWindow(currentUser, currentTopic);
            this.Close();
            qaWindow.Show();
            return;
        }

        private void PopulateTopicData()
        {
            TopicTitleLabel.Content = currentTopic.Title;
            TopicContentLabel.Text = currentTopic.Description;
        }
    }
}
