using System;
namespace BU.DTO.DTOs.ResponseDTO.Chat
{
	
        public class ChatListDTO
        {
            public long ChatId { get; set; }
            public long SenderId { get; set; }
            public long ReceiverId { get; set; }
            public string Message { get; set; }
            public DateTime CreateDate { get; set; }
            public string SenderUserName { get; set; }
            public string ReceiverUserName { get; set; }
            public string Participant { get; set; }
        
    }
}

