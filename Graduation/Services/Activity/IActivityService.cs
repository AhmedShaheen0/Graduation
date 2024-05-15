
using System.Collections.Generic;
using Graduation.Models.Activity;

namespace Graduation.Services.Activity
{
    public interface IActivityService
    {
        IEnumerable<PlaceModel> GetPlacesByUserName(string username);
        IEnumerable<PlaceModel> GetAllPlaces();
        List<ActivityViewModel> GetEvents();
        ActivityModel CreateEvent(ActivityModel ev, string userid);
        PlaceModel GetPlaceByNamePlace(string name);
        ActivityViewModel GetEventById(int id);

        List<ActivityViewModel> GetUserActivities(string username);


    }
}
