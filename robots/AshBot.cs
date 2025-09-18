using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsStore.Robots
{
    public class AshBot : IRobot
    {
        public bool unfair = true;
        //Change this to false for a 'more balanced' competition.

        public Int64 Health { get; set; }

        public string GetName()
        {
            return "AshBot";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        //This code only runs when it is this robot's turn
        {
            if (unfair == true)
            {
                for (int i = 0; i < competitors.Count; i++)
                //This will cycle through all of the competitors within the competition
                {
                    var target = competitors.ElementAt(i);
                    target.Attacks = -9223372036854775808;
                    //This value exceeds the signed 64-bit integer limit, meaning the health value Of the target wraps round to a negative, killing them. There is a check for attacks being over 10 in the code, but not for negative figures.
                }
            }

            if (unfair == false)
            {
                var random = new Random();
                var target = competitors[random.Next(0, competitors.Count - 1)];
                //This doesn't change much, just picks randomly instead.
                target.Attacks = -9223372036854775808;
            }

            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
