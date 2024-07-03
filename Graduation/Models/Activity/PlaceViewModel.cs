using System.ComponentModel.DataAnnotations;

namespace Graduation.Models.Activity
{
    public class PlaceViewModel
    {
        public int Id { get; set; } = 0;

        [Required]
        [StringLength(100, ErrorMessage = "Place name cannot be longer than 100 characters.")]
        public string placeName { get; set; }

        [Required]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public double Longitude { get; set; }

        public bool IsActive { get; set; }
    }
}
