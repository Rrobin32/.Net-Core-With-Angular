using DataLayer.UsersDAL;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;

namespace BussinessLayer.UserBL
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserDAL _iuserDAL;


        public UserBLL(IUserDAL iuserDAL)
        {
            _iuserDAL = iuserDAL;
        }

        public UserResponse GetUser(string operatorId)
        {
            throw new NotImplementedException();
        }

        public List<UserResponse> GetUserInfo(GetUserInfo userInfoDto)
        {
            return _iuserDAL.GetUserInfo(userInfoDto);
        }
    }
}
