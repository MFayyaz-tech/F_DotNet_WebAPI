using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.Chat
{
    public class FeChatDTO : BaseDTO
    {
   
        public long ChatId { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string Message { get; set; }
        public string UserType { get; set; }
        public bool? IsRead { get; set; }
        public string MessageType { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    
    }
}
