using LeagueClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueClassLibrary.Entities
{
    public class SummonersRift : Match
    {
        private ChampionData _championData;

        public SummonersRift(string code, ChampionData championData) : base(code)
        {
            _championData = championData;
        }

        public override void GenerateTeams()
        {
            string[] roles = { "sup", "mid", "jung", "bot", "top" };

            foreach (string role in roles)
            {
                Team1Champions.Add(_championData.GetRandomChampionByPosition(role));
                Team2Champions.Add(_championData.GetRandomChampionByPosition(role));
            }
        }
    }

}
