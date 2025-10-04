using AutoMapper;
using DTO.DTOs.Setup;
using Entities.Setup;

namespace DTO.Mappers.Setup
{
    public class RoleDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Role, RoleDTO>()
            .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.Role_id))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role_name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Approval_permission))
            .ForMember(dest => dest.RoleType, opt => opt.MapFrom(src => src.Role_type))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.Is_default))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ReverseMap();
        }
    }
}
