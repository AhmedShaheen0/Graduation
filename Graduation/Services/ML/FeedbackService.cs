using System;
using System.Collections.Generic;
using System.Linq;
using Graduation.Models.Activity;
using Graduation.Models.ML;
using Graduation.Services.Auth;

namespace Graduation.Services.Activity
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public FeedbackModel CreateFeedback(FeedbackViewModel feedback)
        {
            // Check if the ActivityID exists in the Activities table
            var activityExists = _context.Activities.Any(a => a.ID == feedback.ActivityID);
            if (!activityExists)
            {
                throw new Exception($"Activity with ID {feedback.ActivityID} does not exist.");
            }

            var model = new FeedbackModel
            {
                State = feedback.State,
                Feedback = feedback.Feedback,
                ActivityID = feedback.ActivityID,
                Reward = feedback.Reward,
                Observation = feedback.Observation
            };
            _context.Feedbacks.Add(model);
            _context.SaveChanges();
            return model;
        }

        public FeedbackModel GetFeedbackById(int id)
        {
            return _context.Feedbacks.FirstOrDefault(f => f.ID == id);
        }

        public List<FeedbackModel> GetAllFeedbacks()
        {
            return _context.Feedbacks.ToList();
        }
    }
}