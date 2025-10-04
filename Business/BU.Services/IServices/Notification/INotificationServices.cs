
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.RequestDTO.FCM;


namespace BU.Services.IServices.Notification
{
    public interface INotificationService
    {
        SaveFcmTokenRequestDTO Add(SaveFcmTokenRequestDTO obj);
        //FeCustomerDTO GetByUserId(long userId);
        //void Update(SaveFcmTokenRequestDTO entity);



    }

}

