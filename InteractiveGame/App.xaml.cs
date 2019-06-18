using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace InteractiveGame
{
    public partial class App : Application
    {
        public static Items DbManager = new Items();

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static void SwitchToWindow(Window curWindow, string newWindowName)
        {
            Window newWindow = null;

            switch (newWindowName)
            {
                case "admin": newWindow = new AdminWindow(); break;
                case "login": newWindow = new LoginWindow(); break;
            }

            curWindow.Close();
            newWindow.Show();
            return;
        }
    }
}
