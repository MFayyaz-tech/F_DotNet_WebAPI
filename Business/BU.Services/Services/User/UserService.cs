using AutoMapper;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO;
using BU.DTO.DTOs.RequestDTO.FCM;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Users;
using Common;
using Common.Helper;
using DA.DAO.DAO.Trainings;
using DA.Entities.Jobs;
using DA.Entities.Trainings;
using DAO;
using DAO.DAO.User;
using DTO.DTOs.User;
 using Entities;
using Entities.Users;
using FH.Services.IServices.User;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using Services.IServices.Email;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FH.Services.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<Fe_users> _UserRepository;
		private readonly IEmailService _emailService;
		IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IRepository<Fe_users> UserRepository, IEmailService emailService, IMapper mapper, IConfiguration configuration)
        {
            _UserRepository = UserRepository;
			_emailService = emailService;
            _configuration = configuration;
            _mapper = mapper;
        }
        
       
		public UserDTO Add(UserDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
			Fe_users data = _mapper.Map<UserDTO, Fe_users>(obj);
            data.Last_login_date = null;
            data.User_id = _UserRepository.Insert(data);
            return _mapper.Map<Fe_users, UserDTO>(data);
        }
        public List<UserDTO> GetList()
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
			Fe_users data = _UserRepository.Get(Database.MAIN, obj.UserId);
            data.Is_deleted = true;
            data.Updated_by = obj.UpdatedBy;
            return _UserRepository.Update(data);
        }

		public bool Reject(UserDTO obj)
		{
			if (!string.IsNullOrWhiteSpace(obj.EncUserID))
			{
				obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
			}
			Fe_users data = _UserRepository.Get(Database.MAIN, obj.UserId);
			data.Is_deleted = true;
			data.Updated_by = obj.UpdatedBy;
			return _UserRepository.Update(data);
		}

		public bool Approve(UserDTO obj)
		{
			if (!string.IsNullOrWhiteSpace(obj.EncUserID))
			{
				obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
			}
			Fe_users data = _UserRepository.Get(Database.MAIN, obj.UserId);
			data.Is_deleted = true;
			data.Updated_by = obj.UpdatedBy;
			return _UserRepository.Update(data);
		}
		public bool Update(UserDTO obj)
        {
			Fe_users data = _mapper.Map<UserDTO, Fe_users>(obj);
            bool Succes = false;
            data.Updated_by = Convert.ToInt64(CryptoEngine.Decrypt(obj.EncUserID));
            Succes = _UserRepository.Update(data);
            return Succes;
        }
        public IEnumerable<UserDTO> loadGrid(string[] parameters)
        {
            return _mapper.Map<IEnumerable<Fe_users>, IEnumerable<UserDTO>>(_UserRepository.GetSearchData(Database.MAIN));
        }
        public UserDTO Get(long id)
        {
			Fe_users obj = _UserRepository.Get(Database.MAIN, id);
            UserDTO objDto = _mapper.Map<Fe_users, UserDTO>(obj);
            return objDto;
        }

		public UserDTO CheckIfDuplicateUserExists(UserDTO obj)
		{
			var userExist = _UserRepository.GetList(Database.MAIN, UserDAO.IsUserExistQuery, new { @Email = obj.EmailAddress, @UserId = obj.UserId }).FirstOrDefault();
			return _mapper.Map<Fe_users,UserDTO>(userExist);
		}

        public GetUserIdDTO GetAgencyUserId(GetUserIdDTO id)
        {
            var userList = _UserRepository.GetList(Database.MAIN, UserDAO.GetAgencyByUserId, new { AgencyId = id.UserId });
            var user = userList.FirstOrDefault(); // Get the first user if available

            if (user == null)
            {
                throw new Exception("User not found");
            }

            GetUserIdDTO getUserIdDTO = _mapper.Map<Fe_users, GetUserIdDTO>(user);
            return getUserIdDTO;
        }

        public GetUserIdDTO GetCustomerUserId(GetUserIdDTO id)
        {
            var userList = _UserRepository.GetList(Database.MAIN, UserDAO.GetCustomerByUserId, new { CustomerId = id.UserId });
            var user = userList.FirstOrDefault(); // Get the first user if available

            if (user == null)
            {
                throw new Exception("User not found");
            }

            GetUserIdDTO getUserIdDTO = _mapper.Map<Fe_users, GetUserIdDTO>(user);
            return getUserIdDTO;
        }

       
        public UserDTO GetUserByIdAsync(string userId)
        {
            {
                // Fetch the user list based on the CustomerId
                var userList = _UserRepository.GetList(Database.MAIN, UserDAO.GetUserByUserId, new { userId }).FirstOrDefault(); ;

            

                if (userList == null)
                {
                    throw new Exception("User not found");
                }

                // Map the user entity to the DTO
              
                return _mapper.Map<Fe_users, UserDTO>(userList);
            }
        }

        public bool ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            Fe_users userList = _UserRepository.GetList(Database.MAIN, UserDAO.GetUserById, new { UserId= userId }).FirstOrDefault();

            if (userList == null)
            {
                throw new Exception("User not found");
            }
            var encPassword = CryptoEngine.Encrypt(currentPassword);

            if (userList.Password != encPassword) {

                throw new Exception("Current password is incorrect" );

            }

            // Step 4: Update the user password
            userList.Password = encPassword;
            var result = _UserRepository.Update(userList);

            return result;
        }

        public SaveFcmTokenRequestDTO SaveFcmToken(SaveFcmTokenRequestDTO obj)
        {
            long updatedBy = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                updatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            var user = _UserRepository.Get(Database.MAIN, obj.UserId);
            //user.FcmToken = obj.FcmToken;
            user.Updated_by = updatedBy;
            _UserRepository.Update(user);
            return obj;
        }

        public UserDTO CheckIfDuplicateUserExistsByEmail(UserDTO obj)
        {
            var userExist = _UserRepository.GetList(Database.MAIN, UserDAO.IsEmailExistQuery, new { @Email = obj.EmailAddress }).FirstOrDefault();
            return _mapper.Map<Fe_users, UserDTO>(userExist);
        }

        public bool ActivateUser(AuthPaymentRequestDTO obj)
        {
            var userList = _UserRepository.GetList(Database.MAIN, UserDAO.GetUserById, new { UserId = obj.UserId });
            var user = userList.FirstOrDefault(); 
            if (user != null)
            {
                user.Is_active = true;
                _UserRepository.Update(user);
               
                return true;
            }
            return false;
        }
    }
}
