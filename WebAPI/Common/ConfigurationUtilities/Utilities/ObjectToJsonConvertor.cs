using System.Data;
using System.Text;
using Microsoft.Extensions.Options;
using ConfigurationUtilities.Generic;
using ConfigurationUtilities.Settings;
using ConfigurationUtilities.Utilities;

namespace ConfigurationUtilities.Utilities
{
    public class ObjectToJsonConvertor : IObjectToJsonConvertor
    {
        private readonly AppSettings _appSettings;
        private readonly ValidationMessages _validationMessages;

        public ObjectToJsonConvertor(IOptionsMonitor<AppSettings> appSettings, IOptionsMonitor<ValidationMessages> validationMessages)
        {
            _appSettings = appSettings.CurrentValue;
            _validationMessages = validationMessages.CurrentValue;
        }
               

        public string ConvertStringBuilderToJSON(StringBuilder stringBuilder)
        {
            List<ResponseMessage> errors = Responses.StringBuilder(stringBuilder);
            return Responses.Response(errors);
        }

        public string ConvertListToJSON<T>(List<ResponseMessage> errors, List<T> list) where T : class
        {
            return Responses.Response(errors, list);
        }

        public string ConvertDataTableToJSON(List<ResponseMessage> errors, DataTable dt)
        {
            DataSet ds = new DataSet();
            if (dt.Rows.Count > 0)
            {
                ds.Merge(dt);
            }
            return Responses.Response(errors, ds);
        }

        public string ConvertClassToJSON<T>(List<ResponseMessage> errors, T item) where T : class
        {
            return Responses.Response(errors, item);
        }

        public string ConvertDataSetToJSON(List<ResponseMessage> errors, DataSet ds)
        {
            return Responses.Response(errors, ds);
        }
    }
}
