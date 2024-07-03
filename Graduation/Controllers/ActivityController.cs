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
using Microsoft.Extensions.Options;

namespace Graduation.Controllers
{
    //[Authorize(Roles = AppRoles.Admin  + AppRoles.User)]
    //[Authorize(AuthenticationSchemes = "CustomHeader")]
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
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var actions = _actionService.GetEvents();

            if (actions == null || !actions.Any())
                return Ok("No activities found.");

            return Ok(actions);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEventAsync(int id)
        {

            var action = _actionService.GetEventById(id);
            if (action is null) return Ok("No activity found.");

            return Ok(action);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostEvent([FromForm] ActivityViewModel activity)
        {
            var user = await _userManager.FindByNameAsync(activity.Username);
            if (user == null)
            {
                return BadRequest(new { Message = "User not found" });
            }
            _actionService.CreateEvent(activity, user);

            return Ok();
        }

        [HttpGet("Username/{username}")]

        public IActionResult GetUserActivities(string username)
        {

            var userActivities = _actionService.GetUserActivities(username);

            if (userActivities == null || !userActivities.Any())
            {
                return Ok("No activities found for this user.");
            }

            return Ok(userActivities);
        }

        [HttpGet("Places/Username/{username}")]
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
        [HttpDelete("Place")]
        public ActionResult toggel_Place(int place_id)
        {
            var place = _actionService.toggel_Place(place_id);
            return Ok(place);
        }
        [HttpDelete("Activity")]
        public ActionResult toggel_activityy(int activity_id)
        {
            var activity = _actionService.toggel_Activity(activity_id);
            return Ok(activity);
        }
      
    }
}

