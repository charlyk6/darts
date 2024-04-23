using darts.db;
using darts.db.Entities;
using darts.Pages.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private ContextDB db = new ContextDB();
        private int curScale = 0;
        public GamePage()
        {
            InitializeComponent();
            users = db.Users.Where(u => u.IsPlaying).ToList();
            if (!users.Any()) {
            //TODO : вывести предупреждение и сделать редирект на страницу настроек
            }
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
        void beginArrowsAnimation()
        {
            powerStoryboard.Begin(powerArrow, true);
            cornerStoryboard.Begin(cornerArrow, true);
            aimStoryboard.Begin(aim, true);
        }
        void continueArrowsAnimation()
        {
            powerStoryboard.Resume(powerArrow);
            cornerStoryboard.Resume(cornerArrow);
            aimStoryboard.Resume(aim);
        }

        public void start(object sender, RoutedEventArgs e)
        {

            initArrowsAnimation();

            beginArrowsAnimation();
            game = new Game(users);
            game.initDrotiks(drotik1, drotik2, drotik3);
            game.setTarget(target);

        }
        public void throwClick(object sender, RoutedEventArgs e)
        {
            curScale = 0;
            throwButton.IsEnabled = false;
            game.doThrow((int)(aim.Margin.Left + aim.Width / 2 - drotik1.Width / 2));
            continueArrowsAnimation();
            ansLabel.Content = game.curTry.points;

        }
        public void stopClick(object sender, RoutedEventArgs e)
        {
            switch (curScale)
            {
                case 0:
                    aimStoryboard.Pause(aim);
                    break;
                case 1:
                    powerStoryboard.Pause(powerArrow);
                    game.setPower(constnats.leftPower + (powerArrow.Margin.Left - powerGradient.Margin.Left) * ((constnats.rightPower - constnats.leftPower) / powerGradient.Width));
                    break;
                case 2:
                    cornerStoryboard.Pause(cornerArrow);
                    game.setCorner(constnats.leftCorner + (cornerArrow.Margin.Left - cornerGradient.Margin.Left) * ((constnats.rightCorner - constnats.leftCorner) / cornerGradient.Width));
                    throwButton.IsEnabled = true;
                    throwClick(sender, e);
                    curScale = -1;
                    
                    break;
                
            }
            curScale++;
        }
    }
}
