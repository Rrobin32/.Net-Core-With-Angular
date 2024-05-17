using AppServices.LoginService;
using Azure;
using ConfigurationUtilities.Generic;
using ConfigurationUtilities.Utilities;
using DataTransferObject.DBModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.InputModel.LoginInputModel;
using Models.ResponseModel.LoginResponse;

namespace WebAPI.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IObjectToJsonConvertor _iobjectToJsonConvertor;
        private readonly ILoginAppService _iloginAppService;
        
        public LoginController(IObjectToJsonConvertor objectToJsonConvertor, ILoginAppService loginAppService)
        {
            _iobjectToJsonConvertor = objectToJsonConvertor;
            _iloginAppService = loginAppService;
        }

        /// <summary>
        /// Allow user to login into system
        /// </summary>
        /// <param name="loginInputModel"></param>
        /// <returns>token</returns>
        [HttpGet]
        public IActionResult Login([FromQuery] LoginInputModel loginInputModel)
        {
            LoginResponse loginResponse = new();
            List<ResponseMessage> errors = _iloginAppService.GenrateToken(loginInputModel, ref loginResponse);
            return Ok(_iobjectToJsonConvertor.ConvertClassToJSON(errors, loginResponse));
        }
    }
}
