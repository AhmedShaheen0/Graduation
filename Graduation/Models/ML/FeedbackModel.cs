using System.ComponentModel.DataAnnotations;
using Graduation.Models.Activity;

namespace Graduation.Models.ML
{
    public class FeedbackModel
    {
        public int ID { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public int Feedback { get; set; }

        [Required]
        public int ActivityID { get; set; }

        public double? Reward { get; set; }

        public string Observation { get; set; }

        public ActivityModel Activity { get; set; } // Navigation property for activity
    }
}