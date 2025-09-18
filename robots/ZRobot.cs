using System;
using RobotWars;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RobotsStore
{
    public class ZRobot : IRobot
    {
        public Int64 Health { get; set; }
        private int attacksLeft = 10;

        public string GetName()
        {
            return "Z Robot";

        }
        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            List<String> firstTargets = new List<String> { "Bully Robot", "Compassionate Robot" };
            var aliveCompetitors = competitors.Where(c => c.Health > 0);
            var aliveCompassionate = competitors.Where(c => c.Name == firstTargets.ElementAt(0)).FirstOrDefault();
            this.attacksLeft = 10;
            if (aliveCompassionate != null)
            {

                if (aliveCompassionate.Health >= 10)
                {
                    aliveCompassionate.Attacks = 10;
                    this.attacksLeft = 0;

                }

                else
                {
                    aliveCompassionate.Attacks = aliveCompassionate.Health;
                    this.attacksLeft -= (int)aliveCompassionate.Health;
                }

            }

            else
            {
                var aliveBully = competitors.Where(c => c.Name == firstTargets.ElementAt(1)).FirstOrDefault();

                if (aliveBully != null)
                {
                    if (aliveBully.Health >= 10)
                    {
                        aliveBully.Attacks = 10;
                        this.attacksLeft = 0;
                    }

                    else
                    {
                        aliveBully.Attacks = aliveBully.Health;
                        this.attacksLeft -= (int)aliveBully.Health;
                    }
                }

                else
                {
                    var aliveCheating = aliveCompetitors.Where(c => c.Name == "Cheating Robot").FirstOrDefault();
                    if (this.attacksLeft > 0)
                    {
                        var targets = competitors.OrderBy(c => c.Health).Where(c => c.Name != "Cheating Robot" && c.Attacks == 0).FirstOrDefault();
                        if (targets != null)
                        {

                            if (targets.Health >= this.attacksLeft)
                            {
                                targets.Attacks = this.attacksLeft;
                            }

                            else
                            {
                                this.attacksLeft -= (int)targets.Health;
                                targets.Attacks = targets.Health;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(this.attacksLeft);
            if (this.attacksLeft > 0)
            {
                var target = competitors.Where(c => c.Attacks == 0).OrderBy(c => c.Health).FirstOrDefault();         //Finding the highest in alphabetical order - always targets Bully Robot if alive. DESTROY IT ALWAYS
                if (target != null)
                {
                    if (target.Health >= 10)
                    {
                        target.Attacks = this.attacksLeft;
                    }

                    else
                    {
                        if (this.attacksLeft > target.Health)
                        {
                            target.Attacks = target.Health;
                        }

                        else
                        {
                            target.Attacks = this.attacksLeft;
                        }
                    }
                }
            }

            foreach (RobotAction robotAction in competitors)
            {
                Console.WriteLine(robotAction.Name + " - " + robotAction.Attacks);
            }
            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }


    }
}
