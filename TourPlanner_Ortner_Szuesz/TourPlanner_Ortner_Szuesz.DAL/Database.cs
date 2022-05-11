using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.DAL
{
    class Database : IDataAccess
    {
        private string connectionString;

        public Database()
        {
            // get connection data e.g. from config file
            connectionString = "...";
            // establish connection with db
        }
        public List<Tour> GetTours()
        {
            // select sql query

            return new List<Tour>() {
                new Tour("Tour3"),
                new Tour("Tour4")
            };
        }
    }
}
