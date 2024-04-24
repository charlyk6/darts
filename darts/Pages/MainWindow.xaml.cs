using darts.db;
using darts.db.Entities;
using darts.Pages.Games;
using darts.Pages.Settings;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace darts.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GamePage game { get; set; }
        private SettingsPage settings { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            settings = new SettingsPage();
            MainFrame.NavigationService.Navigate(settings);

        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(settings);
        }
        private bool checkEmpty()
        {
            var users = settings.getUsers();
            int cnt = 0;
            foreach (var user in users)
            {
                if(user.IsPlaying) cnt++;
            }
            if(cnt==0) return true;
            return false;
        }
        private void GameButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (checkEmpty())
            {
                var msgWindow = new MessageWindow("Выберите хотя бы одного игрока");
                msgWindow.ShowDialog();
                return;
            }
            if(game == null) game = new GamePage();
            MainFrame.NavigationService.Navigate(game);

        }
        private void NewGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (checkEmpty())
            {
                var msgWindow = new MessageWindow("Выберите хотя бы одного игрока");
                msgWindow.ShowDialog();
                return;
            }
            game = new GamePage();
            MainFrame.NavigationService.Navigate(game);
        }
    }

}
