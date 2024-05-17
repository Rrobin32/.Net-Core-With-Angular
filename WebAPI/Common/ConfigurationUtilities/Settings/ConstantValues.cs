using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationUtilities.Settings
{
    public sealed class ConstantValues
    {
        public const string MD5Key = "a244bc2be4245c022748235a46dedf15";
        public const string DefaultTime = "T00:00:00";
        public static readonly string DateFormat = string.IsNullOrEmpty(AppSettings.DateFormat) ? "yyyy-MM-ddTHH:mm:ss" : $"{AppSettings.DateFormat}THH:mm:ss";
        public static readonly string Output_DateFormat = $"{DateFormat.Replace(" ", "T")}";
        public const string ShortFormat = @"^\d{0,5}$";
        public const string IntFormat = @"^\d{0,10}$";
        public const string LongFormat = @"^\d{0,19}$";
        public const string DecimalFormat = @"^[+-]?(\d*\.)?\d+$";
        public const string keyAesEncrDecr = "123456$#@$^@1ERF";

        #region Regular Expressions

        public const string RegExNumberNotStartWithZero = "^[1-9][0-9]*$";
        public const string RegExAlphabetsOnly = "[A-Za-z]";
        public const string RegExNumbersOnly = "[0-9]";
        public const string RegExAlphaNumericOnly = "[A-Za-z0-9]";
        // Internet Message Format - RFC2822
        public const string RegExEmailAddressRFC2822 = "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        //Simple Mail Transfer Protocol - RFC 5321
        public const string RegExEmailAddressRFC5321 = "([!#-'*+/-9=?A-Z^-~-]+(\\.[!#-'*+/-9=?A-Z^-~-]+)*|\"([]!#-[^-~ \\t]|(\\\\[\\t -~]))+\")@([0-9A-Za-z]([0-9A-Za-z-]{0,61}[0-9A-Za-z])?(\\.[0-9A-Za-z]([0-9A-Za-z-]{0,61}[0-9A-Za-z])?)*|\\[((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])(\\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|IPv6:((((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){6}|::((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){5}|[0-9A-Fa-f]{0,4}::((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){4}|(((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):)?(0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}))?::((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){3}|(((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){0,2}(0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}))?::((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){2}|(((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){0,3}(0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}))?::(0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):|(((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){0,4}(0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}))?::)((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):(0|[1-9A-Fa-f][0-9A-Fa-f]{0,3})|(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])(\\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3})|(((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){0,5}(0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}))?::(0|[1-9A-Fa-f][0-9A-Fa-f]{0,3})|(((0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}):){0,6}(0|[1-9A-Fa-f][0-9A-Fa-f]{0,3}))?::)|(?!IPv6:)[0-9A-Za-z-]*[0-9A-Za-z]:[!-Z^-~]+)])";
        // Internet Message Format - RFC5322
        public const string RegExEmailAddressRFC5322 = "([!#-'*+/-9=?A-Z^-~-]+(\\.[!#-'*+/-9=?A-Z^-~-]+)*|\"([]!#-[^-~ \\t]|(\\\\[\\t -~]))+\")@([!#-'*+/-9=?A-Z^-~-]+(\\.[!#-'*+/-9=?A-Z^-~-]+)*|\\[[\\t -Z^-~]*])";
        //Update to Internet Message Format to Allow Group Syntax in the "From:" and "Sender:" Header Fields (Updates RFC 5322) - RFC6854
        public const string RegExEmailAddressRFC6854 = "([-!#-'*+/-9=?A-Z^-~]+(\\.[-!#-'*+/-9=?A-Z^-~]+)*|\"([]!#-[^-~ \\t]|(\\\\[\\t -~]))+\")@([-!#-'*+/-9=?A-Z^-~]+(\\.[-!#-'*+/-9=?A-Z^-~]+)*|\\[[\\t -Z^-~]*])";

        public const string RegExAtleastOneNonSpaceCharacter = @"^(?!\s*$).+";
        public const string RegExNumberwithfourdigits = "[0-9]{4}$";
        #endregion


        public const string ModeCreate = "C";
        public const string ModeRead = "R";
        public const string ModeUpdate = "U";
        public const string ModeDelete = "D";

        public const string DefaultFlag = "V";

        //[TO DO] add these status to a common table and have reference in orders table
        public const string Status_New = "New";
        public const string Status_In_Progress = "In-progress";
        public const string Status_Completed = "Completed";
        public const string Status_Assigned = "Assigned";
        public const string Status_Overdue = "Overdue";
        public const string Status_Hold = "Hold";
        public const string Status_To_Be_Assigned = "ToBeAssigned";


        public const string User_Operator = "Operator";



        public const int New_Splited_User_Id = 0;




        #region Application Success/Failuer Responses
        public const string FailureMessage = "Error Occured.";

        public const string BulkAssignmentSuccessMessage = "Bulk-Assignment Done Successfully.";
        public const string BulkAssignmentFailureMessage = "Please Provide Valid Data For Bulk-Assignment.";
        public const string BulkAssignmentLessThanTwoMessage = "Please provide more than one Sales Order data for Bulk-Assignment.";

        public const string UserFunctionsValidInformationMessage = "Please Provide Valid User-Function List.";
        public const string UserQuestionsValidInformationMessage = "Please Provide Valid User-Questions List.";

        public const string OrderSplitSuccessMessage = "Order Splitted Successfully.";
        public const string OrderUpdateSuccessMessage = "Order Updated Successfully.";


        public const string LocationAddSuccessMessage = "Location Added Successfully.";
        public const string LocationUpdateSuccessMessage = "Location Updated Successfully.";
        public const string LocationDeleteSuccessMessage = "Location Deleted Successfully.";
        #endregion

        #region Server Error Responses

        public const string ServiceInternalServerError = "Error while handling your request, try api documentation to use the service correctly.";

        #endregion

    }

    public static class EnumConstants
    {
        public enum DBOperation
        {
            CREATE = 1,
            UPDATE = 2,
            DELETE = 3,
            READ = 4,
            SOFTDELETE = 5,
        }
        public enum ObjectStatus
        {
            ACTIVE = 1,
            INACTIVE = 2,
        }
        /// <summary>
        /// Defines all the object
        /// </summary>
        public enum EntityType
        {
            AreaOrLocation = 1,
            Audit = 2,
            Bay = 3,
            Customer = 4,
            Device = 5,
            Function = 6,
            Language = 7,
            Login = 8,
            Order = 9,
            OrderItem = 10,
            Prioritie = 11,
            SystemConfiguration = 12,
            User = 13,
            UserGroup = 14,
            Warehouse = 15,
        }

        public enum UserGroupType
        {
            Operator = 1000,
            Supervisor = 1001,
            Admin = 1002,
        }

        public enum FunctionTypes
        {
            Picking = 1,
            PutAway = 2,
            Inventory = 3,
            Inbound = 4,
            Outbound = 5,
            Replenishment = 6,
        }
        public enum AuditOrderLevel
        {
            Header = 1,
            Item = 2,
            Split = 3,
            BulkAssign = 4,
        }
    }


}
