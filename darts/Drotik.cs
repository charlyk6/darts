using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace darts
{
    public class Drotik
    {
        public Image drotik = new Image();
        public Throw Throw = new Throw();
        public Storyboard flyStoryBoard = new Storyboard();

        public ThicknessAnimationUsingKeyFrames flyAnimation = new ThicknessAnimationUsingKeyFrames();
        public DoubleAnimation flyAnimationWidth = new DoubleAnimation();
        public DoubleAnimation flyAnimationHeight = new DoubleAnimation();

        public ThicknessAnimation flyAnimationDelta = new ThicknessAnimation();
        double rsize = 3;

        public Drotik(Image dr)
        {
            drotik = dr;
        }
        public void do_throw(int x, int y0)
        {
            Throw.init();
            drotik.Margin = new Thickness(x + drotik.Width / 2 - drotik.Width * rsize / 2, Throw.normalizeY(Throw.f(0), y0), 0, 0);
            initFlyAnimation(y0);
            beginFlyAnimation();
            stayVisible();
            drotik.Margin = new Thickness(x, Throw.normalizeY(Throw.f(Throw.time), y0), 0, 0);

        }
        public int getX()
        {
            return (int)drotik.Margin.Left;
        }
        public int getY()
        {
            return (int)drotik.Margin.Top;
        }
        public void stayInvisibe()
        {
            drotik.Visibility = Visibility.Hidden;
        }
        public void stayVisible()
        {
            drotik.Visibility = Visibility.Visible;

        }
        public void initFlyAnimation(int y0)
        {
            int timeAnimation = 700;
            int frames = 100;
            flyAnimation.Duration = TimeSpan.FromMilliseconds(timeAnimation);
            flyAnimation.KeyFrames.Clear();

            for (int i = 0; i < frames; i++)
            {
                double curWidth = drotik.Width * rsize - (drotik.Width * rsize - drotik.Width) * i / frames;
                double curx = drotik.Margin.Left + drotik.Width * rsize / 2 - curWidth / 2;
                flyAnimation.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(curx, Throw.normalizeY(Throw.f(i * Throw.time / frames),y0), 0, 0)));
            }
            flyAnimation.FillBehavior = FillBehavior.Stop;

            Storyboard.SetTargetName(flyAnimation, drotik.Name);
            Storyboard.SetTargetProperty(flyAnimation, new PropertyPath(Image.MarginProperty));
            
            flyStoryBoard.Children.Add(flyAnimation);

            flyAnimationWidth.Duration = TimeSpan.FromMilliseconds(timeAnimation);
            flyAnimationWidth.From = drotik.Width * rsize;
            flyAnimationWidth.To = drotik.Width;
            flyAnimationWidth.FillBehavior = FillBehavior.Stop;

            Storyboard.SetTargetName(flyAnimationWidth, drotik.Name);
            Storyboard.SetTargetProperty(flyAnimationWidth, new PropertyPath(Image.WidthProperty));

            flyAnimationHeight.Duration = flyAnimationWidth.Duration;
            flyAnimationHeight.From = flyAnimationWidth.From;
            flyAnimationHeight.To = flyAnimationWidth.To;
            flyAnimationHeight.FillBehavior = FillBehavior.Stop;
            

            Storyboard.SetTargetName(flyAnimationHeight, drotik.Name);
            Storyboard.SetTargetProperty(flyAnimationHeight, new PropertyPath(Image.HeightProperty));

            flyStoryBoard.Children.Add(flyAnimationWidth);
            flyStoryBoard.Children.Add(flyAnimationHeight);


        }
        public void beginFlyAnimation()
        {
            flyStoryBoard.Begin(drotik, true);
        }
    }
}
