using AppServices.UserService;
using ConfigurationUtilities.Generic;
using ConfigurationUtilities.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;
using System.Reflection;

namespace WebAPI.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {

        private readonly IUserAppService _iuserAppService; 
        private readonly IObjectToJsonConvertor _iobjectToJsonConvertor; 

        public UsersController(IUserAppService userAppService, IObjectToJsonConvertor jsonConvertor)
        {
            _iuserAppService = userAppService;
            _iobjectToJsonConvertor = jsonConvertor;
        }

        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo([FromQuery] GetUserInfo getUserInfo)
        {
            List<UserResponse> userResponse = new List<UserResponse>();
            List<ResponseMessage> errors = _iuserAppService.GetUserInfo(getUserInfo, ref userResponse);
            return Ok(_iobjectToJsonConvertor.ConvertListToJSON(errors, userResponse));
        }
    }
}
