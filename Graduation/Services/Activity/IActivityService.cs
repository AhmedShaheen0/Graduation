
using System.Collections.Generic;
using Graduation.Models.Activity;
using Graduation.Models.Auth;

namespace Graduation.Services.Activity
{
    public interface IActivityService
    {
        IEnumerable<PlaceModel> GetPlacesByUserName(string username);
        IEnumerable<PlaceModel> GetAllPlaces();
        List<ActivityViewModel> GetEvents();
        ActivityModel CreateEvent(ActivityViewModel ev, ApplicationUser user)   ;
        PlaceModel GetPlaceByNamePlace(string name);
        ActivityViewModel GetEventById(int id);
        List<ActivityViewModel> GetUserActivities(string username);
        string toggel_Activity(int activity_id);
        string toggel_Place(int place_id);


    }
}
