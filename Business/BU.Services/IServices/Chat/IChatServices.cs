using BU.DTO.DTOs.Chat;
using BU.DTO.DTOs.ResponseDTO.Chat;
using DTO.DTOs.User;
using System.Collections.Generic;

namespace BU.Services.IServices.Chat
{
    public interface IFeChatService
    {
        FeChatDTO Add(FeChatDTO obj);
        IEnumerable<FeChatDTO> GetChatsBetweenUsers(long senderId, long receiverId, int pageNumber = 1, int pageSize = 10);
        IEnumerable<ChatListDTO> GetChatList(UserDTO userId);


    }
}
