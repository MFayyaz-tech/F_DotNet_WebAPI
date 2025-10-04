using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.RequestDTO.Trainings
{
    public class UploadTrainingFileDTO
    {
        public long TrainingId { get; set; }  
        public IEnumerable<IFormFile> MediaFiles { get; set; }
        public IEnumerable<IFormFile> BannerFiles { get; set; }
    }
}
