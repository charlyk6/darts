using darts.Pages.Settings;
using System;
using System.Collections.Generic;
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

        Storyboard powerStoryboard = new Storyboard();
        Storyboard cornerStoryboard = new Storyboard();

        Constants constnats = new Constants();
        public List<UserModel> users { get; set; }

        public GamePage()
        {
            InitializeComponent();
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
        }
        void beginArrowsAnimation()
        {
            powerStoryboard.Begin(powerArrow, true);
            cornerStoryboard.Begin(cornerArrow, true);
        }

        public void start(object sender, RoutedEventArgs e)
        {

            initArrowsAnimation();

            beginArrowsAnimation();
            game = new Game(users);
            game.initDrotiks(drotik1, drotik2, drotik3);
            game.setTarget(target);

        }
        public void powerClick(object sender, RoutedEventArgs e)
        {
            powerStoryboard.Pause(powerArrow);
            game.setPower(constnats.leftPower + (powerArrow.Margin.Left - powerGradient.Margin.Left) * ((constnats.rightPower - constnats.leftPower) / powerGradient.Width));
        }
        public void cornerClick(object sender, RoutedEventArgs e)
        {
            cornerStoryboard.Pause(cornerArrow);
            game.setCorner(constnats.leftCorner + (cornerArrow.Margin.Left - cornerGradient.Margin.Left) * ((constnats.rightCorner - constnats.leftCorner) / cornerGradient.Width));

        }
        public void throwClick(object sender, RoutedEventArgs e)
        {
            game.doThrow((int)(aim.Margin.Left + aim.Width / 2 - drotik1.Width / 2));
            beginArrowsAnimation();
            ansLabel.Content = game.curTry.points;
        }
        public void aimChange(object sender, RoutedEventArgs e)
        {
            var curPos = Mouse.GetPosition(this);
            var leftX = target.Margin.Left;
            var rightX = target.Margin.Left + target.Width;
            if(curPos.X <= rightX && curPos.X >= leftX)
            {
                aim.Margin = new Thickness(curPos.X - aim.Width / 2, aim.Margin.Top, 0, 0);
            }
        }
    }
}
