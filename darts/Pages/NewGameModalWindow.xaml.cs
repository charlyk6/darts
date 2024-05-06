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
    /// Логика взаимодействия для NewGameModalWindow.xaml
    /// </summary>
    public partial class NewGameModalWindow : Window
    {
        public NewGameModalWindow()
        {
            InitializeComponent();
        }

        private void NewGameСlick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void ContinueСlick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
