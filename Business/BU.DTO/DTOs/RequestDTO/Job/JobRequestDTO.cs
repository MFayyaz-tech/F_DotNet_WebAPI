using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.RequestDTO.Job
{
    public class JobRequestDTO : BaseDTO
    {
        public long JobId { get; set; }  
        public long ContractId { get; set; }  
        public long CustomerId { get; set; }
        public int CustomerRating { get; set; }
        public string FeedBack { get; set; }
        public string AttachmentMediaBase64 { get; set; }
        public string CancelationReason { get; set; }

    }
}
