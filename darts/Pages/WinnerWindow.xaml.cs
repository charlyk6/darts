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

namespace darts.Pages
{
    /// <summary>
    /// Логика взаимодействия для WinnerWindow.xaml
    /// </summary>
    public partial class WinnerWindow : Window
    {
        public WinnerWindow()
        {
            InitializeComponent();
        }

        private void NewGameButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
