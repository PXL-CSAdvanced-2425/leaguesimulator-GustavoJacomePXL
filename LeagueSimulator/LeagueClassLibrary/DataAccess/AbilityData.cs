using LeagueClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueClassLibrary.DataAccess
{
    public static class AbilityData
    {
        private static List<Ability> _abilities;

        public static void LoadCSV(string padNaarCsv)
        {
            _abilities = new List<Ability>();

            using (StreamReader sr = new StreamReader(padNaarCsv))
            {
                sr.ReadLine(); // header overslaan
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    string AbilityId = parts[0];
                    string championName = parts[1];
                    string abilityName = parts[2];

                    Ability ability = new Ability(AbilityId, championName, abilityName);
                    _abilities.Add(ability);
                }
            }
        }

        public static List GetAbilitiesByChampionName(string championName)
        {

        }

    }
}
