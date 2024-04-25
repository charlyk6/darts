using darts.db;
using darts.db.Entities;
using darts.Pages.Games;
using darts.Pages.Settings;
using System.Collections.Generic;
using System.Linq;
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
       
        private void GameButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsValid(out string message))
            {
                var msgWindow = new MessageWindow(message);
                msgWindow.ShowDialog();
                return;
            }
            if(game == null) game = new GamePage();
            MainFrame.NavigationService.Navigate(game);
        }

        private bool IsValid(out string message)
        {
            message = string.Empty;
            //проверяем есть ли активные игроки (отмечаются на странице настроек)
            var players = settings.getActivePlayers();
            if (!players.Any()) {
                message = "Внимание! \n\nНет активных игроков \nНеобходимо изменить настройки";
                return false;
            }
            
            return true;
        }
    }

}
