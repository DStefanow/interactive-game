using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for TopicWindow.xaml
    /// </summary>
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
