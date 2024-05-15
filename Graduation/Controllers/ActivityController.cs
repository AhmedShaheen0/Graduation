using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Graduation.Const;
using Graduation.Models.Activity;
using Graduation.Models.Auth;
using Graduation.Services.Activity;

namespace Graduation.Controllers
{
    [Authorize(Roles = AppRoles.Admin + ", " + AppRoles.User)]
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {

        private readonly ILogger<ActivityController> _logger;
        private readonly IActivityService _actionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivityController(IActivityService actionService, UserManager<ApplicationUser> userManager, ILogger<ActivityController> logger)
        {
            _actionService = actionService;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpGet("Event")]
        public async Task<IActionResult> GetEvents()
        {
            var actions = _actionService.GetEvents();
            return Ok(actions);
        }
        [HttpGet("Event/{Id}")]
        public async Task<IActionResult> GetEventAsync(int id)
        {

            var action = _actionService.GetEventById(id);
            if (action is null) return NotFound();

            return Ok(action);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> PostEvent(ActivityViewModel activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(activity.Username);
            var place = new PlaceModel
            {
                Id = activity.Place.Id,
                Latitude = activity.Place.Latitude,
                Longitude = activity.Place.Longitude,
                Name = activity.Place.Name
            };

            // Ensure user is found
            if (user == null)
            {
                return NotFound("User not found");
            }
            var activityModel = new ActivityModel
            {
                Name = activity.Name,
                Date = DateTime.UtcNow,
                Userid = user.Id,
                User = user,
                Place = place
            };

            _actionService.CreateEvent(activityModel, user.UserName);

            return Ok();
        }
        [HttpGet("Event/Username")]

        public IActionResult GetUserActivities(string username)
        {

            var userActivities = _actionService.GetUserActivities(username);

            if (userActivities == null || !userActivities.Any())
            {
                return NotFound("No activities found for this user.");
            }

            return Ok(userActivities);
        }

        [HttpGet("Places_By_Username")]
        public async Task<IActionResult> GetPlacesByUserName(string username)
        {
            var places = _actionService.GetPlacesByUserName(username);
            return Ok(places);
        }

        [HttpGet("Places")]
        public async Task<IActionResult> GetAllPlaces()
        {
            var places = _actionService.GetAllPlaces();
            return Ok(places);
        }
    }
}

