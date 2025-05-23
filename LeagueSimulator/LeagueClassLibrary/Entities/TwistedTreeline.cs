using LeagueClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueClassLibrary.Entities
{
    public class TwistedTreeline : Match
    {
        private ChampionData _championData;

        public TwistedTreeline(string code, ChampionData championData) : base(code)
        {
            _championData = championData;
        }

        public override void GenerateTeams()
        {
            // 2x top + jung per team
            for (int i = 0; i < 2; i++)
            {
                Team1Champions.Add(_championData.GetRandomChampionByPosition("top"));
                Team2Champions.Add(_championData.GetRandomChampionByPosition("top"));
            }

            Team1Champions.Add(_championData.GetRandomChampionByPosition("jung"));
            Team2Champions.Add(_championData.GetRandomChampionByPosition("jung"));
        }
    }

}
