using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InputModel.UsersInputObj
{
    public class UserInputModel
    {
    }

    public class GetUserInfo
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
