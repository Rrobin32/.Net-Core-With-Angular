using ConfigurationUtilities.Settings;
using ConfigurationUtilities.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InputModel.LoginInputModel
{
    public  class LoginInputModel
    {
        [CustomRequired((int)Uservalidation.UserNameRequired)]
        public string UserName { get;set; }
        [CustomRequired((int)Uservalidation.PasswordRequired)]
        public string Password { get;set; }
    }
}
