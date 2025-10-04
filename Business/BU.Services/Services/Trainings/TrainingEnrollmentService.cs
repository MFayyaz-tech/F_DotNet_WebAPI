using AutoMapper;
using BU.DTO.DTOs.RequestDTO.Trainings;
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
    public class TrainingEnrollmentService : ITrainingEnrollmentService
    {
        private readonly IRepository<Fe_trainings> _FeTrainingRepository;
        private readonly IRepository<Fe_training_enrollment> _FeTrainingEnrollmentRepository;
        private readonly IRepository<Fe_training_enrollment_media> _FeTrainingEnrollmentMediaRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public TrainingEnrollmentService(
            IRepository<Fe_trainings> FeTrainingRepository,
            IRepository<Fe_training_enrollment> FeTrainingEnrollmentRepository,
            IRepository<Fe_training_enrollment_media> FeTrainingEnrollmentMediaRepository,
            IMapper mapper, IConfiguration configuration)
        {
            _FeTrainingRepository = FeTrainingRepository;
            _FeTrainingEnrollmentRepository = FeTrainingEnrollmentRepository;
            _FeTrainingEnrollmentMediaRepository = FeTrainingEnrollmentMediaRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public TrainingEnrollmentDTO Add(TrainingEnrollmentDTO obj)
        {
            throw new NotImplementedException();
        }
        public List<TrainingEnrollmentDTO> GetList()
        {
            throw new NotImplementedException();
        }
        public bool Delete(TrainingEnrollmentDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            bool response = _FeTrainingEnrollmentRepository.Archive(new
            {
                EnrollmentId = obj.EnrollmentId,
                UpdatedBy = obj.UpdatedBy
            });
            return response;
        }
        public bool Update(TrainingEnrollmentDTO obj)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TrainingEnrollmentDTO> loadGrid(string[] parameters)
        {
            throw new NotImplementedException();
        }
        public TrainingEnrollmentDTO Get(long id)
        {
            throw new NotImplementedException();
        }

        public TrainingEnrollmentDTO EnrolCustomer(TrainingEnrolRequestDTO obj)
        {
            var training = _FeTrainingRepository.GetList(Database.MAIN, TrainingsDAO.GetOnlyTrainingByIdQuery, new { TrainingId = obj.TrainingId }).FirstOrDefault();
            Fe_training_enrollment enrol = new Fe_training_enrollment();
            enrol.Training_id = obj.TrainingId;
            enrol.Customer_id = obj.CustomerId;
            enrol.Enrollment_status = training.Is_approval_required == true ? EnrollmentStatus.Pending.ToString(): EnrollmentStatus.Enrolled.ToString();
            enrol.Enrollment_date = DateTime.Now;
            enrol.Is_active = true;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                enrol.Created_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            enrol.Enrollment_id = _FeTrainingEnrollmentRepository.Insert(enrol);
            if(obj.MediaFiles != null && obj.MediaFiles.Count > 0)
            {
                string rootPath = _configuration["Web:DocumentPath"];
                string fullPath = "";
                string completeFilePath = "";
                List<Fe_training_enrollment_media> mediaFilesList = new List<Fe_training_enrollment_media>();
                foreach (var _mediaObj in obj.MediaFiles)
                {
                    if (_mediaObj != null && _mediaObj.MediaFile != null)
                    {
                        var mediaFile = _mediaObj.MediaFile;
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // Adding timestamp
                        string file_path = $"\\Documents\\TrainingsEnrollmentMedia\\{enrol.Enrollment_id}\\{timestamp}_{mediaFile.FileName}";
                        fullPath = $"{rootPath}\\Documents\\TrainingsEnrollmentMedia\\{enrol.Enrollment_id}";
                        completeFilePath = rootPath + file_path;
                        uploadFile(file_path, fullPath, mediaFile, rootPath);
                        Fe_training_enrollment_media media = new Fe_training_enrollment_media()
                        {
                            Enrollment_id = enrol.Enrollment_id,
                            Media_name = mediaFile.FileName,
                            Media_path = file_path,
                            Media_type = mediaFile.ContentType,
                            Media_category = _mediaObj.MediaCategory
                        };
                        mediaFilesList.Add(media);

                    }
                }
                if(mediaFilesList.Count > 0 )
                {
                    _FeTrainingEnrollmentMediaRepository.BulkInsert(mediaFilesList);
                }
            }

            return _mapper.Map<Fe_training_enrollment, TrainingEnrollmentDTO>(enrol);
        }

        public bool CheckAlreadyEnrolled(TrainingEnrolRequestDTO obj)
        {
            bool recordExists = false;
            Fe_training_enrollment record =  _FeTrainingEnrollmentRepository.GetList(Database.MAIN, TrainingEnrollmentDAO.AlreadyEnrolledQuery, new
            {
                TrainingId = obj.TrainingId,
                CustomerId = obj.CustomerId
            }).FirstOrDefault();
            if(record != null)
            {
                recordExists = true;
            }
            return recordExists;
        }

        public IEnumerable<TrainingEnrollmentsResponseDTO> AgencyTrainingEnrollmentRequests(TrainingEnrolRequestDTO obj)
        {
            var record = _FeTrainingEnrollmentRepository.GetList(Database.MAIN, TrainingEnrollmentDAO.AgencyTrainingEnrollmentRequestsQuery, new
            {
                obj.AgencyId,
                obj.TrainingId
            });
            return _mapper.Map<IEnumerable<Fe_training_enrollment>, IEnumerable<TrainingEnrollmentsResponseDTO>>(record);
        }

        public IEnumerable<TrainingEnrollmentsResponseDTO> CustomerEnrollmentRequests(TrainingEnrolRequestDTO obj)
        {
            var record = _FeTrainingEnrollmentRepository.GetList(Database.MAIN, TrainingEnrollmentDAO.CustomerTrainingEnrollmentRequestsQuery, new
            {
                CustomerId = obj.CustomerId
            });
            return _mapper.Map<IEnumerable<Fe_training_enrollment>, IEnumerable<TrainingEnrollmentsResponseDTO>>(record);
        }

        public bool AgencyApproveRejectEnrollmentRequest(TrainingEnrolRequestDTO obj)
        {
            Fe_training_enrollment record = _FeTrainingEnrollmentRepository.GetList(Database.MAIN, TrainingEnrollmentDAO.GetEnrollmentByIdQuery, new
            {
                EnrollmentId = obj.EnrollmentId
            }).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                record.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            if(obj.EnrollmentStatus == "Approved")
            {
                record.Enrollment_status = EnrollmentStatus.Enrolled.ToString();
            }
            else
            {
                record.Enrollment_status = EnrollmentStatus.Rejected.ToString();
                record.Rejection_reason = obj.RejectionReason;
            }
            _FeTrainingEnrollmentRepository.Update(record);
            return true;
        }


        public bool CompleteTrainingEnrollment(TrainingEnrolRequestDTO obj)
        {
            Fe_training_enrollment record = _FeTrainingEnrollmentRepository.GetList(Database.MAIN, TrainingEnrollmentDAO.GetEnrollmentByIdQuery, new
            {
                EnrollmentId = obj.EnrollmentId
            }).FirstOrDefault();


            if (record == null)
            {
                throw new Exception("Enrollment record not found.");
            }

            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                record.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            record.Enrollment_status = EnrollmentStatus.Completed.ToString();
 
            _FeTrainingEnrollmentRepository.Update(record);
            return true;
        }




        public IEnumerable<TrainingEnrollmentsResponseDTO> GetTrainingsCustomerNotEnrolled(TrainingEnrolRequestDTO obj)
        {
            throw new NotImplementedException();
        }

        #region
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

        public IEnumerable<CustomerCompletedTrainingsResponseDTO> GetCustomerCompletedTrainings(TrainingEnrolRequestDTO obj)
        {
            var records = _FeTrainingEnrollmentRepository.GetList(Database.MAIN, TrainingEnrollmentDAO.CustomerCompletedTrainingQuery, new
            {
                CustomerId = obj.CustomerId
            });

            return _mapper.Map<IEnumerable<Fe_training_enrollment>, IEnumerable<CustomerCompletedTrainingsResponseDTO>>(records);
        }


        public IEnumerable<TrainingEnrollmentsResponseDTO> GetCustomerEnrolledTrainings(TrainingEnrolRequestDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrainingEnrollmentMediaDTO> GetEnrollmentMedia(MediaRequestDTO obj)
        {
            // Retrieve the media records associated with the given EnrollmentId
            var records = _FeTrainingEnrollmentMediaRepository.GetList(Database.MAIN, TrainingEnrollmentDAO.GetTrainingEnrollmentMedia, new
            {
                EnrollmentId = obj.EnrollmentId
            }).ToList();

            // Map the list of media records to the list of DTOs
            return _mapper.Map<IEnumerable<Fe_training_enrollment_media>, IEnumerable<TrainingEnrollmentMediaDTO>>(records);
        }

        #endregion
    }
}
