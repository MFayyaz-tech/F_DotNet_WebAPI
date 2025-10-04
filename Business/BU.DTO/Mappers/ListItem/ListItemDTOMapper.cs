

using AutoMapper;
using BU.DTO.DTOs.Chat;
using BU.DTO.DTOs.ListItem;
using BU.DTO.DTOs.ResponseDTO.Chat;
using DA.Entities.Chat;
using DA.Entities.ItemList;

namespace BU.DTO.Mappers.Chat
{
    public class ListItemDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_item_list, ListItemDTO>()
              .ForMember(dest => dest.ListItemId, opt => opt.MapFrom(src => src.List_item_id))
              .ForMember(dest => dest.ListType, opt => opt.MapFrom(src => src.List_type))
              .ForMember(dest => dest.CodeType, opt => opt.MapFrom(src => src.Code))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(src => src.Display_order))
              .ForMember(dest => dest.DocumentPath, opt => opt.MapFrom(src => src.Document_path))

              .ReverseMap();

        }
    }
}

