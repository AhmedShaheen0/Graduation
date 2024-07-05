using System.ComponentModel.DataAnnotations;

namespace Graduation.Models.ML
{
    public class FeedbackViewModel
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
    }
}