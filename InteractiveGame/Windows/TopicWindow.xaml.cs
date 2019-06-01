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
        }
    }
}
