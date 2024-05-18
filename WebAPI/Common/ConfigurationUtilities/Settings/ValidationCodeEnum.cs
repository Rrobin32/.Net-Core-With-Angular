using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationUtilities.Settings
{
    public class ValidationCodeEnum
    {
    }

    #region Common Validation 1 to 100
    public enum CommonValidationCode
    {
        Success = 0,
        Error = 1,
        NoRecordFound = 2

    }
    #endregion

    #region User Validation 101 to 200
    public enum Uservalidation
    {
        UserAlreadyExist = 101,
        UserNameRequired = 102,
        PasswordRequired = 103,
        FirstNameRequired = 104,
        InvalidCredentials = 105,
        UserIdRequired = 106
    }
    #endregion
}
