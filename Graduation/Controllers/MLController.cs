using Microsoft.AspNetCore.Mvc;
using Graduation.Models.Activity;
using Graduation.Services.Activity;
using System.Collections.Generic;
using Graduation.Models.ML;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Graduation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MLController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly ApplicationDbContext _context;
        public MLController(IFeedbackService feedbackService, ApplicationDbContext context)
        {
            _feedbackService = feedbackService;
            _context = context;
        }

        [HttpPost("Create")]
        public IActionResult CreateFeedback([FromBody] FeedbackViewModel feedback)
        {
            var createdFeedback = _feedbackService.CreateFeedback(feedback);
            return Ok(createdFeedback);
        }

        [HttpGet("{id}")]
        public IActionResult GetFeedbackById(int id)
        {
            var feedback = _feedbackService.GetFeedbackById(id);
            if (feedback == null)
                return NotFound("Feedback not found.");

            return Ok(feedback);
        }

        [HttpGet]
        public IActionResult GetAllFeedbacks()
        {
            var feedbacks = _feedbackService.GetAllFeedbacks();
            return Ok(feedbacks);
        }
        [HttpGet("actions")]
        public  ActionResult<IEnumerable<ActionModel>> GetActions()
        {
            return  _context.Actions.ToList();
        }

        // POST: api/Actions
        [HttpPost("actions")]
        public async Task<ActionResult<ActionModel>> PostAction(ActionModel actionModel)
        {
            _context.Actions.Add(actionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActions), new { id = actionModel.Id }, actionModel);
        }
    }
}