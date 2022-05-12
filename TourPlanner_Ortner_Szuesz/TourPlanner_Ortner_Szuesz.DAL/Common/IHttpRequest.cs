using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.DAL.Common
{
    public interface IHttpRequest
    {
        public Task<Tour> GetTourFromRequest(Tour tourItem);
        public Task<byte[]> GetTourImageFromRequest(Tour tourItem);
    }
}
