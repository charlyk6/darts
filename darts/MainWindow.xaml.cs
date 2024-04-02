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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace darts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
        public MainWindow()
        {
            InitializeComponent();
            //start();
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
        void initAimAnimation()
        {
            aim.HorizontalAlignment = HorizontalAlignment.Left;
            
            aimLeftAnimation.From = aim.Margin;
            aimLeftAnimation.To = new Thickness(target.Margin.Left, aim.Margin.Top, 0, 0);
            if(aim.Margin.Left - target.Margin.Left != 0)
            {
                aimLeftAnimation.SpeedRatio = 2 * (target.Width / 2) / (aim.Margin.Left - target.Margin.Left);
            }
            aimRightAnimation.From = aim.Margin;
            aimRightAnimation.To = new Thickness(target.Margin.Left+target.Width-aim.Width, aim.Margin.Top, 0, 0);
            if (target.Margin.Left + target.Width - aim.Width - aim.Margin.Left != 0)
            {
                aimRightAnimation.SpeedRatio = 2 * (target.Width / 2) / (target.Margin.Left + target.Width - aim.Width - aim.Margin.Left);
            }

            Storyboard.SetTargetName(aimLeftAnimation, aim.Name);
            Storyboard.SetTargetProperty(aimLeftAnimation, new PropertyPath(Image.MarginProperty));
            aimLeftStoryboard.Children.Add(aimLeftAnimation);
            Storyboard.SetTargetName(aimRightAnimation, aim.Name);
            Storyboard.SetTargetProperty(aimRightAnimation, new PropertyPath(Image.MarginProperty));
            aimRightStoryboard.Children.Add(aimRightAnimation);
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
            initAimAnimation();
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
        public void keyPress(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    aimLeft(sender, e); break;
                case Key.Right:
                    aimRight(sender, e); break;
            }
        }
        public void aimLeft(object sender, RoutedEventArgs e)
        {
            aimStop(sender, e);
            initAimAnimation();
            aimLeftStoryboard.Begin(aim, true);
        }
        public void aimRight(object sender, RoutedEventArgs e)
        {
            aimStop(sender, e);
            initAimAnimation();
            aimRightStoryboard.Begin(aim, true);
        }
        public void aimStop(object sender, RoutedEventArgs e)
        {
            aimLeftStoryboard.Pause(aim);
            aimRightStoryboard.Pause(aim);
        }
    }

}
