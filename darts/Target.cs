using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace darts
{
    public static class Target
    {
        public static bool IsInTarget(Image target, Drotik drotik)
        {
            var x = drotik.CenterX;
            var y = drotik.CenterY;
            y = GetY(target) - y + (int)target.Height / 2;
            x -= GetX(target);
            double r = Math.Sqrt(x * x + y * y);
            if (r > 175)
            {
                return false;
            }
            return true;
        }
        public static ThrowResult GetPoints(Image target, Drotik drotik)
        {
            var x = drotik.CenterX;
            var y = drotik.CenterY;
            y = GetY(target) - y + (int)target.Height / 2;
            x -= GetX(target);
            double r = Math.Sqrt(x * x + y * y);
            var throwResult = new ThrowResult();
            throwResult.mult = 1;
            if (r <= 8)
            {
                throwResult.points = 50;
                throwResult.mult = 2;
                return throwResult;
            }
            if (r <= 15)
            {
                throwResult.points = 25;
                throwResult.mult = 1;
                return throwResult;
            }
            List<int> sectors = new List<int> { 6, 13, 13, 4, 4, 18, 18, 1, 1, 20, 20, 5, 5, 12, 12, 9, 9, 14, 14, 11, 11, 8, 8, 16, 16, 7, 7, 19, 19, 3, 3, 17, 17, 2, 2, 15, 15, 10, 10, 6 };
            double corn = Math.Atan2(y, x) / Math.PI * 180;
            if (corn < 0)
            {
                corn += 360;
            }
            throwResult.points = sectors[(int)corn / 9];
            if (r <= 89 && r >= 76)
            {
                throwResult.mult = 3;
            }
            else if (r <= 145 && r >= 130)
            {
                throwResult.mult = 2;
            }
            else if (r > 145)
            {
                throwResult.mult = 0;
            }
            throwResult.points *= throwResult.mult;
            drotik.Points = throwResult.points;

            return throwResult;
        }
        private static int GetX(Image target)
        {
            return (int)(target.Margin.Left + target.Width / 2);
        }
        private static int GetY(Image target)
        {
            return (int)target.Margin.Top;
        }
    }
    public struct ThrowResult
    {
        public int points { get; set; }
        public int mult { get; set; }
    }
}
