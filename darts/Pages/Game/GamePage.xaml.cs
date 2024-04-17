using System;
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
        Try curTry = new Try();
        ThicknessAnimation powerAnimation = new ThicknessAnimation();
        ThicknessAnimation cornerAnimation = new ThicknessAnimation();
        ThicknessAnimation aimLeftAnimation = new ThicknessAnimation();
        ThicknessAnimation aimRightAnimation = new ThicknessAnimation();

        Storyboard powerStoryboard = new Storyboard();
        Storyboard cornerStoryboard = new Storyboard();
        Storyboard aimLeftStoryboard = new Storyboard();
        Storyboard aimRightStoryboard = new Storyboard();

        Constants constnats = new Constants();
        public GamePage()
        {
            InitializeComponent();
        }

        void initCornerAnimation()
        {
            powerAnimation.From = powerArrow.Margin;
            powerAnimation.To = new Thickness(powerGradient.Width + powerGradient.Margin.Left - powerArrow.Width, 450, 0, 0);
            powerAnimation.RepeatBehavior = RepeatBehavior.Forever;
            powerAnimation.AutoReverse = true;
            Storyboard.SetTargetName(powerAnimation, powerArrow.Name);
            Storyboard.SetTargetProperty(powerAnimation, new PropertyPath(Image.MarginProperty));
            powerStoryboard.Children.Add(powerAnimation);

            cornerAnimation.From = cornerArrow.Margin;
            cornerAnimation.To = new Thickness(cornerGradient.Width + cornerGradient.Margin.Left - cornerArrow.Width, 518, 0, 0);
            cornerAnimation.RepeatBehavior = RepeatBehavior.Forever;
            cornerAnimation.AutoReverse = true;
            Storyboard.SetTargetName(cornerAnimation, cornerArrow.Name);
            Storyboard.SetTargetProperty(cornerAnimation, new PropertyPath(Image.MarginProperty));
            cornerStoryboard.Children.Add(cornerAnimation);
        }
        void beginAnimation()
        {
            powerStoryboard.Begin(powerArrow, true);
            cornerStoryboard.Begin(cornerArrow, true);
        }

        public void start(object sender, RoutedEventArgs e)
        {

            initCornerAnimation();

            beginAnimation();
            curTry.initDrotiks(drotik1, drotik2, drotik3);
            curTry.target.target = target;
        }
        public void powerClick(object sender, RoutedEventArgs e)
        {
            powerStoryboard.Pause(powerArrow);
            curTry.setPower(constnats.leftPower + (powerArrow.Margin.Left - powerGradient.Margin.Left) * ((constnats.rightPower - constnats.leftPower) / powerGradient.Width));
            powerLabel.Content = constnats.leftPower + (powerArrow.Margin.Left - powerGradient.Margin.Left) * ((constnats.rightPower - constnats.leftPower) / powerGradient.Width);
        }
        public void cornerClick(object sender, RoutedEventArgs e)
        {
            cornerStoryboard.Pause(cornerArrow);
            curTry.setCorner(constnats.leftCorner + (cornerArrow.Margin.Left - cornerGradient.Margin.Left) * ((constnats.rightCorner - constnats.leftCorner) / cornerGradient.Width));
            cornerLabel.Content = constnats.leftCorner + (cornerArrow.Margin.Left - cornerGradient.Margin.Left) * ((constnats.rightCorner - constnats.leftCorner) / cornerGradient.Width);

        }
        public void throwClick(object sender, RoutedEventArgs e)
        {
            curTry.doThrow((int)(aim.Margin.Left + aim.Height / 2 - drotik1.Height / 2));
            beginAnimation();
            ansLabel.Content = curTry.points;
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
