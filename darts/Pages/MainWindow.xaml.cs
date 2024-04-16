using darts.Pages.Games;
using darts.Pages.Settings;
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
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(settings);
        }

        private void GameButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(game);
        }
    }

}
