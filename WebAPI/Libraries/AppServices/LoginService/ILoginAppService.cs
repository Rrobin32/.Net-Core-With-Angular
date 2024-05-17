using ConfigurationUtilities.Generic;
using Models.InputModel.LoginInputModel;
using Models.ResponseModel.LoginResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.LoginService
{
    public interface ILoginAppService
    {
        List<ResponseMessage> GenrateToken(LoginInputModel loginInputModel, ref LoginResponse loginResponse);
    }
}
