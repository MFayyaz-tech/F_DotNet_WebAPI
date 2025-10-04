using AutoMapper;
using DTO.DTOs.Setup;
using Entities.Setup;

namespace DTO.Mappers.Setup
{
    public class RolePermissionDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Role_Permission, RolePermissionDTO>()
            .ForMember(dest => dest.RolePermissionID, opt => opt.MapFrom(src => src.Role_permission_id))
            .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.Role_id))
            .ForMember(dest => dest.PermissionID, opt => opt.MapFrom(src => src.Permission_id))
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
