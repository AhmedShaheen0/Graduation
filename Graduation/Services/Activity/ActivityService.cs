
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Graduation.Models.Activity;
using Graduation.Services.Auth;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Graduation.Models.Auth;

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



        public ActivityModel CreateEvent(ActivityViewModel ev, ApplicationUser user)
        {
            var place = new PlaceModel
            {
                Latitude = ev.Place.Latitude,
                Longitude = ev.Place.Longitude,
                Name = ev.Place.placeName
                ,IsActive =true
            };

                    var model = new ActivityModel
            {

                username = user.UserName,
                Date = DateTime.UtcNow.Date,
                Time = TimeOnly.FromDateTime(DateTime.Now).ToString("HH:mm"),
                        Duration = ev.Duration,
                        Name = ev.Name,
                Place = place,
                User = user,
                IsActive = true
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
               .Where(x => x.IsActive == true && x.Place.IsActive == true)
             .FirstOrDefault(e => e.ID == id);

            if (activity is null)
            {
                return null;
            }


            // Create and return the ActivityViewModel instance
            var activityVM = new ActivityViewModel
            {
                ID = activity.ID,
                Username = activity.User.UserName,
                Name = activity.Name,
                Date = activity.Date,
                Time = activity.Time,
                Duration = activity.Duration,
                Place = new PlaceViewModel
                {
                    Id = activity.Place.Id,
                    Latitude = activity.Place.Latitude,
                    Longitude = activity.Place.Longitude,
                    placeName = activity.Place.Name
                }
            };
            return activityVM;
        }




        public List<ActivityViewModel> GetEvents()
        {
            var activities = _context.Activities
                .Where(activity => activity.IsActive && activity.Place.IsActive) // Filter on server side
                .Select(activity => new ActivityViewModel
                {
                    ID = activity.ID,
                    Username = activity.User.UserName,
                    Name = activity.Name,
                    Date = activity.Date,
                Time = activity.Time,
                    Duration = activity.Duration,
                    Place = new PlaceViewModel
                    {
                        Id = activity.Place.Id,
                        Latitude = activity.Place.Latitude,
                        Longitude = activity.Place.Longitude,
                        placeName = activity.Place.Name,
                        IsActive = activity.Place.IsActive // Include IsActive property
                    }
                })
                .ToList();

            return activities;
        }




        public List<ActivityViewModel> GetUserActivities(string username)
        {
            // Fetch the activities that match the filtering criteria
            var activities = _context.Activities
                .Include(x=>x.User)
                .Include(y=>y.Place)
                .Where(x => x.User.UserName == username && x.IsActive && x.Place.IsActive)
                .ToList();  // Load data into memory for further processing

            // Project the loaded data into ActivityViewModel
            var activityViewModels = activities.Select(activity => new ActivityViewModel
            {
                ID = activity.ID,
                Username = activity.User.UserName,
                Time = activity.Time,
                Duration = activity.Duration,
                Name = activity.Name,
                Date = activity.Date,
                Place = new PlaceViewModel
                {
                    Id = activity.Place.Id,
                    Latitude = activity.Place.Latitude,
                    Longitude = activity.Place.Longitude,
                    placeName = activity.Place.Name
                },
                IsActive = activity.IsActive
            })
           .AsEnumerable() // Switch to client-side evaluation

           .ToList();

            return activityViewModels;
        }

        public IEnumerable<PlaceModel> GetAllPlaces()
        {
            return _context.Places.Where(x => x.IsActive == true).ToList();
        }
        public IEnumerable<PlaceModel> GetPlacesByUserName(string username)
        {
            var places = from activity in _context.Activities
                         where activity.User.UserName == username && activity.IsActive
                         select activity.Place;

            return places.ToList();
        }

        public PlaceModel GetPlaceByNamePlace(string name)
        {
            return _context.Places.FirstOrDefault(x => x.Name == name && x.IsActive == true);
        }

        public string toggel_Activity(int activity_id)
        {
            var activity = _context.Activities.FirstOrDefault(x => x.ID == activity_id);
            if (activity == null) return "Activity is not found";
            activity.IsActive = false;
            _context.SaveChanges();
            return "The activity is deleted";
        }
        public string toggel_Place(int place_id)
        {
            var place = _context.Places.FirstOrDefault(x => x.Id == place_id);
            if (place == null) return "The Place is not found";
            place.IsActive = false;
            _context.SaveChanges();
            return "The Place is deleted";
        }
    }
}
