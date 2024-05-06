using darts.db;
using darts.db.Entities;
using darts.Pages.Games;
using darts.Pages.Settings;
using System.CodeDom;
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
        private GamePage gamePage { get; set; }
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
            var res = IsValid();
            if (!res.Result)
            {
                var msgWindow = new MessageWindow(res.Message);
                msgWindow.ShowDialog();
                return;
            }
            //TODO: сделать проверку на наличие игррррр
            gamePage = new GamePage();
            MainFrame.NavigationService.Navigate(gamePage);
        }

        private Valid IsValid()
        {
            var message = string.Empty;
            //проверяем есть ли активные игроки (отмечаются на странице настроек)
            var players = settings.getActivePlayers();
            if (!players.Any())
            {
                return new Valid()
                {
                    Result = false,
                    Message = "Внимание! \n\nНет активных игроков \nНеобходимо изменить настройки"
                };
            }

            return new Valid();
        }
    }

    public struct Valid
    {
        public Valid()
        {
            
        }
        public bool Result { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }

}
