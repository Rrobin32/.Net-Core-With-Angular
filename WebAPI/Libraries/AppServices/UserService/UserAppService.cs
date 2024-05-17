using BussinessLayer.UserBL;
using ConfigurationUtilities.Generic;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;

namespace AppServices.UserService
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserBLL _iuserBLL;
        private readonly IGenericAppServices _igenericAppServices;

        public UserAppService(IUserBLL userBLL, IGenericAppServices genericAppServices)
        {
            _iuserBLL = userBLL;    
            _igenericAppServices = genericAppServices;
        }
        List<ResponseMessage> IUserAppService.GetUserInfo(GetUserInfo getUserInfo, ref List<UserResponse> userResponse)
        {
            GetUserInfo userInfoDto = _igenericAppServices.AutoMapper<GetUserInfo, GetUserInfo>(getUserInfo);
            userResponse = _iuserBLL.GetUserInfo(userInfoDto);
            return _igenericAppServices.ConvertList(userResponse);
        }
    }
}
