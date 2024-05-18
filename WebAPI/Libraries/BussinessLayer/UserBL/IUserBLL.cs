using DataTransferObject.DBModel;
using Models.InputModel.LoginInputModel;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;
using System.Text;

namespace BussinessLayer.UserBL
{
    public interface IUserBLL
    {
        void AddUserInfo(AddUserInfo userInfoDto);
        void DeleteUserInfo(DeleteUserInfo dto);
        void UpdateUserInfo(UpdateUserInfo dto);
        User GetUser(string userName);
        List<UserResponse> GetUserInfo(GetUserInfo userInfoDto);
        StringBuilder ValidateUser(AddUserInfo userInfoDto);
        StringBuilder ValidateUserCredential(LoginInputModel userInfoDto);
    }
}
