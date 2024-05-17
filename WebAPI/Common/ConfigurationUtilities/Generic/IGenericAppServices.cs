using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationUtilities.Generic
{
    public interface IGenericAppServices
    {
        D AutoMapper<S, D>(S model);
        List<ResponseMessage> ConvertList<T>(List<T> list) where T : class;
        List<ResponseMessage> ConvertDataTable(DataTable dataTable);
        List<ResponseMessage> ConvertDataSet(DataSet dataSet);
        List<ResponseMessage> ConvertStringBuilder(StringBuilder stringBuilder);
        List<ResponseMessage> ConvertClass<T>(T item) where T : class;
    }
}
