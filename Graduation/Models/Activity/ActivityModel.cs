using System;
using System.ComponentModel.DataAnnotations;
using Graduation.Models.Auth;

namespace Graduation.Models.Activity
{
    public class ActivityModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Activity name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public DateTime Date { get; set; } 
        public string  Time { get; set; }
        public int Duration { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public ApplicationUser User { get; set; } // Navigation property for user

        [Required]
        public PlaceModel Place { get; set; } // Navigation property for places

        public bool IsActive { get; set; }
    }

}
