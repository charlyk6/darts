﻿using darts.db;
using darts.db.Entities;
using darts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace darts.Pages.Games
{
    /// <summary>
    /// Логика взаимодействия для GameFieldPage.xaml
    /// </summary>
    public partial class GamePage : Page
    {

        Game game;
        ThicknessAnimation powerAnimation = new ThicknessAnimation();
        ThicknessAnimation cornerAnimation = new ThicknessAnimation();
        ThicknessAnimation aimAnimation = new ThicknessAnimation();


        Storyboard powerStoryboard = new Storyboard();
        Storyboard cornerStoryboard = new Storyboard();
        Storyboard aimStoryboard = new Storyboard();


        Constants constnats = new Constants();
        public List<UserEntity> users { get; set; }

        public ObservableCollection<PlayerScoreModel> playerScores { get; set; } = new ObservableCollection<PlayerScoreModel>();

        private ContextDB db = new ContextDB();
        private int curScale = 0;
        public GamePage()
        {
            InitializeComponent();            
            GameLoaded();

            DataContext = playerScores;
            start();
        }

        private void GameLoaded()
        {
            var gameId = CreateNewGame();
            InitPlayers(gameId);

        }

        private int CreateNewGame()
        {
            db.Games.Add(new GameEntity
            {
                Date = DateTime.Now                
            });
            var gameId = db.SaveChanges();

            return gameId;
        }
        private void InitPlayers(int gameId)
        {
            users = db.Users.Where(u => u.IsPlaying).ToList();

            playerScores.Clear();
            foreach (var user in users)
            {
                playerScores.Add(new PlayerScoreModel
                {
                    UserId = user.Id,
                    NickName = user.NickName,
                    Level = user.UserLevel,
                    Score = user.Score,
                    Points = user.Score,
                    LastThrow = 0,
                    NumberThrow = 0
                });
                db.UsersGames.Add(new UsersGameEntity
                {
                    GameId = gameId,
                    UserId = user.Id,
                    Level = user.UserLevel,
                    Total = user.Score
                });
            }
            db.SaveChanges();
        }


        void initArrowsAnimation()
        {
            powerAnimation.From = powerArrow.Margin;
            powerAnimation.To = new Thickness(powerGradient.Width + powerGradient.Margin.Left - powerArrow.Width, powerArrow.Margin.Top, 0, 0);
            powerAnimation.RepeatBehavior = RepeatBehavior.Forever;
            powerAnimation.AutoReverse = true;
            Storyboard.SetTargetName(powerAnimation, powerArrow.Name);
            Storyboard.SetTargetProperty(powerAnimation, new PropertyPath(Image.MarginProperty));
            powerStoryboard.Children.Add(powerAnimation);

            cornerAnimation.From = cornerArrow.Margin;
            cornerAnimation.To = new Thickness(cornerGradient.Width + cornerGradient.Margin.Left - cornerArrow.Width, cornerArrow.Margin.Top, 0, 0);
            cornerAnimation.RepeatBehavior = RepeatBehavior.Forever;
            cornerAnimation.AutoReverse = true;
            Storyboard.SetTargetName(cornerAnimation, cornerArrow.Name);
            Storyboard.SetTargetProperty(cornerAnimation, new PropertyPath(Image.MarginProperty));
            cornerStoryboard.Children.Add(cornerAnimation);

            aimAnimation.From = aim.Margin;
            aimAnimation.To = new Thickness(aimScale.Width + aimScale.Margin.Left - aim.Width, aim.Margin.Top, 0, 0);
            aimAnimation.RepeatBehavior = RepeatBehavior.Forever;
            aimAnimation.AutoReverse = true;
            aimAnimation.SpeedRatio = 0.7;
            Storyboard.SetTargetName(aimAnimation, aim.Name);
            Storyboard.SetTargetProperty(aimAnimation, new PropertyPath(Image.MarginProperty));
            aimStoryboard.Children.Add(aimAnimation);
        }     
       

        public void start()
        {
            initArrowsAnimation();
            //Необходимо вывести что ходит первый игрок
            var player1 = users?.FirstOrDefault()?.NickName ?? string.Empty;
            currentPlayer.Content = player1;

            aimStoryboard.Begin(aim, true);
            

            game = new Game(playerScores);
            game.initDrotiks(drotik1, drotik2, drotik3);
            game.setTarget(target);

        }
        public void throwClick()
        {
            curScale = 0;
            game.doThrow((int)(aim.Margin.Left + aim.Width / 2 - drotik1.Width / 2));
            aimStoryboard.Resume(aim);
            //Нужно перенести в другое место, чтобы выводит чей бросок
            ansLabel.Content = game.curTry.points;
                    playersScores.Items.Refresh();

        }
        public void stopClick(object sender, RoutedEventArgs e)
        {
            switch (curScale)
            {
                case 0:
                    aimStoryboard.Pause(aim);
                    powerStoryboard.Begin(powerArrow, true);
                    break;
                case 1:
                    powerStoryboard.Pause(powerArrow);
                    cornerStoryboard.Begin(cornerArrow, true);
                    game.setPower(constnats.leftPower + (powerArrow.Margin.Left - powerGradient.Margin.Left) * ((constnats.rightPower - constnats.leftPower) / powerGradient.Width));
                    break;
                case 2:
                    cornerStoryboard.Pause(cornerArrow);
                    game.setCorner(constnats.leftCorner + (cornerArrow.Margin.Left - cornerGradient.Margin.Left) * ((constnats.rightCorner - constnats.leftCorner) / cornerGradient.Width));
                    
                    throwClick();
                    curScale = -1;
                    
                    break;
                
            }
            curScale++;
        }

        
    }
}
