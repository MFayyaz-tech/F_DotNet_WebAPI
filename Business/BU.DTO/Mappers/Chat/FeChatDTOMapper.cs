using AutoMapper;
using BU.DTO.DTOs.Chat;
using BU.DTO.DTOs.ResponseDTO.Chat;
using DA.Entities.Chat;

namespace BU.DTO.Mappers.Chat
{
    public class FeChatDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_chat, FeChatDTO>()
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.Chat_id))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Sender_id))
                .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.Receiver_id))
                   .ForMember(dest => dest.MessageType, opt => opt.MapFrom(src => src.Message_type))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => src.Is_read))
                .ForMember(dest => dest.MessageType, opt => opt.MapFrom(src => src.Message_type))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
                .ReverseMap();


            cfg.CreateMap<Fe_chat, ChatListDTO>()
               .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.Chat_id))
               .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Sender_id))
               .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.Receiver_id))
               .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
               .ForMember(dest => dest.SenderUserName, opt => opt.MapFrom(src => src.Sender_user_name))
               .ForMember(dest => dest.ReceiverUserName, opt => opt.MapFrom(src => src.Receiver_user_name))
               .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
               .ReverseMap();
        }
    }
}
