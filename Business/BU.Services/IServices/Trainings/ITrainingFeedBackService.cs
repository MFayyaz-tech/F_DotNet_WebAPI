using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Trainings
{
    public interface
        ITrainingFeedBackService
    {
        TrainingFeedBackDTO Add(TrainingFeedBackDTO obj);
        List<TrainingFeedBackDTO> GetList();
            bool DeleteFeedback(FeedbackReplyDTO obj);

        bool Delete(TrainingFeedBackDTO obj);
        bool Update(TrainingFeedBackDTO obj);
        IEnumerable<TrainingFeedBackDTO> GetCustomerFeedBack(TrainingFeedBackRequestDTO obj);

        IEnumerable<TrainingFeedBackDTO> loadGrid(string[] parameters);
        TrainingFeedBackDTO Get(long id);
        IEnumerable<TrainingFeedBackDTO> GetTrainingFeedBacks(TrainingFeedBackRequestDTO obj);
    }
}
