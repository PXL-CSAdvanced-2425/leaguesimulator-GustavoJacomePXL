using LeagueClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueClassLibrary.DataAccess
{
    public static class ChampionData
    {
        private static DataTable _dataTableChampions;
        private static Random r = new Random();

        public static void LoadCSV(string padNaarCsv)
        {
            _dataTableChampions = new DataTable();
            _dataTableChampions.Columns.Add("name", typeof(string));
            _dataTableChampions.Columns.Add("release", typeof(int));
            _dataTableChampions.Columns.Add("position1", typeof(string));
            _dataTableChampions.Columns.Add("position2", typeof(string)).AllowDBNull = true;
            _dataTableChampions.Columns.Add("position3", typeof(string)).AllowDBNull = true;

            using (StreamReader sr = new StreamReader(padNaarCsv))
            {
                string line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    DataRow row = _dataTableChampions.NewRow();

                    row["name"] = parts[0];
                    row["release"] = int.Parse(parts[1]);
                    row["position1"] = parts[2];
                    row["position2"] = string.IsNullOrWhiteSpace(parts[3]) ? DBNull.Value : parts[3];
                    row["position3"] = string.IsNullOrWhiteSpace(parts[4]) ? DBNull.Value : parts[4];

                    _dataTableChampions.Rows.Add(row);
                }
            }
        }

        public static DataView GetDataViewChampion()
        {
            return _dataTableChampions.DefaultView;
        }

        public static DataView GetDataViewChampionsByPosition(string position)
        {
            string filter = $"position1 = '{position}' OR position2 = '{position}' OR position3 = '{position}'";
            DataView dv = new DataView(_dataTableChampions);
            dv.RowFilter = filter;
            return dv;
        }

        public static DataView GetDataViewChampionsBestToWorst()
        {
            if (!_dataTableChampions.Columns.Contains("positionCount"))
            {
                _dataTableChampions.Columns.Add("positionCount", typeof(int));

                foreach (DataRow row in _dataTableChampions.Rows)
                {
                    int count = 1;
                    if (row["position2"] != DBNull.Value) count++;
                    if (row["position3"] != DBNull.Value) count++;
                    row["positionCount"] = count;
                }
            }

            DataView dv = new DataView(_dataTableChampions);
            dv.Sort = "release DESC, positionCount DESC, name ASC";
            return dv;
        }

        public static Champion GetRandomChampionByPosition(string position)
        {
            DataView dv = GetDataViewChampionsByPosition(position);
            if (dv.Count == 0) return null;

            int index = r.Next(dv.Count);
            DataRow row = dv[index].Row;

            string name = row["name"].ToString();
            int release = (int)row["release"];
            string pos1 = row["position1"].ToString();
            string pos2 = row["position2"] == DBNull.Value ? null : row["position2"].ToString();
            string pos3 = row["position3"] == DBNull.Value ? null : row["position3"].ToString();

            List<Ability> abilities = AbilityData.GetAbilitiesByChampionName(name);
            return new Champion(name, release, pos1, pos2, pos3, abilities);
        }



    }
}
