using DataTransferObject.DBModel;
using Models.InputModel.LoginInputModel;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;

namespace DataLayer.UsersDAL
{
    public interface IUserDAL
    {
        void AddUserInfo(AddUserInfo userInfoDto);
        User GetUser(string userName);
        List<UserResponse> GetUserInfo(GetUserInfo userInfoDto);
        User ValidateUserCredential(LoginInputModel loginInputModel);
    }
}
