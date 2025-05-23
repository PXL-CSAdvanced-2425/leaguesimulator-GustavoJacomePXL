using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueClassLibrary.Entities
{
    public abstract class Match : IWinnable
    {
        public List<Champion> Team1Champions { get; protected set; }
        public List<Champion> Team2Champions { get; protected set; }
        public int Winner { get; set; }
        public string Code { get; set; }

        public Match(string code)
        {
            Code = code;
            Team1Champions = new List<Champion>();
            Team2Champions = new List<Champion>();
        }

        public abstract void GenerateTeams();

        public void DecideWinner()
        {
            double avg1 = Team1Champions.Average(c => c.ReleaseYear);
            double avg2 = Team2Champions.Average(c => c.ReleaseYear);

            if (avg1 > avg2)
            {
                Winner = 1;
            }
            else if (avg2 > avg1)
            {
                Winner = 2;
            }
            else
            {
                int assassins1 = Team1Champions.Count(c => c.Class == "Assassin");
                int assassins2 = Team2Champions.Count(c => c.Class == "Assassin");

                if (assassins1 > assassins2)
                    Winner = 1;
                else if (assassins2 > assassins1)
                    Winner = 2;
                else
                    Winner = 1;
            }
        }
    }

}
