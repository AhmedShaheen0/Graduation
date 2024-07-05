using System.Collections.Generic;
using Graduation.Models.Activity;
using Graduation.Models.ML;

namespace Graduation.Services.Activity
{
    public interface IFeedbackService
    {
        FeedbackModel CreateFeedback(FeedbackViewModel feedback);
        FeedbackModel GetFeedbackById(int id);
        List<FeedbackModel> GetAllFeedbacks();
    }
}