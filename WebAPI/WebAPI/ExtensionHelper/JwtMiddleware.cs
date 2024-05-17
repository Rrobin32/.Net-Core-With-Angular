using BussinessLayer.UserBL;
using ConfigurationUtilities.Settings;
using DataTransferObject.DBModel;
using Microsoft.IdentityModel.Tokens;
using Models.ResponseModel.UsersResponse;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebAPI.ExtensionHelper
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserBLL _iuserBLL;

        public JwtMiddleware(RequestDelegate next, IUserBLL userBLL)
        {
            _next = next;
            _iuserBLL = userBLL;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                attachUserToContextLogin(context, token);
            }

            await _next(context);
        }

        private void attachUserToContextLogin(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(JWT.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = jwtToken.Claims.First(x => x.Type == "UserName").Value;

                // attach user to context on successful jwt validation
                User userResponses = _iuserBLL.GetUser(userName);
                if(userResponses != null)
                {
                    context.Items["User"] = userResponses;
                }
            }
            catch
            {

            }
        }

    }
}
