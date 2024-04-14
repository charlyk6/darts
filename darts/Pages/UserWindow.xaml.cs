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
            if(User.FirstName is null || User.LastName is null || User.NickName is null || User.Total is null)
            {
                var msgWindow = new MessageWindow("Заполните все поля");
                msgWindow.ShowDialog();
                return;
            }
            DialogResult = true;
        }
    }
}
