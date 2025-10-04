using System;
using System.Collections.Generic;

namespace BU.DTO.DTOs.RequestDTO.FCM
{
	public class NotificationRequestDTO
	{
        public class NotificationRequest
        {
      
            public string Title { get; set; }
            public string Body { get; set; }
            public long SenderId { get; set; }
            public long ReciverId { get; set; }
            public Dictionary<string, string> Data { get; set; }


        }

    }
}

