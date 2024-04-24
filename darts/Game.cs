using darts.db.Entities;
using darts.Models;
using darts.Pages.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace darts
{
    public class Game
    {
        public ObservableCollection<PlayerScoreModel> playerScores { get; set; }
        public Try curTry = new Try();
        Image? drotik1, drotik2, drotik3;
        public int curUser = 0;
        public int curThrow = 0;
        public Game(ObservableCollection<PlayerScoreModel> us) {
            playerScores = us;
        }
       
        public void initDrotiks(Image d1, Image d2, Image d3)
        {
            drotik1 = d1;
            drotik2 = d2;
            drotik3 = d3;
            curTry.initDrotiks(drotik1, drotik2, drotik3);
        }
        public void setTarget(Image target)
        {
            curTry.target.target = target;
        }
        public void setPower(double power)
        {
            curTry.setPower(power);
        }
        public void setCorner(double corner)
        {
            curTry.setCorner(corner);
        }
        public void doThrow(int x)
        {
            
            if(curThrow == 3)
            {
                curThrow = 0;
                curUser++;
                curUser %= playerScores.Count;
            }
            curTry.doThrow(x);
            playerScores[curUser].LastThrow = curTry.points;
            playerScores[curUser].NumberThrow++;
            playerScores[curUser].Points -= curTry.points;

            curThrow++;
        }
    }
}
