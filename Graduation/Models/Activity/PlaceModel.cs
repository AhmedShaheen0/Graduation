using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graduation.Models.Activity
{
    public class PlaceModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public double Longitude { get; set; }

        public List<ActivityModel> Activities { get; set; } // Navigation property for activities

        public bool IsActive { get; set; }
    }

}
