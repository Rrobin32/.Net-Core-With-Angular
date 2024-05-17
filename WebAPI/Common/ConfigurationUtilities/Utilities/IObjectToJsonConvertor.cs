using System.Data;
using System.Text;
using ConfigurationUtilities.Generic;

namespace ConfigurationUtilities.Utilities
{
    public interface IObjectToJsonConvertor
    {
        string ConvertStringBuilderToJSON(StringBuilder stringBuilder);
        string ConvertListToJSON<T>(List<ResponseMessage> errors, List<T> list) where T : class;
        string ConvertDataTableToJSON(List<ResponseMessage> errors, DataTable dataTable);
        string ConvertDataSetToJSON(List<ResponseMessage> errors, DataSet dataSet);
        string ConvertClassToJSON<T>(List<ResponseMessage> errors, T item) where T : class;
    }
}
