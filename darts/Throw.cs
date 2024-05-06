using System;

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
            time = constants.S / power / Math.Cos(corner);
        }
        /// <summary>
        /// функция координаты по Y от времени
        /// </summary>
        /// <param name="t">время</param>
        /// <returns></returns>
        public double f(double t)
        {
            return (Math.Sin(corner) * power * t - constants.g * t * t / 2) / 0.45 * 320;
        }
        public double normalizeY(double y, double y0)
        {
            return y0 - y;
            // кто прочитал тот сдохнет
        }
    }
}
