using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Trainings
{
    public interface ITrainingEnrollmentService
    {
        TrainingEnrollmentDTO Add(TrainingEnrollmentDTO obj);
        TrainingEnrollmentDTO EnrolCustomer(TrainingEnrolRequestDTO obj);
        IEnumerable<TrainingEnrollmentMediaDTO> GetEnrollmentMedia(MediaRequestDTO obj);
        
        List<TrainingEnrollmentDTO> GetList();
        bool Delete(TrainingEnrollmentDTO obj);
        bool Update(TrainingEnrollmentDTO obj);
        IEnumerable<TrainingEnrollmentDTO> loadGrid(string[] parameters);
        TrainingEnrollmentDTO Get(long id);
        bool CheckAlreadyEnrolled(TrainingEnrolRequestDTO obj);
        IEnumerable<TrainingEnrollmentsResponseDTO> AgencyTrainingEnrollmentRequests(TrainingEnrolRequestDTO obj);
        IEnumerable<TrainingEnrollmentsResponseDTO> CustomerEnrollmentRequests(TrainingEnrolRequestDTO obj);
        IEnumerable<CustomerCompletedTrainingsResponseDTO> GetCustomerCompletedTrainings(TrainingEnrolRequestDTO obj);
        bool AgencyApproveRejectEnrollmentRequest(TrainingEnrolRequestDTO obj);
        IEnumerable<TrainingEnrollmentsResponseDTO> GetCustomerEnrolledTrainings(TrainingEnrolRequestDTO obj);
        IEnumerable<TrainingEnrollmentsResponseDTO> GetTrainingsCustomerNotEnrolled(TrainingEnrolRequestDTO obj);
        bool CompleteTrainingEnrollment(TrainingEnrolRequestDTO obj);
    }
}
