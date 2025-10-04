using AutoMapper;
using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.ResponseDTO.Job;
using BU.DTO.DTOs.Trainings;
using BU.DTO.DTOs.Users;
using DTO.DTOs.User;
using DTO.DTOs.Users;
using Entities.Users;

namespace DTO.Mappers.User
{
    public class UserDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_users, UserDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User_name))
            .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email_address))
			.ForMember(dest => dest.ApprovalStatus, opt => opt.MapFrom(src => src.Approval_status))
			.ForMember(dest => dest.RejectedReason, opt => opt.MapFrom(src => src.Rejected_reason))
			.ForMember(dest => dest.ResetPasswordToken, opt => opt.MapFrom(src => src.Reset_password_token))
			.ForMember(dest => dest.TokenExpiryDate, opt => opt.MapFrom(src => src.Token_expiry_date))
			.ForMember(dest => dest.ResetPasswordOTP, opt => opt.MapFrom(src => src.Reset_password_OTP))
			.ForMember(dest => dest.OTPExpiryDate, opt => opt.MapFrom(src => src.OTP_expiry_date))
			.ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.Last_login_date))
			.ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role_id))
			.ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.User_type))
			.ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ReverseMap();

			cfg.CreateMap<Fe_users, RegisterAgencyRequestDTO>()
		   .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email_address))
		   .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
		   .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role_id))
           .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.User_type))
		   .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
		   .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
		   .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
		   .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
		   .ReverseMap();

            cfg.CreateMap<Fe_users, CustomerRegistrationRequestDTO>()
           .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email_address))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
           .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role_id))
           .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.User_type))
           .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
           .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
           .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
           .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
           .ReverseMap();

            cfg.CreateMap<Fe_users, UserAuthDTO>()
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User_name))
           .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email_address))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
             .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.Is_active))
                   .ForMember(dest => dest.isDeleted, opt => opt.MapFrom(src => src.Is_deleted))

           .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.User_type))
           .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role_id))
           .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.Last_login_date))
           .ReverseMap();

            cfg.CreateMap<Fe_users, TrainersDTO>()
           .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email_address))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
           .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role_id))
           .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.User_type))
           .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
           .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
           .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
           .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
           .ReverseMap();

            cfg.CreateMap<Fe_users, AgentsDTO>()
           .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email_address))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
           .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role_id))
           .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.User_type))
           .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
           .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
           .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
           .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
           .ReverseMap();

            cfg.CreateMap<Fe_users, GetUserIdDTO>()
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
           .ReverseMap();
        }
    }
}

