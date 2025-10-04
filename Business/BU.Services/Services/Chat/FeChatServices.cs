using AutoMapper;
using BU.DTO.DTOs.Chat;
using BU.DTO.DTOs.ResponseDTO.Chat;
using BU.Services.IServices.Chat;
using Common;
using Common.Helper;
using DA.DAO.DAO.Chat;
using DA.Entities.Chat;
using DAO;
using DTO.DTOs.User;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;

namespace BU.Services.Services.Chat
{
    public class FeChatService : IFeChatService
    {
        private readonly IRepository<Fe_chat> _FeChatRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public FeChatService(IRepository<Fe_chat> FeChatRepository, IMapper mapper, IConfiguration configuration)
        {
            _FeChatRepository = FeChatRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public FeChatDTO Add(FeChatDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            Fe_chat chat = _mapper.Map<FeChatDTO, Fe_chat>(obj);
            obj.ChatId = _FeChatRepository.Insert(chat);
            return obj;
        }

        public List<FeChatDTO> GetList()
        {
            throw new NotImplementedException();
        }

        public bool Delete(FeChatDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            bool response = _FeChatRepository.Archive(new
            {
                ChatId = obj.ChatId,
                UserId = obj.UpdatedBy
            });
            return response;
        }

        public bool Update(FeChatDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FeChatDTO> LoadGrid(string[] parameters)
        {
            throw new NotImplementedException();
        }

        public FeChatDTO Get(long id)
        {
            throw new NotImplementedException();
        }

        public bool AddChats(List<FeChatDTO> obj)
        {
            _FeChatRepository.BulkInsert(_mapper.Map<List<FeChatDTO>, List<Fe_chat>>(obj));
            return true;
        }

        public IEnumerable<FeChatDTO> GetChatsByUserId(long userId)
        {
            var chats = _FeChatRepository.GetList(Database.MAIN, FeChatDAO.GetChatsByUserIdQuery, new { UserId = userId });
            return _mapper.Map<IEnumerable<Fe_chat>, IEnumerable<FeChatDTO>>(chats);
        }

        public IEnumerable<FeChatDTO> GetUnreadMessages(long userId)
        {
            var unreadMessages = _FeChatRepository.GetList(Database.MAIN, FeChatDAO.GetUnreadMessagesQuery, new { UserId = userId });
            return _mapper.Map<IEnumerable<Fe_chat>, IEnumerable<FeChatDTO>>(unreadMessages);
        }

        public IEnumerable<FeChatDTO> GetChatsBetweenUsers(long senderId, long receiverId)
        {
            var chats = _FeChatRepository.GetList(Database.MAIN, FeChatDAO.GetChatsBetweenUsersQuery, new { SenderId = senderId, ReceiverId = receiverId });
            return _mapper.Map<IEnumerable<Fe_chat>, IEnumerable<FeChatDTO>>(chats);
        }

        public IEnumerable<ChatListDTO> GetChatList(UserDTO userId)
        {
            // Ensure the UserId property is correctly used from the UserDTO
            var chats = _FeChatRepository.GetList(Database.MAIN, FeChatDAO.GetUserChatList, new { UserId = userId.UserId });

            // Map the list of Fe_chat to a list of ChatListDTO
            var chatList = _mapper.Map<IEnumerable<Fe_chat>, IEnumerable<ChatListDTO>>(chats);

            return chatList;
        }

        public IEnumerable<FeChatDTO> GetChatsBetweenUsers(long senderId, long receiverId, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException("Page number cannot be less than 1.", nameof(pageNumber));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size cannot be less than 1.", nameof(pageSize));
            }

            var offset = (pageNumber - 1) * pageSize;

            var chats = _FeChatRepository.GetList(Database.MAIN, FeChatDAO.GetChatsBetweenUsersQuery, new
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Offset = offset,
                PageSize = pageSize
            });

            var chat = _mapper.Map<IEnumerable<Fe_chat>, IEnumerable<FeChatDTO>>(chats);
            return chat;
        }

    }

}
