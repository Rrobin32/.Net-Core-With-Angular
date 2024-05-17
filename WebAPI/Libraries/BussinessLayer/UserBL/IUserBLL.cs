using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;

namespace BussinessLayer.UserBL
{
    public interface IUserBLL
    {
        UserResponse GetUser(string operatorId);
        List<UserResponse> GetUserInfo(GetUserInfo userInfoDto);
    }
}
