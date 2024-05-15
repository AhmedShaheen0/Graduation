using System;
using Graduation.Models.Auth;

namespace Graduation.Models.Activity
{
    public class ActivityModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Userid { get; set; }
        public ApplicationUser User { get; set; } // Navigation property for user
        public PlaceModel Place { get; set; } // Navigation property for places
    }

}
