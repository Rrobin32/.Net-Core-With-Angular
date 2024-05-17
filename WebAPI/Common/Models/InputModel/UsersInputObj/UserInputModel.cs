using ConfigurationUtilities.Settings;
using ConfigurationUtilities.Utilities;
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

    public class AddUserInfo
    {
        public long? Id { get; set; }
        [CustomRequired((int)Uservalidation.UserNameRequired)]
        public string UserName { get; set; }
        [CustomRequired((int)Uservalidation.UserNameRequired)]
        public string Password { get; set; }
        [CustomRequired((int)Uservalidation.UserNameRequired)]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public string? Message { get; set; }
    }
}
