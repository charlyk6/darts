﻿using darts.db;
using darts.db.Entities;
using darts.Models;
using Microsoft.EntityFrameworkCore;
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
        GameEntity game;
        ThicknessAnimation powerAnimation = new ThicknessAnimation();
        ThicknessAnimation cornerAnimation = new ThicknessAnimation();
        ThicknessAnimation aimAnimation = new ThicknessAnimation();

        public List<Image> drotikImages;
        public List<Drotik> drotiks;
        public int indexCurrentDrotik = 0;
        public int indexCurrentPlayer = 0;
        public bool isFinished = false;


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

            drotiks = new List<Drotik>
            {
                new Drotik(drotik1),new Drotik(drotik2),new Drotik(drotik3)
            };
            GameLoaded();

            DataContext = playerScores;
            start();
        }

        private void GameLoaded()
        {
            int gameId;
            //Проверяем нет ли НЕзаконченных игр:
            var activeGames = FindActiveGames();
            if (!activeGames.Any())
            {
                game = CreateNewGame();
                InitPlayers(game);
            }
            else
            {
                LoadGamesItems(activeGames);
            }
        }


        private List<GameEntity> FindActiveGames()
        {
            return db.Games
                .Include(g => g.UsersGames)
                    .ThenInclude(ug => ug.User)
                .Where(g => !g.IsFinish).ToList();
        }

        private GameEntity? CreateNewGame()
        {
            var game = db.Games.Add(new GameEntity
            {
                Date = DateTime.Now
            });
            db.SaveChanges();

            return db.Games
                 .Include(g => g.UsersGames)
                     .ThenInclude(ug => ug.User)
                 .FirstOrDefault(g => g.Id == game.Entity.Id);
        }

        private void LoadGamesItems(List<GameEntity> games)
        {
            playerScores.Clear();
            //TODO: пока не знаю как правильно, пока берем просто первую
            game = games.FirstOrDefault();
            if (game is not null)
            {
                var usersGames = game.UsersGames.ToList();
                foreach (var usersGame in usersGames)
                {
                    playerScores.Add(new PlayerScoreModel
                    {
                        UserId = usersGame.UserId.Value,
                        NickName = usersGame.User.NickName,
                        Level = usersGame.Level,
                        Total = usersGame.Total,
                        Scores = usersGame.Scores,
                        NumberThrow = usersGame.NumberThrow
                    });
                }
            }
        }

        private void InitPlayers(GameEntity game)
        {
            playerScores.Clear();
            users = db.Users.Where(u => u.IsPlaying).ToList();

            foreach (var user in users)
            {
                playerScores.Add(new PlayerScoreModel
                {
                    UserId = user.Id,
                    NickName = user.NickName,
                    Level = user.UserLevel,
                    Total = user.Total,
                    Scores = user.Total,
                    NumberThrow = 0
                });
                game.UsersGames.Add(new UsersGameEntity
                {
                    GameId = game.Id,
                    UserId = user.Id,
                    Level = user.UserLevel,
                    Total = user.Total,
                    Scores = user.Total
                });
            }
            db.SaveChanges();
        }


        void initArrowsAnimation()
        {
            aimAnimation.From = aim.Margin;
            aimAnimation.To = new Thickness(aimScale.Width + aimScale.Margin.Left - aim.Width, aim.Margin.Top, 0, 0);
            aimAnimation.RepeatBehavior = RepeatBehavior.Forever;
            aimAnimation.AutoReverse = true;
            aimAnimation.SpeedRatio = 0.7;
            Storyboard.SetTargetName(aimAnimation, aim.Name);
            Storyboard.SetTargetProperty(aimAnimation, new PropertyPath(Image.MarginProperty));
            aimStoryboard.Children.Add(aimAnimation);

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
        }


        public void start()
        {
            initArrowsAnimation();
            var indexCurrentPlayer = 0;
            var player = playerScores[indexCurrentPlayer];
            currentPlayer.Content = player.NickName;
            aimStoryboard.Begin(aim, true);

            //game = new Game(playerScores);
            //game.initDrotiks(drotik1, drotik2, drotik3);
            //game.setTarget(target);

            //while (!isGameFinished)
            //{
            //    //var currentPlayer = playerScores[indexCurPlayer];

            //    for (int i = 0; i < 3; i++)
            //    {
            //        var drotik = new Drotik(drotiksImages[i]);
            //        isGameFinished = Move(drotik);
            //        if (isGameFinished)
            //        {
            //            //TODO вывести на экран - игра закончена
            //            break;
            //        }


            //    }

            //    //indexCurPlayer++;
            //    //indexCurPlayer %= playerScores.Count;
            //}

        }



        private void Move()
        {
            var x = aim.Margin.Left + aim.Width / 2;
            var y0 = target.Margin.Top;
            drotiks[indexCurrentDrotik].Throw.corner = (constnats.leftCorner + (cornerArrow.Margin.Left - cornerGradient.Margin.Left) * ((constnats.rightCorner - constnats.leftCorner) / cornerGradient.Width));
            drotiks[indexCurrentDrotik].Throw.power = constnats.leftPower + (powerArrow.Margin.Left - powerGradient.Margin.Left) * ((constnats.rightPower - constnats.leftPower) / powerGradient.Width);

            drotiks[indexCurrentDrotik].MakeThrow((int)x, (int)y0);
            var points = Target.GetPoints(target, drotiks[indexCurrentDrotik]);
            ansLabel.Content = points;
            isFinished = CheckFinishedGame(points);
            if (isFinished)
            {
                Finish();
            }
            //TODO сохранить в БД
            indexCurrentDrotik++;
            //TODO разобраться когда делать паузу
            CheckMove();
            aimStoryboard.Begin(aim, true);
            //TODO куча логики

        }

        private void Finish()
        {
            var userId = playerScores[indexCurrentPlayer].UserId;
            var winner = game.UsersGames.FirstOrDefault(x => x.UserId == userId)?.User;

            game.IsFinish = true;
            game.Winner = winner;
            //TODO Отобразить победителя

            db.SaveChanges();
        }

        private bool CheckFinishedGame(int points)
        {
            var result = false;
            var userId = playerScores[indexCurrentPlayer].UserId;
            var usersGame = game.UsersGames.FirstOrDefault(x => x.UserId == userId);
            if (usersGame != null)
            {
                var level = usersGame.Level;
                var total = usersGame.Total;
                var scores = usersGame.Scores;
                switch (level)
                {
                    case darts.db.Enums.Level.Easy:
                        //в любом случае засчитываем бросок
                        scores -= points;
                        usersGame.Scores = scores;
                        usersGame.NumberThrow++;
                        playerScores[indexCurrentPlayer].Scores -= points;
                        if (scores <= 0)
                        {
                            result = true;
                        }

                        break;
                    case darts.db.Enums.Level.Medium:
                        //если не превысил, то засчитываем бросок

                        break;
                    case darts.db.Enums.Level.Hard:
                        //проверяем на превышение и окончание игры должно быть через удвоение

                        break;
                    default:
                        break;
                }

            }
            playerScores[indexCurrentPlayer].NumberThrow++;
            playersScores.Items.Refresh();
            db.SaveChanges();
            return result;
        }

        private void CheckMove()
        {
            if (indexCurrentDrotik == 3)
            {
                indexCurrentPlayer++;
                indexCurrentPlayer %= playerScores.Count;
                indexCurrentDrotik = 0;
                foreach (var drotik in drotiks)
                {
                    drotik.StayInvisibe();
                }
                var player = playerScores[indexCurrentPlayer];
                currentPlayer.Content = player.NickName;
            }
        }


        //public void MakeThrow()
        //{
        //    game.doThrow((int)(aim.Margin.Left + aim.Width / 2 - drotik1.Width / 2));
        //    aimStoryboard.Resume(aim);
        //    //Нужно перенести в другое место, чтобы выводит чей бросок
        //    ansLabel.Content = game.curTry.points;
        //    playersScores.Items.Refresh();

        //}
        public void stopClick(object sender, RoutedEventArgs e)
        {
            switch (curScale)
            {
                case 0:
                    aimStoryboard.Pause(aim);
                    powerStoryboard.Begin(powerArrow, true);
                    curScale++;
                    break;
                case 1:
                    powerStoryboard.Pause(powerArrow);
                    cornerStoryboard.Begin(cornerArrow, true);
                    //game.setPower(constnats.leftPower + (powerArrow.Margin.Left - powerGradient.Margin.Left) * ((constnats.rightPower - constnats.leftPower) / powerGradient.Width));
                    curScale++;
                    break;
                case 2:
                    cornerStoryboard.Pause(cornerArrow);
                    //game.setCorner(constnats.leftCorner + (cornerArrow.Margin.Left - cornerGradient.Margin.Left) * ((constnats.rightCorner - constnats.leftCorner) / cornerGradient.Width));
                    curScale = 0;
                    Move();
                    //MakeThrow();


                    break;
            }
        }


    }
}
