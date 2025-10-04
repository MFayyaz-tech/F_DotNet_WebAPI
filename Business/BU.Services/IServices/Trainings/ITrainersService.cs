using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Trainings
{
    public interface ITrainersService
    {
        TrainersDTO Add(TrainersDTO obj);
        List<TrainersDTO> GetList();
        bool Delete(TrainersDTO obj);
        bool Update(TrainersDTO obj);
        IEnumerable<TrainersDTO> loadGrid(string[] parameters);
        TrainersDTO Get(long id);
        IEnumerable<TrainerResponseDTO> GetTrainersByAgencyId(TrainerRequestDTO obj);
    }
}
