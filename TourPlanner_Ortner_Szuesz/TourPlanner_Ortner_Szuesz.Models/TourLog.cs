using System;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.Models
{
    public class TourLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DifficultyTypes Difficulty { get; set; }
        public int TotalTime { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int TourId { get; set; }   

        public TourLog(int Id, DateTime date, DifficultyTypes difficulty, int totalTime, int rating, string comment, int tourId)
        {
            this.Id = Id;
            this.Date = date;
            this.Difficulty = difficulty;
            this.TotalTime = totalTime;
            this.Rating = rating;
            this.Comment = comment;
            this.TourId = tourId;
        }
    }
}
