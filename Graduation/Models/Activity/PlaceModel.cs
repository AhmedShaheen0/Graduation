using System.Collections.Generic;

namespace Graduation.Models.Activity
{
    public class PlaceModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<ActivityModel> Activities { get; set; } // Navigation property for activities
    }

}
