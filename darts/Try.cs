using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace darts
{
    public class Try
    {
        public int throwCnt = 0;
        public int points = 0;
        public List<Drotik> drotiks = new List<Drotik>();
        public Target target = new Target();
        public void initDrotiks(Image dr1, Image dr2, Image dr3)
        {
            drotiks.Clear();
            drotiks.Add(new Drotik(dr1));
            drotiks.Add(new Drotik(dr2));
            drotiks.Add(new Drotik(dr3));
        }
        public void setCorner(double corner)
        {
            if (throwCnt == 3)
            {
                throwCnt = 0;
                foreach (Drotik d in drotiks)
                {
                    d.stayInvisibe();
                }
            }
            drotiks[throwCnt].Throw.corner = corner;
        }
        public void setPower(double power)
        {
            if (throwCnt == 3)
            {
                throwCnt = 0;
                foreach (Drotik d in drotiks)
                {
                    d.stayInvisibe();
                }
            }
            drotiks[throwCnt].Throw.power = power;
        }
        public void doThrow(int x)
        {
            if (throwCnt == 3)
            {
                throwCnt = 0;
                foreach (Drotik d in drotiks)
                {
                    d.stayInvisibe();
                }
            }
            //drotiks[throwCnt].initFlyAnimation();
            //drotiks[throwCnt].beginFlyAnimation();

            drotiks[throwCnt].do_throw(x, target.getY());
            points = target.getPoints((int)(drotiks[throwCnt].getX() + drotiks[throwCnt].drotik.Width/2), (int)(drotiks[throwCnt].getY() + drotiks[throwCnt].drotik.Height/2));
            throwCnt++;
            
        }
    }
}
