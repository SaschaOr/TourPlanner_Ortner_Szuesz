using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.DAO;

namespace TourPlanner_Ortner_Szuesz.DAL.Common
{
    public class DALFactory
    {
        private static string assemblyName;
        private static Assembly dalAssembly;
        private static IDatabase database;

        // load DAL assembly
        static DALFactory()
        {
            //assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];
            assemblyName = "TourPlanner_Ortner_Szuesz.DAL.SqlServer";
            Console.WriteLine("Test1");
            dalAssembly = Assembly.Load(assemblyName);
            Console.WriteLine("Test2");
        }

        // create database object with connection string from config
        public static IDatabase GetDatabase()
        {
            if (database == null)
            {
                database = CreateDatabase();
            }
            return database;
        }

        private static IDatabase CreateDatabase()
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["PostgresSQLConnectionString"].ConnectionString;
            string connectionString = "Server=127.0.0.1;Port=5432;Database=tourplanner;User Id=postgres;Password=password";
            return CreateDatabase(connectionString);
        }

        // create database object with specific connection string
        private static IDatabase CreateDatabase(string connectionString)
        {
            string databaseClassName = assemblyName + ".Database";
            Type dbClass = dalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass, new object[] { connectionString }) as IDatabase;
        }

        // create tour sql dao object
        public static ITourDAO CreateTourDAO()
        {
            string className = assemblyName + ".TourSqlDAO";

            Type zoneType = dalAssembly.GetType(className);
            return Activator.CreateInstance(zoneType) as ITourDAO;
        }

        // create tour log sql dao object
        public static ITourLogDAO CreateTourLogDAO()
        {
            string className = assemblyName + ".TourLogSqlDAO";

            Type zoneType = dalAssembly.GetType(className);
            return Activator.CreateInstance(zoneType) as ITourLogDAO;
        }
    }
}
