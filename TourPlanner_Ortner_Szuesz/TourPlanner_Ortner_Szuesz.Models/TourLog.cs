using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Ortner_Szuesz.Models
{
    public class TourLog
    {
        public DateTime Date { get; set; }
        public DifficultyTypes Difficulty { get; set; }
        public int TotalTime { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public TourLog(DateTime date, DifficultyTypes difficulty, int totalTime, int rating, string comment)
        {
            this.Date = date;
            this.Difficulty = difficulty;
            this.TotalTime = totalTime;
            this.Rating = rating;
            this.Comment = comment;
        }
    }
}
