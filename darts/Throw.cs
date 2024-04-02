using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace darts
{
    public class Throw
    {
        public double power { get; set; }
        public double corner { get; set; }
        public double time = 0; 
        Constants constants = new Constants();
        
        public void init()
        {
            time = constants.h / power / Math.Cos(corner);
        }
        public double f(double t)
        {
            return (Math.Sin(corner) * power * t - constants.g * t * t / 2) / 0.45 * 320;
        }
        public double normalizeY(double y, double y0)
        {
            return y0 - y;
        }
    }
}
