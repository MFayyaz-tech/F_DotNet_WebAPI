using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.RequestDTO.FCM
{
    public class SaveFcmTokenRequestDTO : BaseDTO
    {
        public long UserId { get; set; }
        public string Token { get; set; }

    }
}
