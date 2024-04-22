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
            game = new GamePage();
            settings = new SettingsPage();
            MainFrame.NavigationService.Navigate(settings);

        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(settings);
        }

        private void GameButton_OnClick(object sender, RoutedEventArgs e)
        {
            List<UserModel> users = settings.usersModels;
            game.users = users;
            MainFrame.NavigationService.Navigate(game);
        }
        private void NewGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            game = new GamePage();
            List<UserModel> users = settings.usersModels;
            game.users = users;
            MainFrame.NavigationService.Navigate(game);
        }
    }

}
