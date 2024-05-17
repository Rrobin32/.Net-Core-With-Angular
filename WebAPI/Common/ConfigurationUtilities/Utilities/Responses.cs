using System.Data;
using System.Text;
using Newtonsoft.Json;
using ConfigurationUtilities.Generic;
using ConfigurationUtilities.Settings;

namespace ConfigurationUtilities.Utilities
{
    public sealed class Responses
    {
        public static List<ResponseMessage> RecordFound()
        {
            return ErrorDetail((int)CommonValidationCode.Success);
        }

        public static void ValidationError(List<ResponseMessage> errors, int code)
        {
            ResponseMessage error = new ResponseMessage
            {
                CustomCode = code,
                Message = Message(code)
            };
            errors.Add(error);
        }

        public static void ValidationError(List<ResponseMessage> errors, int code, string message)
        {
            ResponseMessage error = new ResponseMessage
            {
                CustomCode = code,
                Message = message
            };
            errors.Add(error);
        }

        public static string Response(List<ResponseMessage> errors)
        {
            return Output(errors, string.Empty);
        }

        public static string Response<T>(List<ResponseMessage> errors, T response)
        {
            return Output(errors, response);
        }

        public static List<ResponseMessage> RecordNotFound()
        {
            return ErrorDetail((int)CommonValidationCode.NoRecordFound);
        }

        public static List<ResponseMessage> StringBuilder(StringBuilder stringBuilder)
        {
            List<ResponseMessage> errors = new List<ResponseMessage>();
            StringBuilderError(errors, stringBuilder);
            return errors;
        }

        public static void ValidationMessage(StringBuilder stringBuilder, int code, params string[] array)
        {
            stringBuilder.AppendLine($"{code}~{string.Format(Message(code), array)}");
        }

        private static List<ResponseMessage> ErrorDetail(int code)
        {
            List<ResponseMessage> errors = new List<ResponseMessage>();
            ResponseMessage error = new ResponseMessage
            {
                CustomCode = code,
                Message = Message(code)
            };
            errors.Add(error);
            return errors;
        }

        private static string Message(int code)
        {
            return ValidationMessages.CodeMessages.First(x => x.Code.Equals(code)).Message;
        }

        private static void StringBuilderError(List<ResponseMessage> errors, StringBuilder stringBuilder)
        {
            string[] lines = stringBuilder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                ResponseMessage error = new ResponseMessage
                {
                    CustomCode = Convert.ToInt32(line.Split("~")[0]),
                    Message = line.Split("~")[1]
                };
                errors.Add(error);
            }
        }

        private static string Output<T>(List<ResponseMessage> errors, T response)
        {
            string responseString = "{ }";
            string errorString = string.Empty;
            JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
            {
                DateFormatString = ConstantValues.Output_DateFormat,
            };
            //JsonSerializerSettings.Converters.Add(new JsonFormatter());
            if (errors != null && errors.Count > 0)
            {
                //Nikolash 07 April 2023 -- Removed response null checking from Branch and added to success check condition.
                if (errors.First().CustomCode.Equals(0) && response != null)
                {
                    if (typeof(T) == typeof(DataSet))
                    {
                        responseString = JsonConvert.SerializeObject(response, JsonSerializerSettings);
                    }
                    else if (typeof(T).IsGenericType)
                    {
                        responseString = "{\"" + response.GetType().GetGenericArguments()[0].Name + "\": " + JsonConvert.SerializeObject(response, JsonSerializerSettings) + " }";
                    }
                    else
                    {
                        responseString = "{\"" + typeof(T).Name + "\": " + JsonConvert.SerializeObject(response, JsonSerializerSettings) + " }";
                    }
                }
                errorString = JsonConvert.SerializeObject(errors, JsonSerializerSettings);
            }
            return "{ \"ResponseMessage\": " + errorString + ", \"ResponseData\": " + responseString + " }";
        }
    }
}
