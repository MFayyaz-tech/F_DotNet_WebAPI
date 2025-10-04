using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.ResponseDTO.Testimonials;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Trainings
{
    public interface ITrainingsService
    {
        TrainingsDTO Add(TrainingsDTO obj);
        FeedbackReplyDTO AddReply(FeedbackReplyDTO obj);
        List<TrainingsDTO> GetList();
        IEnumerable<TestimonialsDTO> GetTestimonials(AgencyDTO agencyId);
        TestimonialsDTO GetTestimonialsDetail(TrainingRequestDTO obj);
        //Test
        bool Delete(TrainingsDTO obj);
        bool Update(TrainingsDTO obj);
        bool UpdateTraining(TrainingUpdateRequestDTO obj);
        IEnumerable<TrainingsDTO> loadGrid(string[] parameters);
        TrainingsDTO Get(long id);
        bool Upload(UploadTrainingFileDTO obj);
        TrainingDetailResponseDTO GetTrainingDetails(TrainingRequestDTO obj);
        TrainingDetailResponseDTO GetCustomerCompletedTrainingDetail(TrainingRequestDTO obj);

        bool PublishTraining(TrainingRequestDTO obj);
        bool UnPublishTraining(TrainingRequestDTO obj);
        bool CompleteTraining(TrainingRequestDTO obj);
        IEnumerable<TrainingDetailResponseDTO> GetTrainingsByStatus(TrainingRequestDTO obj);
        IEnumerable<TrainingDetailResponseDTO> GetTrainingsByAgencyId(TrainingRequestDTO obj);
        bool UpdateTrainingProgress(TrainingRequestDTO obj);
        IEnumerable<TrainingDetailResponseDTO> GetTrainingsCustomerNotEnrolled(TrainingRequestDTO obj);
        IEnumerable<TrainingDetailResponseDTO> GetFeaturedTrainings(TrainingRequestDTO obj);
        IEnumerable<TrainingDetailResponseDTO> GetCustomerEnrolledTrainings(TrainingRequestDTO obj);
    }
}
