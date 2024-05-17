using AutoMapper;
using ConfigurationUtilities.Utilities;
using System.Data;
using System.Text;

namespace ConfigurationUtilities.Generic
{
    public class GenericAppServices : IGenericAppServices
    {

        public D AutoMapper<S, D>(S model)
        {
            MapperConfiguration config = new MapperConfiguration(myObj =>
            {
                myObj.CreateMap<S, D>();
            });
            IMapper iMapper = config.CreateMapper();
            D dto = iMapper.Map<S, D>(model);
            return dto;
        }

        public List<ResponseMessage> ConvertList<T>(List<T> list) where T : class
        {
            return GetErrors(list.Count > 0);
        }

        public List<ResponseMessage> ConvertDataTable(DataTable dt)
        {
            return GetErrors(dt.Rows.Count > 0);
        }

        public List<ResponseMessage> ConvertDataSet(DataSet dataSet)
        {
            List<ResponseMessage> errors = new List<ResponseMessage>();
            bool isExist = false;
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                if (dataSet.Tables[i].Rows.Count > 0)
                {
                    isExist = true;
                    break;
                }
            }
            return GetErrors(isExist);
        }

        public List<ResponseMessage> ConvertStringBuilder(StringBuilder stringBuilder)
        {
            return Responses.StringBuilder(stringBuilder);
        }

        public List<ResponseMessage> ConvertClass<T>(T item) where T : class
        {
            return GetErrors(item != null);
        }

        private List<ResponseMessage> GetErrors(bool expression)
        {
            return expression ? Responses.RecordFound() : Responses.RecordNotFound();
        }
    }
}
