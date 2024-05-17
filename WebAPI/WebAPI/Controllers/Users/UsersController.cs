using AppServices.UserService;
using ConfigurationUtilities.Generic;
using ConfigurationUtilities.Utilities;
using DataTransferObject.DBModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;
using System.Reflection;
using WebAPI.ExtensionHelper;

namespace WebAPI.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseController
    {

        private readonly IUserAppService _iuserAppService; 
        private readonly IObjectToJsonConvertor _iobjectToJsonConvertor; 

        public UsersController(IUserAppService userAppService, IObjectToJsonConvertor jsonConvertor)
        {
            _iuserAppService = userAppService;
            _iobjectToJsonConvertor = jsonConvertor;
        }

        /// <summary>
        /// Fetch the list of user available in the system
        /// </summary>
        /// <param name="getUserInfo"></param>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo([FromQuery] GetUserInfo getUserInfo)
        {
            List<UserResponse> userResponse = new List<UserResponse>();
            List<ResponseMessage> errors = _iuserAppService.GetUserInfo(getUserInfo, ref userResponse);
            return Ok(_iobjectToJsonConvertor.ConvertListToJSON(errors, userResponse));
        }

        [HttpPost]
        public IActionResult AddUserInfo([FromBody] AddUserInfo addUserInfo)
        {
            AddUserInfoResponseObj addUserResponse = new();
            List<ResponseMessage> errors = _iuserAppService.AddUserInfo(addUserInfo, ref addUserResponse);
            return Ok(_iobjectToJsonConvertor.ConvertClassToJSON(errors, addUserResponse));
        }

    }
}
