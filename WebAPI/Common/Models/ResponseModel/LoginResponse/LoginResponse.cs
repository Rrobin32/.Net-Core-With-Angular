using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseModel.LoginResponse
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public DateTime? TokenExpiryDatetime { get; set; }
    }
}
