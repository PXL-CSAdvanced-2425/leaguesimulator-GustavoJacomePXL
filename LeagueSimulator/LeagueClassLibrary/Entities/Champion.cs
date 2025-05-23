using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueClassLibrary.Entities
{
    public class Champion
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public int ReleaseYear { get; set; }
        public List<Abilities> Abilities { get; set; }
        public List<string> Positions { get; set; }
        public string IconSource { get; set; }
        public string BannerSource { get; set; }
        public int CostIP { get; set; }
        public int CostRP { get; set; }

        public Champion(string Name, string Title, string Class, int ReleaseYear, List<Abilities> Abilities, List<string> Positions, string IconSource, string BannerSource, int CostIP, int CostRP)
        {
                
        }

        public override string ToString()
        {
            return $"{Name} {Title}";
        }

        public string GetCost()
        {
            return $"RP: {CostRP} / IP: {CostIP}";
        }
    }
}
