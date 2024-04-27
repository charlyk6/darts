using darts.db;
using darts.db.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace darts.Pages.Settings
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private ContextDB db = new ContextDB();

        public SettingsPage()
        {
            InitializeComponent();
            Loaded += SettingsPage_Loaded;
        }
        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Users.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Users.Local.ToObservableCollection();
        }

        public List<UserEntity> getActivePlayers()
        {
            return db.Users.Where(u => u.IsPlaying).ToList();
        }

        public List<UserEntity> getUsers()
        {
            var users = db.Users.Local.ToList();
            return users;
        }
        private void checkBox_UnChecked(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            UserWindow UserWindow = new UserWindow(new UserEntity());
            if (UserWindow.ShowDialog() == true)
            {
                UserEntity User = UserWindow.User;
                db.Users.Add(User);
                db.SaveChanges();

            }
        }

        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            UserEntity? user = usersList.SelectedItem as UserEntity;
            // если ни одного объекта не выделено, выходим
            if (user is null)
            {
                var msgWindow = new MessageWindow("Выберите игрока");
                msgWindow.ShowDialog();
                return;
            }

            //UserEntity? user = userM.getEntity();
            
            UserWindow UserWindow = new UserWindow(new UserEntity
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.NickName,
                UserLevel = user.UserLevel,
                Total = user.Total,
                IsPlaying = user.IsPlaying
            });

            if (UserWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                user = db.Users.Find(UserWindow.User.Id);
                if (user != null)
                {
                    user.FirstName = UserWindow.User.FirstName;
                    user.LastName = UserWindow.User.LastName;
                    user.NickName = UserWindow.User.NickName;
                    user.UserLevel = UserWindow.User.UserLevel;
                    user.Total = UserWindow.User.Total;
                    user.IsPlaying = UserWindow.User.IsPlaying;
                    db.SaveChanges();
                    usersList.Items.Refresh();
                }
            }
        }

        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            UserEntity? user = usersList.SelectedItem as UserEntity;
            // если ни одного объекта не выделено, выходим
            if (user is null)
            {
                var msgWindow = new MessageWindow("Выберите игрока");
                msgWindow.ShowDialog();
                return;
            }

            //UserEntity user = userM.getEntity();
            //usersModels.Remove(userM);
            db.Users.Remove(user);
            db.SaveChanges();
            //usersList.Items.Refresh();
        }
    }
}
