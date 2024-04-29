﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace darts
{
    public class Drotik
    { 
        public Throw Throw = new Throw();        
        public double x;
        public double y;
        
        private static readonly double rsize = 3;
        private Image _drotikImage { get; set; }

        public Storyboard flyStoryBoard = new Storyboard();
        public ThicknessAnimationUsingKeyFrames flyAnimation = new ThicknessAnimationUsingKeyFrames();
        public DoubleAnimation flyAnimationWidth = new DoubleAnimation();
        public DoubleAnimation flyAnimationHeight = new DoubleAnimation();
        public ThicknessAnimation flyAnimationDelta = new ThicknessAnimation();
        public int Points { get; set; }


        public Drotik(Image drotikImage)
        {
            _drotikImage = drotikImage;
        }
        public void MakeThrow(int x, int y0)
        {
            Throw.init();
            _drotikImage.Margin = new Thickness(x + _drotikImage.Width / 2 - _drotikImage.Width * rsize / 2, Throw.normalizeY(Throw.f(0), y0), 0, 0);
            InitFlyAnimation(y0);
            beginFlyAnimation();
            StayVisible();
            _drotikImage.Margin = new Thickness(x, Throw.normalizeY(Throw.f(Throw.time), y0), 0, 0);
            this.x = x;
            this.y = Throw.normalizeY(Throw.f(Throw.time), y0);
        }
        
        public void StayInvisibe()
        {
            _drotikImage.Visibility = Visibility.Hidden;
        }
        public void StayVisible()
        {
            _drotikImage.Visibility = Visibility.Visible;

        }
        public void InitFlyAnimation(int y0)
        {
            int timeAnimation = 700;
            int frames = 100;
            flyAnimation.Duration = TimeSpan.FromMilliseconds(timeAnimation);
            flyAnimation.KeyFrames.Clear();

            for (int i = 0; i < frames; i++)
            {
                double curWidth = _drotikImage.Width * rsize - (_drotikImage.Width * rsize - _drotikImage.Width) * i / frames;
                double curx = _drotikImage.Margin.Left + _drotikImage.Width * rsize / 2 - curWidth / 2;
                flyAnimation.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(curx, Throw.normalizeY(Throw.f(i * Throw.time / frames),y0), 0, 0)));
            }
            flyAnimation.FillBehavior = FillBehavior.Stop;

            Storyboard.SetTargetName(flyAnimation, _drotikImage.Name);
            Storyboard.SetTargetProperty(flyAnimation, new PropertyPath(Image.MarginProperty));
            
            flyStoryBoard.Children.Add(flyAnimation);

            flyAnimationWidth.Duration = TimeSpan.FromMilliseconds(timeAnimation);
            flyAnimationWidth.From = _drotikImage.Width * rsize;
            flyAnimationWidth.To = _drotikImage.Width;
            flyAnimationWidth.FillBehavior = FillBehavior.Stop;

            Storyboard.SetTargetName(flyAnimationWidth, _drotikImage.Name);
            Storyboard.SetTargetProperty(flyAnimationWidth, new PropertyPath(Image.WidthProperty));

            flyAnimationHeight.Duration = flyAnimationWidth.Duration;
            flyAnimationHeight.From = flyAnimationWidth.From;
            flyAnimationHeight.To = flyAnimationWidth.To;
            flyAnimationHeight.FillBehavior = FillBehavior.Stop;
            

            Storyboard.SetTargetName(flyAnimationHeight, _drotikImage.Name);
            Storyboard.SetTargetProperty(flyAnimationHeight, new PropertyPath(Image.HeightProperty));

            flyStoryBoard.Children.Add(flyAnimationWidth);
            flyStoryBoard.Children.Add(flyAnimationHeight);


        }
        public void beginFlyAnimation()
        {
            flyStoryBoard.Begin(_drotikImage, true);
        }
    }
}
