using BU.DTO.DTOs.RequestDTO;
using BU.DTO.DTOs.RequestDTO.FCM;
using BU.DTO.DTOs.Users;
using DTO.DTOs.User;
using IN.Common.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FH.Services.IServices.User
{
    public interface IUserService
    {
        UserDTO Add(UserDTO obj);
        UserDTO CheckIfDuplicateUserExists(UserDTO obj);
        UserDTO CheckIfDuplicateUserExistsByEmail(UserDTO obj);
        bool ActivateUser(AuthPaymentRequestDTO obj);
        List<UserDTO> GetList();
        bool Delete(UserDTO obj);
		bool Approve(UserDTO obj);
		bool Reject(UserDTO obj);
		bool Update(UserDTO obj);
        IEnumerable<UserDTO> loadGrid(string[] parameters);
        UserDTO Get(long id);
        GetUserIdDTO GetAgencyUserId(GetUserIdDTO id);

        GetUserIdDTO GetCustomerUserId(GetUserIdDTO id);
        bool ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        UserDTO GetUserByIdAsync(string userId);
        SaveFcmTokenRequestDTO SaveFcmToken(SaveFcmTokenRequestDTO obj);
    }
}
