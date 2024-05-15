using System;

namespace Graduation.Models.Activity
{
    public class ActivityViewModel
    {
        public string Username { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public PlaceViewModel Place { get; set; } // Instead of PlaceModel, use string for place name
    }


}
