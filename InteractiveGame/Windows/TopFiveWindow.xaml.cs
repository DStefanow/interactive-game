using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InteractiveGame.Windows
{
    public partial class TopFiveWindow : Window
    {
        public TopFiveWindow()
        {
            InitializeComponent();
            PopulateTopPlayersBoard();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            App.SwitchToWindow(this, "login");
            return;
        }

        private void PopulateTopPlayersBoard()
        {
            List<UserScore> topPlayers = UserScore.GetTopFivePlayers();

            string resultData;
            GameUser player;
            foreach (UserScore topPlayer in topPlayers)
            {
                player = GameUser.GetUserById((int)topPlayer.UserId);

                resultData = player.Username + " - " + topPlayer.Score;
                
                TopFiveBoard.Items.Add(new ListBoxItem
                {
                    Name = "Id" + topPlayer.UserId,
                    Content = resultData
                });
            }
        }
    }
}
