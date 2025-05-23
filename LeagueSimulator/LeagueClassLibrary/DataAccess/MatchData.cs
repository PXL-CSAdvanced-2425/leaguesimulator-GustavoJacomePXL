using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeagueClassLibrary.DataAccess
{
    public static class MatchData
    {
        private static DataTable _dataTableMatches;

        public static void InitializeDataTableMatches()
        {
            _dataTableMatches = new DataTable("Matches");

            DataColumn idCol = new DataColumn("Id", typeof(int));
            idCol.AutoIncrement = true;
            idCol.AutoIncrementSeed = 1;
            idCol.AutoIncrementStep = 1;

            _dataTableMatches.Columns.Add(idCol);
            _dataTableMatches.Columns.Add("Code", typeof(string));
            _dataTableMatches.Columns.Add("Winner", typeof(string));
        }


        public static void AddFinishedMatch(Match match)
        {
            string winner = match.Winner == 1 ? "Red" : "Blue";

            DataRow row = _dataTableMatches.NewRow();
            row["Code"] = match.Code;
            row["Winner"] = winner;
            _dataTableMatches.Rows.Add(row);
        }


        public static DataView GetDataViewMatches()
        {
            return _dataTableMatches.DefaultView;
        }

        public static void ExportToXML()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "XML bestanden|*.xml",
                Title = "Sla matches op als XML"
            };

            if (sfd.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    _dataTableMatches.WriteXml(sw);
                }
            }
        }

        public static bool IsUniqueCode(string code)
        {
            foreach (DataRow row in _dataTableMatches.Rows)
            {
                if (row["Code"].ToString().Equals(code, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }


    }
}
