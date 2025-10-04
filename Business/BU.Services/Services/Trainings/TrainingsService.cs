using AutoMapper;
using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.ResponseDTO.Testimonials;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using BU.Services.IServices.Trainings;
using Common;
using Common.Helper;
using DA.DAO.DAO.Trainings;
using DA.Entities.Trainings;
using DAO;
using IN.Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BU.Services.Services.Trainings
{
    public class TrainingsService : ITrainingsService
    {
        private readonly IRepository<Fe_trainings> _TrainingsRepository;
        private readonly IRepository<Fe_feedback_reply> _TrainingsFeedBackRepository;

        private readonly IRepository<Fe_training_media> _TrainingMediaRepository;
        private readonly IRepository<Fe_training_feedback> _FeedBackRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public TrainingsService(IRepository<Fe_trainings> TrainingsRepository, IRepository<Fe_training_media> TrainingMediaRepository, IRepository<Fe_training_feedback> TrainingFeedbackRepository, IMapper mapper, IConfiguration configuration, IRepository<Fe_feedback_reply> TrainingsFeedbackRepository)
        {
            _TrainingsRepository = TrainingsRepository;
            _TrainingMediaRepository = TrainingMediaRepository;
            _FeedBackRepository = TrainingFeedbackRepository;
            _configuration = configuration;
            _mapper = mapper;
            _TrainingsFeedBackRepository = TrainingsFeedbackRepository;
        }

        public TrainingsDTO Add(TrainingsDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }

            long nextIdentity = _TrainingsRepository.GetNextIdentityId("fe_trainings");
            if (!string.IsNullOrEmpty(obj.Base64Image))
            {
                string rootPath = _configuration["Web:DocumentPath"];
                string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(encodeImage);
                string folderPath = $"\\Documents\\Trainings\\{nextIdentity}";
                string fullPath = $"{rootPath}\\Documents\\Trainings\\{nextIdentity}";
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
                string fileName = Guid.NewGuid().ToString() + ".jpg";

                File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                obj.PhotoPath = Path.Combine(folderPath, fileName);
            }

            Fe_trainings training = _mapper.Map<TrainingsDTO, Fe_trainings>(obj);
            training.Training_status = TrainingStatus.UnPublished.ToString();
            obj.TrainingId = _TrainingsRepository.Insert(training);
            return obj;
        }

        public List<TrainingsDTO> GetList()
        {
            var trainings = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetAllTrainingsQuery).ToList();
            return _mapper.Map<List<Fe_trainings>, List<TrainingsDTO>>(trainings);
        }

        public bool Delete(TrainingsDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            return _TrainingsRepository.Archive(new
            {
                @UpdatedBy = obj.UpdatedBy,
                @TrainingId = obj.TrainingId
            });

        }

        public bool Update(TrainingsDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrainingsDTO> loadGrid(string[] parameters)
        {
            var trainings = _TrainingsRepository.GetSearchData(Database.MAIN, parameters);
            return _mapper.Map<IEnumerable<Fe_trainings>, IEnumerable<TrainingsDTO>>(trainings);
        }

        public TrainingsDTO Get(long id)
        {
            var training = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingByIdQuery, new { TrainingId = id }).FirstOrDefault();
            return _mapper.Map<Fe_trainings, TrainingsDTO>(training);
        }

        public bool Upload(UploadTrainingFileDTO obj)
        {
            long nextIdentity = _TrainingMediaRepository.GetNextIdentityId("fe_training_media");
            string rootPath = _configuration["Web:DocumentPath"];
            string fullPath = "";
            string completeFilePath = "";
            List<Fe_training_media> mediaFilesList = new List<Fe_training_media>();
            if(obj.MediaFiles != null)
            {
                foreach (var MediaFile in obj.MediaFiles)
                {
                    if (MediaFile != null)
                    {
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // Adding timestamp
                        string file_path = $"\\Documents\\TrainingsMedia\\{obj.TrainingId}\\{timestamp}_{MediaFile.FileName}";
                        fullPath = $"{rootPath}\\Documents\\TrainingsMedia\\{obj.TrainingId}";
                        completeFilePath = rootPath + file_path;
                        uploadFile(file_path, fullPath, MediaFile, rootPath);
                        Fe_training_media media = new Fe_training_media()
                        {
                            Training_id = obj.TrainingId,
                            Media_name = MediaFile.FileName,
                            Media_path = file_path,
                            Media_type = MediaFile.ContentType,
                            Category = "Media"
                        };
                        mediaFilesList.Add(media);

                    }
                }

            }

            if (obj.BannerFiles != null )
            {
                foreach (var MediaFile in obj.BannerFiles)
                {
                    if (MediaFile != null)
                    {
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // Adding timestamp
                        string file_path = $"\\Documents\\TrainingsBanners\\{obj.TrainingId}\\{timestamp}_{MediaFile.FileName}";
                        fullPath = $"{rootPath}\\Documents\\TrainingsBanners\\{obj.TrainingId}";
                        completeFilePath = rootPath + file_path;
                        uploadFile(file_path, fullPath, MediaFile, rootPath);
                        Fe_training_media media = new Fe_training_media()
                        {
                            Training_id = obj.TrainingId,
                            Media_name = MediaFile.FileName,
                            Media_path = file_path,
                            Media_type = MediaFile.ContentType,
                            Category = "Banner"
                        };
                        mediaFilesList.Add(media);

                    }
                }
            }
            
            
            if (mediaFilesList.Count > 0)
            {
                _TrainingMediaRepository.BulkInsert(mediaFilesList);
            }

            return true;
        }

        private void uploadFile(string filePath, string fullPath, IFormFile formFile, string rootPath)
        {
            //string rootPath = _configuration["Web:DocumentPath"];
            if (!Directory.Exists(fullPath))
                System.IO.Directory.CreateDirectory(fullPath);

            fullPath = $"{rootPath}{filePath}";
            using (Stream stream = new FileStream(fullPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

        }

        public TrainingDetailResponseDTO GetTrainingDetails(TrainingRequestDTO obj)
        {
            var training = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingByIdQuery, new { TrainingId = obj.TrainingId }).FirstOrDefault();
            TrainingDetailResponseDTO response = _mapper.Map<Fe_trainings, TrainingDetailResponseDTO>(training);

            var trainingMedia = _TrainingMediaRepository.GetList(Database.MAIN, TrainingMediaDAO.GetTrainingMedia, new { TrainingId = obj.TrainingId }).ToList();
            response.TrainingMedia = _mapper.Map<List<Fe_training_media>, List<TrainingMediaDTO>>(trainingMedia);

            var trainingBanner = _TrainingMediaRepository.GetList(Database.MAIN, TrainingMediaDAO.GetTrainingBanner, new { TrainingId = obj.TrainingId }).ToList();
            response.TrainingBanner = _mapper.Map<List<Fe_training_media>, List<TrainingMediaDTO>>(trainingBanner);

            return response;
        }

        public bool PublishTraining(TrainingRequestDTO obj)
        {

            var training = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingByIdQuery, new { TrainingId = obj.TrainingId }).FirstOrDefault();

            training.Training_status = TrainingStatus.Active.ToString();
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                training.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            _TrainingsRepository.Update(training);
            return true;
        }

        public bool UnPublishTraining(TrainingRequestDTO obj)
        {

            var training = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingByIdQuery, new { TrainingId = obj.TrainingId }).FirstOrDefault();

            training.Training_status = TrainingStatus.UnPublished.ToString();
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                training.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            _TrainingsRepository.Update(training);
            return true;
        }

        public bool CompleteTraining(TrainingRequestDTO obj)
        {

            var training = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingByIdQuery, new { TrainingId = obj.TrainingId }).FirstOrDefault();
            training.Training_status = TrainingStatus.Completed.ToString();
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                training.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            _TrainingsRepository.Update(training);
            return true;
        }

        public IEnumerable<TrainingDetailResponseDTO> GetTrainingsByStatus(TrainingRequestDTO obj)
        {
            var trainings = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingByStatusQuery, new { TrainingStatus = obj.TrainingStatus });
            return _mapper.Map<IEnumerable<Fe_trainings>, IEnumerable<TrainingDetailResponseDTO>>(trainings);
        }

        public IEnumerable<TrainingDetailResponseDTO> GetTrainingsByAgencyId(TrainingRequestDTO obj)
        {
            var trainings = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingByAgencyQuery,
                new
                {
                    TrainingStatus = obj.TrainingStatus,
                    AgencyId = obj.AgencyId
                }
                );
            return _mapper.Map<IEnumerable<Fe_trainings>, IEnumerable<TrainingDetailResponseDTO>>(trainings);
        }

        public bool UpdateTrainingProgress(TrainingRequestDTO obj)
        {
            var training = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetOnlyTrainingByIdQuery,
                new
                {
                    TrainingId = obj.TrainingId
                }
                ).FirstOrDefault();
            if (training != null)
            {
                training.Training_progress = obj.TrainingProgress;
                if (!string.IsNullOrWhiteSpace(obj.EncUserID))
                {
                    training.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
                }
                _TrainingsRepository.Update(training);
            }

            return true;
        }

        public IEnumerable<TrainingDetailResponseDTO> GetCustomerEnrolledTrainings(TrainingRequestDTO obj)
        {
            var customerEnrTrainings = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetCustomerEnrolledTraingingsQuery, new { CustomerId = obj.CustomerId });
            return _mapper.Map<IEnumerable<Fe_trainings>, IEnumerable<TrainingDetailResponseDTO>>(customerEnrTrainings);
        }


        public IEnumerable<TrainingDetailResponseDTO> GetTrainingsCustomerNotEnrolled(TrainingRequestDTO obj)
        {
            var customerEnrTrainings = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingsCustomerNotEnrolledQuery, new { CustomerId = obj.CustomerId });
            return _mapper.Map<IEnumerable<Fe_trainings>, IEnumerable<TrainingDetailResponseDTO>>(customerEnrTrainings);
        }
        public bool UpdateTraining(TrainingUpdateRequestDTO obj)
        {
            var training = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetOnlyTrainingByIdQuery,
                new
                {
                    TrainingId = obj.TrainingID
                }
            ).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                training.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            training.Duration = obj.Duration;
            training.Location_lat = obj.Lat;
            training.Location_lng = obj.Lng;
            training.To_date = obj.StartDate;
            training.From_date = obj.EndDate;
            training.Trainer_name = obj.TrainerName;
            training.Training_title = obj.TrainingTitle;
            training.Details = obj.TrainingDescription;
            training.Fee = obj.TrainingPrice;
            _TrainingsRepository.Update(training);
            return true;
        }

        public TrainingDetailResponseDTO GetCustomerCompletedTrainingDetail(TrainingRequestDTO obj)
        {
            var training = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingByIdQuery, new { obj.TrainingId }).FirstOrDefault();
            TrainingDetailResponseDTO response = _mapper.Map<Fe_trainings, TrainingDetailResponseDTO>(training);

            var trainingBanner = _TrainingMediaRepository.GetList(Database.MAIN, TrainingMediaDAO.GetTrainingBanner, new { obj.TrainingId }).ToList();
            response.TrainingBanner = _mapper.Map<List<Fe_training_media>, List<TrainingMediaDTO>>(trainingBanner);

            var feedBack = _FeedBackRepository.GetList(Database.MAIN, TrainingsDAO.GetTrainingFeedBack, new { obj.TrainingId }).ToList();
            response.TrainingFeedBacks = _mapper.Map<List<Fe_training_feedback>, List<TrainingFeedBackDTO>>(feedBack);

            return response;
        }

        public IEnumerable<TestimonialsDTO> GetTestimonials(AgencyDTO agencyId)
        {
            var testimonials = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTestimonials, new { agencyId.AgencyId });
            return _mapper.Map<IEnumerable<Fe_trainings>, IEnumerable<TestimonialsDTO>>(testimonials);

        }
        public TestimonialsDTO GetTestimonialsDetail(TrainingRequestDTO obj)
        {
            // Fetch the main testimonial for the training
            var testimonial = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetTestimonialsById, new { obj.TrainingId }).FirstOrDefault();
            if (testimonial == null)
            {
                throw new InvalidOperationException("Testimonial not found.");
            }

            TestimonialsDTO response = _mapper.Map<Fe_trainings, TestimonialsDTO>(testimonial);

            var feedBackList = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetFeedbackListInTestimonial, new { obj.TrainingId }).ToList();

            response.feedBackList = feedBackList.Select(feedback =>
            {
                var feedbackDto = _mapper.Map<Fe_trainings, TestimonialsDetails>(feedback);
                
                var replyList = _TrainingsFeedBackRepository.GetList(Database.MAIN, TrainingsDAO.GetFeedbackRepliesByFeedbackId, new { feedback.Training_feedback_id }).FirstOrDefault();
                feedbackDto.Replies = _mapper.Map<Fe_feedback_reply, FeedbackReplyDTO>(replyList);

                return feedbackDto;
            }).ToList();

            return response;
        }


        public FeedbackReplyDTO AddReply(FeedbackReplyDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }

            Fe_feedback_reply feedback = _mapper.Map<FeedbackReplyDTO, Fe_feedback_reply>(obj);

          

            _TrainingsFeedBackRepository.Insert(feedback);


            return obj;
        }

        public IEnumerable<TrainingDetailResponseDTO> GetFeaturedTrainings(TrainingRequestDTO obj)
        {
            var customerEnrTrainings = _TrainingsRepository.GetList(Database.MAIN, TrainingsDAO.GetFeaturedTraining, new {CustomerId = obj.CustomerId});
            return _mapper.Map<IEnumerable<Fe_trainings>, IEnumerable<TrainingDetailResponseDTO>>(customerEnrTrainings);
        }
    }
}
