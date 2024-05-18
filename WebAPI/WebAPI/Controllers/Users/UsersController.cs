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

        /// <summary>
        /// Add user info
        /// </summary>
        /// <param name="addUserInfo"></param>
        /// <returns></returns>
        [HttpPost("AddUserInfo")]
        public IActionResult AddUserInfo([FromBody] AddUserInfo addUserInfo)
        {
            AddUserInfoResponseObj addUserResponse = new();
            List<ResponseMessage> errors = _iuserAppService.AddUserInfo(addUserInfo, ref addUserResponse);
            return Ok(_iobjectToJsonConvertor.ConvertClassToJSON(errors, addUserResponse));
        }

        /// <summary>
        /// Update User Info
        /// </summary>
        /// <param name="updateUserInfo"></param>
        /// <returns></returns>
        [HttpPost("UpdateUserInfo")]
        public IActionResult UpdateUserInfo([FromBody] UpdateUserInfo updateUserInfo)
        {
            UpdateUserInfoResponseObj updateUserInfoResponseObj = new();
            List<ResponseMessage> errors = _iuserAppService.UpdateUserInfo(updateUserInfo, ref updateUserInfoResponseObj);
            return Ok(_iobjectToJsonConvertor.ConvertClassToJSON(errors, updateUserInfoResponseObj));
        }

        /// <summary>
        /// Delete User Info
        /// </summary>
        /// <param name="updateUserInfo"></param>
        /// <returns></returns>
        [HttpPost("DeleteUserInfo")]
        public IActionResult DeleteUserInfo([FromBody] DeleteUserInfo deleteUserInfo)
        {
            DeleteUserInfoResponseObj deleteUserInfoResponseObj = new();
            List<ResponseMessage> errors = _iuserAppService.DeleteUserInfo(deleteUserInfo, ref deleteUserInfoResponseObj);
            return Ok(_iobjectToJsonConvertor.ConvertClassToJSON(errors, deleteUserInfoResponseObj));
        }

    }
}
