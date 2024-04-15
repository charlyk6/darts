using darts.db.Entities;
using System.Windows;

namespace darts.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserEntity User { get; set; }
        public UserWindow(UserEntity user)
        {
            InitializeComponent();
            this.User = user;
            DataContext = User;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
