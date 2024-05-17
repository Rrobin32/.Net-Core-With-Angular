using BussinessLayer.UserBL;
using ConfigurationUtilities.Generic;
using ConfigurationUtilities.Settings;
using DataTransferObject.DBModel;
using Microsoft.IdentityModel.Tokens;
using Models.InputModel.LoginInputModel;
using Models.ResponseModel.LoginResponse;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppServices.LoginService
{
    public class LoginAppService : ILoginAppService
    {
        private readonly IGenericAppServices _igenericAppServices;
        private readonly IUserBLL _iuserBLL;

        public LoginAppService(IGenericAppServices igenericAppServices, IUserBLL iuserBLL)
        {
            _igenericAppServices = igenericAppServices;
            _iuserBLL = iuserBLL;
        }

        public List<ResponseMessage> GenrateToken(LoginInputModel loginInputModel, ref LoginResponse loginResponse)
        {
            StringBuilder message = _iuserBLL.ValidateUserCredential(loginInputModel);
            if (message.Length > 0)
            {
                return _igenericAppServices.ConvertStringBuilder(message);
            }
            else
            {
                loginResponse = GenerateJwtToken(loginInputModel);
                return _igenericAppServices.ConvertClass(loginResponse);
            }
        }

        public LoginResponse GenerateJwtToken(LoginInputModel loginModel)
        {
            var secretKey = Encoding.UTF8.GetBytes(JWT.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] {
                        new Claim("UserName", loginModel.UserName),
                        new Claim(JwtRegisteredClaimNames.Aud, JWT.ValidAudience),
                        new Claim(JwtRegisteredClaimNames.Iss, JWT.ValidIssuer)
                    }),
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(JWT.ExpireTime)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            LoginResponse jwtTokenResponse = new LoginResponse()
            {
                Token = tokenString,
                TokenExpiryDatetime = token.ValidTo.ToLocalTime()
            };
            return jwtTokenResponse;
        }
    }
}
