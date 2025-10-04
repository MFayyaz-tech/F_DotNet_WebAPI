using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace BU.DTO.DTOs.RequestDTO.Trainings
{
    public class TrainingFeedBackRequestDTO
    {
        public long TrainingId {  get; set; }
        public long CustomerId { get; set; }
    }
}
