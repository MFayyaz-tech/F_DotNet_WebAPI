using AutoMapper;
using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using BU.Services.IServices.Trainings;
using Common;
using Common.Helper;
using System.Linq;

using DA.DAO.DAO.Trainings;
using DA.Entities.Trainings;
using DAO;
using Entities.Users;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BU.Services.Services.Trainings
{
    public class TrainingFeedBackService : ITrainingFeedBackService
    {
        private readonly IRepository<Fe_training_feedback> _TrainingfeedbackRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public TrainingFeedBackService(IRepository<Fe_training_feedback> TrainingfeedbackRepository, IMapper mapper, IConfiguration configuration)
        {
            _TrainingfeedbackRepository = TrainingfeedbackRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public TrainingFeedBackDTO Add(TrainingFeedBackDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            long nextIdentity = _TrainingfeedbackRepository.GetNextIdentityId("fe_training_feedback");
            if (!string.IsNullOrEmpty(obj.Base64Image))
            {
                string rootPath = _configuration["Web:DocumentPath"];
                string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(encodeImage);
                string folderPath = $"\\Documents\\TrainingFeedback\\{nextIdentity}";
                string fullPath = $"{rootPath}\\Documents\\TrainingFeedback\\{nextIdentity}";
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
                string fileName = Guid.NewGuid().ToString() + ".jpg";

                File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                obj.AttachmentMedia = Path.Combine(folderPath, fileName);
            }
            Fe_training_feedback feeback = _mapper.Map<TrainingFeedBackDTO, Fe_training_feedback>(obj);
            obj.TrainingFeedBackId = _TrainingfeedbackRepository.Insert(feeback);
            return obj;
        }
        public List<TrainingFeedBackDTO> GetList()
        {
            throw new NotImplementedException();
        }
        public bool Delete(TrainingFeedBackDTO obj)
        {
            throw new NotImplementedException();
        }
        public bool Update(TrainingFeedBackDTO obj)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TrainingFeedBackDTO> loadGrid(string[] parameters)
        {
            throw new NotImplementedException();
        }
        public TrainingFeedBackDTO Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrainingFeedBackDTO> GetTrainingFeedBacks(TrainingFeedBackRequestDTO obj)
        {
            var feebacks = _TrainingfeedbackRepository.GetList(Database.MAIN, TrainingFeedBackDAO.GetTrainingFeedbacksByTrainingId, new { TrainingId = obj.TrainingId });
            return _mapper.Map<IEnumerable<Fe_training_feedback>, IEnumerable<TrainingFeedBackDTO>>(feebacks);
        }

        public IEnumerable<TrainingFeedBackDTO> GetCustomerFeedBack(TrainingFeedBackRequestDTO obj)
        {
            var feebacks = _TrainingfeedbackRepository.GetList(Database.MAIN, TrainingFeedBackDAO.GetCustomerFeedBacks, new { CustomerId = obj.CustomerId });
            return _mapper.Map<IEnumerable<Fe_training_feedback>, IEnumerable<TrainingFeedBackDTO>>(feebacks);

        }

        public bool DeleteFeedback(FeedbackReplyDTO obj)
        {
            try
            {


                var training = _TrainingfeedbackRepository.GetList(Database.MAIN, TrainingsDAO.GetFeedbackById, new { feedbackId = obj.TrainingFeedbackId }).FirstOrDefault();



                if (training == null)
                {
                    Console.WriteLine("Error: Feedback not found with ID: " + obj.TrainingFeedbackId);
                    return false;
                }

                // Update the fields
                training.Updated_by = 1;
                training.Is_deleted = true;


                // Update the feedback in the database
                bool updateSuccess = _TrainingfeedbackRepository.Update(training);

                // Return the result of the update operation
                return updateSuccess;
            }
            catch (Exception ex)
            {
                // Log any other errors that may occur during the process
                Console.WriteLine("Error while deleting feedback: " + ex.Message);
                return false;
            }
        }

    }
}
