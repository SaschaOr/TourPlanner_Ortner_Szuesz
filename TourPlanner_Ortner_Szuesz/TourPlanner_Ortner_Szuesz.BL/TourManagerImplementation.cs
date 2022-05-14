using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.DAL.REST;
using TourPlanner_Ortner_Szuesz.DAL.SqlServer;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public class TourManagerImplementation : ITourManager
    {
        public Tour CreateItem(string name, string description, string startLocation, string endLocation, TransportTypes transportType)
        {
            //ITourDAO tourDAO = DALFactory.CreateTourDAO();
            ITourDAO tourDAO = new TourSqlDAO();
            return tourDAO.AddNewItem(name, description, startLocation, endLocation, transportType);
        }

        public IEnumerable<Tour> GetItems()
        {
            //ITourDAO tourDAO = DALFactory.CreateTourDAO();
            ITourDAO tourDAO = new TourSqlDAO();
            return tourDAO.GetItems();
        }

        private async Task<Tour> GetDistanceAndTimeFromTour(Tour tourItem)
        {
            var httpRequest = new HttpRequest(new HttpClient());

            try
            {
                tourItem = await httpRequest.GetTourFromRequest(tourItem);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }

            // mapquest api can not resolve location
            if(tourItem.Distance == 0 || tourItem.EstimatedTime == 0)
            {
                throw new NullReferenceException();
            }

            return tourItem;
        }

        private async Task<Tour> SaveImageInFileSystem(Tour tourItem)
        {
            var httpRequest = new HttpRequest(new HttpClient());

            try
            {
                // get image bytes
                var tourBytes = await httpRequest.GetTourImageFromRequest(tourItem);

                // define path of image
                tourItem.RouteImagePath = Path.Combine("Resources\\tours", $"{tourItem.Id}_{tourItem.Name}.png");

                // save image bytes as png image in file system
                File.WriteAllBytes(tourItem.RouteImagePath, tourBytes);

                // save image path in database


                return tourItem;
            }
            catch(NullReferenceException)
            {
                throw new NullReferenceException();
            }
        }
    }
}
