using AutoMapper;
using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Customer;
using BU.DTO.DTOs.RequestDTO.FCM;
using BU.Services.IServices.Customer;
using BU.Services.IServices.Notification;
using Common;
using Common.Helper;
using DA.DAO.DAO.Customer;
using DA.DAO.DAO.Jobs;
using DA.DAO.DAO.Notifications;
using DA.Entities.Agency;
using DA.Entities.Customer;
using DA.Entities.Jobs;
using DA.Entities.Notifications;
using DAO;
using DAO.DAO.User;
using Entities.Users;
using Logging;
using Microsoft.Extensions.Configuration;
using Services.IServices.Email;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BU.Services.Services.Customer
{
    public class FeNotificationServices : INotificationService
    {
        private readonly IRepository<Fe_notifications_tokens> _FeNotificationRepository;
        private readonly IRepository<Fe_users> _UserRepository;

        IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogging _logging;
        public FeNotificationServices(IRepository<Fe_notifications_tokens> FeNotificationRepository, IRepository<Fe_users> userRepository, IMapper mapper, IConfiguration configuration, ILogging logging)
        {
            _FeNotificationRepository = FeNotificationRepository;
            _configuration = configuration;
            _mapper = mapper;
            _UserRepository = userRepository;
            _logging = logging;
        }

        public SaveFcmTokenRequestDTO Add(SaveFcmTokenRequestDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }


            var existingUser = _FeNotificationRepository.GetList(Common.Database.MAIN, FeNotificationsDAO.GetUserToken, new { obj.UserId }).FirstOrDefault(x => x.User_id == obj.UserId);

            if (existingUser != null)
            {
                // Update existing token

                existingUser.token = obj.Token;
                _FeNotificationRepository.Update(existingUser);
            }
            else
            {
                // Insert new token
                var entity = _mapper.Map<Fe_notifications_tokens>(obj);
                _FeNotificationRepository.Insert(entity);
            }

            return obj;
        }




    }
}
