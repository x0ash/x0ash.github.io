using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsStore.Robots
{
    public class SprayRobot : IRobot
    {
        public Int64 Health { get; set; }

        public string GetName()
        {
            return "Spray Robot";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            var aliveCount = competitors.Where(c => c.Health > 0);
            int possibleAttacks = (int) Math.Floor(10.00 / (aliveCount.Count()));
            for (int i = 0; i < aliveCount.Count(); i++)
            {
                Console.WriteLine(possibleAttacks + " - Loop " + i);

                var target = competitors.Where(c => c.Health > 0).ElementAt(i);
                target.Attacks = possibleAttacks;
            }

            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
