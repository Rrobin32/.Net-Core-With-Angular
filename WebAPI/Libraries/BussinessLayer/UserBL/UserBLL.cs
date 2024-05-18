using ConfigurationUtilities.Settings;
using ConfigurationUtilities.Utilities;
using DataLayer.UsersDAL;
using DataTransferObject.DBModel;
using Models.InputModel.LoginInputModel;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;
using System.Text;

namespace BussinessLayer.UserBL
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserDAL _iuserDAL;


        public UserBLL(IUserDAL iuserDAL)
        {
            _iuserDAL = iuserDAL;
        }

        public User GetUser(string userName)
        {
            return _iuserDAL.GetUser(userName);
        }

        public List<UserResponse> GetUserInfo(GetUserInfo userInfoDto)
        {
            return _iuserDAL.GetUserInfo(userInfoDto);
        }

        public void AddUserInfo(AddUserInfo userInfoDto)
        {
            _iuserDAL.AddUserInfo(userInfoDto);
        }

        public StringBuilder ValidateUser(AddUserInfo userInfoDto)
        {
            StringBuilder validationMessage = new StringBuilder();
            User user = _iuserDAL.GetUser(userInfoDto.UserName);
            if(user.Id > 0)
            {
                Validators.Message(validationMessage, (int)Uservalidation.UserAlreadyExist);
            }
            return validationMessage;
        }

        public StringBuilder ValidateUserCredential(LoginInputModel loginInputModel)
        {
            StringBuilder validationMessage = new StringBuilder();
            User user = _iuserDAL.ValidateUserCredential(loginInputModel);
            if (user.Id == 0)
            {
                Validators.Message(validationMessage, (int)Uservalidation.InvalidCredentials);
            }
            return validationMessage;
        }

        public void DeleteUserInfo(DeleteUserInfo dto)
        {
            _iuserDAL.DeleteUserInfo(dto);
        }

        public void UpdateUserInfo(UpdateUserInfo dto)
        {
            _iuserDAL.UpdateUserInfo(dto);
        }
    }
}
