using System;
using BU.DTO.DTOs.Trainings;
using System.Collections.Generic;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.ListItem
{
	public class ListItemDTO : BaseDTO
    {
        public long ListItemId { get; set; }
        public string ListType { get; set; }
        public string CodeType { get; set; }
        public string Name { get; set; }
        public long DisplayOrder { get; set; }
        public  string DocumentPath { get; set; }


    }

    public class AllListItemDTO 
    {
        public List<ListItemDTO> AddJobType { get; set; }
        public List<ListItemDTO> LicenceType { get; set; }
        public List<ListItemDTO> AgencyBidType { get; set; }
        public List<ListItemDTO> AddTrainingType { get; set; }
        public List<ListItemDTO> AddTrainerExperinceListType { get; set; }
        public List<ListItemDTO> AddTrainerSkillsType { get; set; }
        public List<ListItemDTO> CancelJobType { get; set; }
        public List<ListItemDTO> ServiceCategries { get; set; }


    }
}

