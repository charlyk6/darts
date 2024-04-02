using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace darts
{
    public class Target
    {
        public Image target = new Image();
        public int getPoints(int x, int y)
        {
            y = getY() - y + (int)target.Height / 2;
            x -= getX();
            double r = Math.Sqrt(x * x + y * y);
            if (r <= 8)
            {
                return 50;
            }
            if (r <= 15)
            {
                return 25;
            }
            int ans = 0;
            List<int> sectors = new List<int> { 6, 13, 13, 4, 4, 18, 18, 1, 1, 20, 20, 5, 5, 12, 12, 9, 9, 14, 14, 11, 11, 8, 8, 16, 16, 7, 7, 19, 19, 3, 3, 17, 17, 2, 2, 15, 15, 10, 10, 6 };
            double corn = Math.Atan2(y, x) / Math.PI * 180;
            if (corn < 0)
            {
                corn += 360;
            }
            ans = sectors[(int)corn / 9];
            if (r <= 87 && r >= 77)
            {
                return ans * 3;
            }
            if (r <= 142 && r >= 131)
            {
                return ans * 2;
            }
            if (r > 142)
            {
                return 0;
            }
            return ans;
        }
        public int getX()
        {
            return (int)(target.Margin.Left + target.Width / 2);
        }
        public int getY()
        {
            return (int)target.Margin.Top;
        }
    }
}
