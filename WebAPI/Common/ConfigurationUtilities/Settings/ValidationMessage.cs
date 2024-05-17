using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationUtilities.Settings
{
    public sealed class ValidationMessages
    {
        public static List<CodeMessage> CodeMessages { get; set; }
    }

    public class CodeMessage
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
