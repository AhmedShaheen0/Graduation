
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Graduation.Models.Activity;
using Graduation.Services.Auth;

namespace Graduation.Services.Activity
{
    public class ActivityService : IActivityService
    {
        private readonly IAuthService _auth;
        private readonly ApplicationDbContext _context;
        public ActivityService(ApplicationDbContext context, IAuthService auth)
        {
            _context = context;
            _auth = auth;
        }



        public ActivityModel CreateEvent(ActivityModel ev, string userid)
        {
            var model = new ActivityModel
            {
                Userid = userid,
                Date = DateTime.UtcNow,
                Name = ev.Name,
                Place = ev.Place,
                User = ev.User,

            };
            _context.Activities.Add(model);
            _context.SaveChanges();
            return model;
        }

        public ActivityViewModel GetEventById(int id)
        {
            // Fetch the activity using a join with Place
            var activity = _context.Activities
               .Include(x => x.Place)
               .Include(x => x.User)
             .FirstOrDefault(e => e.ID == id);

            if (activity is null)
            {
                return null;
            }


            // Create and return the ActivityViewModel instance
            var activityVM = new ActivityViewModel
            {
                Username = activity.User.UserName,
                Name = activity.Name,
                Date = activity.Date,
                Place = new PlaceViewModel
                {
                    Id = activity.Place.Id,
                    Latitude = activity.Place.Latitude,
                    Longitude = activity.Place.Longitude,
                    Name = activity.Place.Name
                }
            };
            return activityVM;
        }




        public List<ActivityViewModel> GetEvents()
        {
            var activities = _context.Activities
              .Select(activity => new ActivityViewModel
              {
                  Username = activity.User.UserName,
                  Name = activity.Name,
                  Date = activity.Date,
                  Place = new PlaceViewModel
                  {
                      Id = activity.Place.Id,
                      Latitude = activity.Place.Latitude,
                      Longitude = activity.Place.Longitude,
                      Name = activity.Place.Name
                  }
              })
              .ToList();

            return activities;
        }

        public List<ActivityViewModel> GetUserActivities(string username)
        {
            var activities = _context.Activities
              .Where(activity => activity.User.UserName == username)
              .Select(activity => new ActivityViewModel
              {
                  Username = activity.User.UserName,
                  Name = activity.Name,
                  Date = activity.Date,
                  Place = new PlaceViewModel
                  {
                      Id = activity.Place.Id,
                      Latitude = activity.Place.Latitude,
                      Longitude = activity.Place.Longitude,
                      Name = activity.Place.Name
                  }
              })
              .ToList();

            return activities;
        }

        public IEnumerable<PlaceModel> GetAllPlaces()
        {
            return _context.Places.ToList();
        }
        public IEnumerable<PlaceModel> GetPlacesByUserName(string username)
        {
            return _context.Activities
                .Where(ap => ap.User.UserName == username)
                .Select(p => p.Place)
                .ToList();
        }

        public PlaceModel GetPlaceByNamePlace(string name)
        {
            return _context.Places.FirstOrDefault(x => x.Name == name);
        }

    }
}
