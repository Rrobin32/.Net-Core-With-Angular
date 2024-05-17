using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;

namespace DataLayer.UsersDAL
{
    public interface IUserDAL
    {
        List<UserResponse> GetUserInfo(GetUserInfo userInfoDto);
    }
}
