using System;
using System.ComponentModel.DataAnnotations;

namespace Graduation.Models.Activity
{
    public class ActivityViewModel
    {
              public int ID { get; set; } = 0; // Provide default value

            [Required]
            [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
            public string Username { get; set; }

            public DateTime Date { get; set; } 
            public string Time { get; set; }
            public int Duration { get; set; }

        [Required]
            [StringLength(100, ErrorMessage = "Activity name cannot be longer than 100 characters.")]
            public string Name { get; set; }

            [Required]
            public PlaceViewModel Place { get; set; } // Instead of PlaceModel, use PlaceViewModel for place details

            public bool IsActive { get; set; } = true; // Provide default value
        }
    }

