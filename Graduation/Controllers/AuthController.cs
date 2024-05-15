using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Graduation.Models.Auth;
using Graduation.Services.Auth;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("api/users")] // Define route for getting all users
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUser();
        if (users == null) return NotFound("Not Users Exist !");

            return Ok(users); 
        }

        [HttpGet]
        [Route("api/users/{username}")] 
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _authService.GetUserByUsername(username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user); 
        }
    
    [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
    }
}