using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsStore.Robots
{
    public class SmartRobot : IRobot
    {
        public Int64 Health { get; set; }

        public string GetName()
        {
            return "Smart Robot";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            var aliveCompetitors = competitors.Where(c => c.Health != 0).OrderBy(c => c.Health);

            Console.WriteLine("Lowest Health - " + aliveCompetitors.ElementAt(0).Name + ", Highest Health - " + aliveCompetitors.ElementAt(aliveCompetitors.Count() - 1).Name);

            if (aliveCompetitors.ElementAt(0).Health > this.Health)
            {
                Console.WriteLine("I currently have the lowest health. This means I'm targetting the bot above me.");
                var target = aliveCompetitors.ElementAt(1);
                Console.WriteLine("Targetting " + target.Name);
                target.Attacks = 10;
            }

            else if (aliveCompetitors.ElementAt(aliveCompetitors.Count() - 1).Health < this.Health)
            {
                Console.WriteLine("I'm currently at the top of the leaderboard. I will want to figure out what I would like to do.");
                int random = new Random().Next(0, 2);
                if (random == 0)
                {
                    Console.WriteLine("I've decided to do nothing- I feel this may be a good strategy.");
                }

                else if (random == 1)
                {
                    Console.WriteLine("I've decided to target a random opponent. This may work.");
                    var randomTarget = aliveCompetitors.ElementAt(new Random().Next(0, aliveCompetitors.Count() - 1));
                    Console.WriteLine("Targetting " + randomTarget.Name);
                    randomTarget.Attacks = 10;
                }

                else if (random == 2)
                {
                    bool compassionateExists = false;
                    for (int i = 0; i < aliveCompetitors.Count(); i++)
                    {
                        if (aliveCompetitors.ElementAt(i).Name == "Compassionate Robot")
                        {
                            compassionateExists = true;
                        }
                    }

                    if (compassionateExists == true)
                    {
                        Console.WriteLine("I know the behaviour of Compassionate Robot, and I've detected that it exists. As a result, I am targetting it.");
                        var target = aliveCompetitors.Where(c => c.Name == "Compassionate Robot").ElementAt(0);
                        Console.WriteLine("Targetting " + target.Name);
                        target.Attacks = 10;
                    }

                    else
                    {

                    }
                }
            }

            else
            {
                Console.WriteLine("I'm neither at the top nor the bottom of the leaderboard. I will need to think about what to do.");
                int random = new Random().Next(0, 5);
                Console.WriteLine(random);
                if (random == 0)
                {
                    Console.WriteLine("I've decided that I should target a random opponent. This may work in my favour.");
                    var target = aliveCompetitors.ElementAt(new Random().Next(0, aliveCompetitors.Count() - 1));
                    Console.WriteLine("Targetting " + target.Name);
                    target.Attacks = 10;
                }

                else if (random >= 1)
                {
                    Console.WriteLine("I've decided to attack the highest in terms of health. This will hopefully decrease their chances of winning.");
                    var target = aliveCompetitors.ElementAt(aliveCompetitors.Count() - 1);
                    Console.WriteLine("Targetting " + target.Name);
                    target.Attacks = 10;
                }
            }

            Console.WriteLine("\n");
            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
