using ConfigurationUtilities.Generic;
using DataTransferObject.DBModel;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;

namespace AppServices.UserService
{
    public interface IUserAppService
    {
        List<ResponseMessage> AddUserInfo(AddUserInfo addUserInfo, ref AddUserInfoResponseObj userResponse);
        List<ResponseMessage> DeleteUserInfo(DeleteUserInfo deleteUserInfo, ref DeleteUserInfoResponseObj deleteUserInfoResponseObj);
        List<ResponseMessage> GetUserInfo(GetUserInfo getUserInfo, ref List<UserResponse> userResponse);
        List<ResponseMessage> UpdateUserInfo(UpdateUserInfo updateUserInfo, ref UpdateUserInfoResponseObj updateUserInfoResponseObj);
    }
}
