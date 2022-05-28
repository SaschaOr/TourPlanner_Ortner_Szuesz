﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public ILogger Logger { get; }
        private ITourDAO tourDAO { get; }
        private ITourLogDAO tourLogDAO { get; }

        public TourManagerImplementation(ILogger logger)
        {
            Logger = logger;
            tourDAO = new TourSqlDAO(Logger);
            tourLogDAO = new TourLogSqlDAO(Logger);
        }

        public IEnumerable<Tour> GetItems()
        {
            //ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.GetItems();
        }

        public IEnumerable<Tour> GetSearchResults(string searchString)
        {
            return tourDAO.GetSearchResults(searchString);
        }
        public async Task<Tour> CreateItem(Tour tourItem)
        {
            //ITourDAO tourDAO = DALFactory.CreateTourDAO
            
            tourItem = await GetDistanceAndTimeFromTour(tourItem);
            tourItem = tourDAO.AddNewItem(tourItem);
            tourItem = await SaveImageInFileSystem(tourItem);

            return tourItem;
        }

        public async Task<Tour> CreateItemAfterImport(Tour tourItem)
        {
            Tour newItem = tourDAO.AddNewItem(tourItem);
            newItem = await SaveImageInFileSystem(newItem);

            // tour has tour logs -> save them
            if(tourItem.TourLogs.Any() && tourItem.TourLogs != null)
            {
                foreach (TourLog logItem in tourItem.TourLogs)
                {
                    logItem.TourId = newItem.Id;
                    tourLogDAO.AddNewItem(logItem);
                }
            }

            return newItem;
        }

        public async Task<Tour> UpdateItem(Tour tourItem)
        {
            tourItem = await GetDistanceAndTimeFromTour(tourItem);
            tourItem = tourDAO.UpdateItem(tourItem);
            tourItem = await SaveImageInFileSystem(tourItem);

            return tourItem;
        }

        public bool DeleteItem(Tour tourItem)
        {
            return tourDAO.DeleteItem(tourItem);
        }

        public bool UpdateFavouriteStatus(int tourId, bool favouriteStatus)
        {
            return tourDAO.UpdateFavouriteStatus(tourId, favouriteStatus);
        }

        public async Task<Tour> GetDistanceAndTimeFromTour(Tour tourItem)
        {
            var httpRequest = new HttpRequest(new HttpClient(), Logger);

            try
            {
                tourItem = await httpRequest.GetTourFromRequest(tourItem);
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }

            // mapquest api can not resolve location
            if(tourItem.Distance == 0 || tourItem.EstimatedTime == 0)
            {
                throw new HttpRequestException();
            }

            return tourItem;
        }

        private async Task<Tour> SaveImageInFileSystem(Tour tourItem)
        {
            var httpRequest = new HttpRequest(new HttpClient(), Logger);

            try
            {
                // get image bytes
                var tourBytes = await httpRequest.GetTourImageFromRequest(tourItem);

                // define path of image
                tourItem.RouteImagePath = Path.Combine("Resources\\tours", $"{tourItem.Id}_{tourItem.Name}.png");

                // save image bytes as png image in file system
                File.WriteAllBytes(tourItem.RouteImagePath, tourBytes);

                // save image path in database
                tourDAO.SetRouteImagePath(tourItem.Id, tourItem.RouteImagePath);

                return tourItem;
            }
            catch(HttpRequestException)
            {
                throw new HttpRequestException();
            }
        }
    }
}
