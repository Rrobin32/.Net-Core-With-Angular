using ConfigurationUtilities.Generic;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;

namespace AppServices.UserService
{
    public interface IUserAppService
    {
        List<ResponseMessage> GetUserInfo(GetUserInfo getUserInfo, ref List<UserResponse> userResponse);
    }
}
