using darts.db;
using darts.db.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace darts.Pages.Settings
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private ContextDB db = new ContextDB();
        public List<UserModel> usersModels = new List<UserModel>();

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
            var usersEntities = db.Users.Local.ToList();
            foreach (var user in usersEntities)
            {
                usersModels.Add(new UserModel(user));
            }
            DataContext = usersModels;
        }

        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            UserWindow UserWindow = new UserWindow(new UserEntity());
            if (UserWindow.ShowDialog() == true)
            {
                var User = UserWindow.User;
                db.Users.Add(User);
                usersModels.Add(new UserModel(User));
        //Пример добавления других данных
                //db.Games.Add(new GameEntity
                //{
                //    Date = DateTime.Now,
                //    Title = "Первая игра"                    
                //});
                db.SaveChanges();
                usersList.Items.Refresh();

            }
        }

        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            UserModel? userM = usersList.SelectedItem as UserModel;
            // если ни одного объекта не выделено, выходим
            if (userM is null) return;

            UserEntity? user = userM.getEntity();
            
            UserWindow UserWindow = new UserWindow(new UserEntity
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.NickName
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
                    userM.FirstName = user.FirstName;
                    userM.LastName = user.LastName;
                    userM.NickName = user.NickName;
                    db.SaveChanges();
                    usersList.Items.Refresh();
                }
            }
        }

        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            // получаем выделенный объект
            UserModel? userM = usersList.SelectedItem as UserModel;
            // если ни одного объекта не выделено, выходим
            if (userM is null) return;
            
            UserEntity user = userM.getEntity();
            usersModels.Remove(userM);
            db.Users.Remove(user);
            db.SaveChanges();
            usersList.Items.Refresh();
        }
    }
}
