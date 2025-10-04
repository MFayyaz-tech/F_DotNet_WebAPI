using AutoMapper;
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using BU.Services.IServices.Trainings;
using Common.Helper;
using DA.DAO.DAO.Trainings;
using DA.Entities.Customer;
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
    public class TrainersService : ITrainersService
    {
        private readonly IRepository<Fe_trainers> _FeTrainersRepository;
        private readonly IRepository<Fe_users> _UserRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public TrainersService(IRepository<Fe_trainers> FeTrainersRepository, IRepository<Fe_users> UserRepository, IMapper mapper, IConfiguration configuration)
        {
            _FeTrainersRepository = FeTrainersRepository;
            _UserRepository = UserRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public TrainersDTO Add(TrainersDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            long nextIdentity = _FeTrainersRepository.GetNextIdentityId("fe_trainers");
            string randomPassword = "welcome";
            if (!string.IsNullOrEmpty(obj.Base64Image))
            {
                string rootPath = _configuration["Web:DocumentPath"];
                string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(encodeImage);
                string folderPath = $"\\Documents\\Trainers\\{nextIdentity}";
                string fullPath = $"{rootPath}\\Documents\\Trainers\\{nextIdentity}";
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
                string fileName = Guid.NewGuid().ToString() + ".jpg";

                File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                obj.PhotoPath = Path.Combine(folderPath, fileName);
            }

            Fe_users userRecord = _mapper.Map<TrainersDTO, Fe_users>(obj);
            userRecord.User_name = obj.FirstName + " " + obj.LastName;
            userRecord.User_type = "Trainers";
            userRecord.Approval_status = "Approved";
            userRecord.Password = CryptoEngine.Encrypt(randomPassword);
            userRecord.Is_active = true;
            userRecord.Last_login_date = null;
            userRecord.User_id = _UserRepository.Insert(userRecord);

            // save customer record in fe_customer
            Fe_trainers trainer = _mapper.Map<TrainersDTO, Fe_trainers>(obj);
            trainer.User_id = userRecord.User_id;
            long trainerId = _FeTrainersRepository.Insert(trainer);
            return obj;
        }

        public List<TrainersDTO> GetList()
        {
            throw new NotImplementedException();
        }

        public bool Delete(TrainersDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(TrainersDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrainersDTO> loadGrid(string[] parameters)
        {
            throw new NotImplementedException();
        }

        public TrainersDTO Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrainerResponseDTO> GetTrainersByAgencyId(TrainerRequestDTO obj)
        {
            var trainers = _FeTrainersRepository.GetList(Common.Database.MAIN, TrainersDAO.GetTrainersByAgencyId, new { AgencyId = obj.AgencyId });
            return _mapper.Map<IEnumerable<Fe_trainers>, IEnumerable<TrainerResponseDTO>>(trainers);
        }
    }
}
