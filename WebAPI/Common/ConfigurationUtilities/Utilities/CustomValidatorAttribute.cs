using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using ConfigurationUtilities.Settings;

namespace ConfigurationUtilities.Utilities
{
    public class CustomValidatorAttribute
    {
        /// <summary>
        /// Return message string against code from ValidationMessages.json message file
        /// </summary>
        /// <param name="code">code against which message which to be returned</param>
        /// <returns>message string against code</returns>
        public static string CodeMessage(int code)
        {
            return $"{code}~{ValidationMessages.CodeMessages.First(x => x.Code.Equals(code)).Message}";
        }

        /// <summary>
        /// Validate date format against current data
        /// </summary>
        /// <param name="value">datevalue against which date format to be checked</param>
        /// <param name="code">code against which message which to be returned</param>
        /// <param name="dateFormat"></param>
        /// <param name="errorMessage">message string to be returned against code if date format is not matched</param>
        public static void Validate(object value, int code, string dateFormat, ref string errorMessage)
        {
            if (value != null)
            {
                if (!DateTime.TryParseExact(value.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                {
                    errorMessage = $"{CodeMessage(code)} {dateFormat}";
                }
            }
        }

        /// <summary>
        /// digit length to be matched as per input value
        /// </summary>
        /// <param name="value">value against which lenght will be checked</param>
        /// <param name="code">code against which message which to be returned</param>
        /// <param name="min">minimum length of input parameter</param>
        /// <param name="max">maximum length of input parameter</param>
        /// <param name="errorMessage">message string to be returned against code if date format is not matched</param>
        public static void Validate(object value, int code, int min, int max, ref string errorMessage)
        {
            if (value != null && long.TryParse(value.ToString(), out long val))
            {
                if (!(val.ToString().Length >= min && val.ToString().Length <= max))
                {
                    errorMessage = $"{CodeMessage(code)}";
                }
            }
            else
            {
                errorMessage = $"{CodeMessage(code)}";
            }
        }
    }

    public class CustomRequiredAttribute : RequiredAttribute
    {
        private readonly int _code;

        public CustomRequiredAttribute(int code)
        {
            _code = code;
        }

        /// <summary>
        /// check value is valid or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns>message string to be returned against code if value is null or empty</returns>
        public override bool IsValid(object value)
        {
            if (value == null && string.IsNullOrEmpty(Convert.ToString(value)))
            {
                ErrorMessage = $"{CustomValidatorAttribute.CodeMessage(_code)}";
                return false;
            }
            return base.IsValid(value);
        }
    }

    public class CustomRegularExpressionAttribute : RegularExpressionAttribute
    {
        private readonly int _code;
        private readonly string _pattern;

        public CustomRegularExpressionAttribute(int code, string pattern) : base(pattern)
        {
            _code = code;
            _pattern = pattern;
        }

        public override bool IsValid(object value)
        {

            string val = string.Empty;
            if (value == null)
            {
                ErrorMessage = $"{CustomValidatorAttribute.CodeMessage(_code)}";
                return false;
            }
            else
            {
                val = value.ToString();
            }

            if (string.IsNullOrEmpty(val)|| !Regex.IsMatch(val, _pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100)))
            {
                ErrorMessage = $"{CustomValidatorAttribute.CodeMessage(_code)}";
                return false;
            }

            return base.IsValid(value);
        }
    }

    public class CustomRequiredIfAttribute : ValidationAttribute
    {
        private readonly int _code;
        private readonly string _pattern;
        private readonly string _dependent;

        public CustomRequiredIfAttribute(int code, string pattern, string dependent)
        {
            _code = code;
            _pattern = pattern;
            _dependent = dependent;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            PropertyInfo propertyInfo = context.ObjectInstance.GetType().GetProperty(_dependent);
            string val = Convert.ToString(propertyInfo.GetValue(context.ObjectInstance));
            if (Regex.IsMatch(val, _pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100)))
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    return new ValidationResult($"{CustomValidatorAttribute.CodeMessage(_code)}");
                }
            }
            return ValidationResult.Success;
        }
    }

    #region Future Use
    /*
    public class CustomRequiredOptional : RequiredAttribute
    {
        private readonly int _code;

        public CustomRequiredOptional(int code)
        {
            _code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value == null && string.IsNullOrEmpty(Convert.ToString(value)))
            {
                object model = validationContext.ObjectInstance;
                string[] propertyName = model.GetType().GetProperties().Select(a => a.Name).ToArray();
                foreach (string name in propertyName)
                {
                    PropertyInfo? propertyInfo = model.GetType().GetProperty(name);
                    if (propertyInfo != null && propertyInfo.GetValue(model) != null)
                    {
                        return new ValidationResult($"{CustomValidatorAttribute.CodeMessage(_code)}");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }

    public class CustomDateTimeFormatAttribute : DataTypeAttribute
    {
        private readonly int _code;

        public CustomDateTimeFormatAttribute(int code) : base(DataType.Date)
        {
            _code = code;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                //value = value.ToString().Length == 10 ? $"{value}{ConstantValues.DefaultTime}" : value;
                string errorMessage = string.Empty;
                CustomValidatorAttribute.Validate(value, _code, ConstantValues.DateFormat, ref errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ErrorMessage = errorMessage;
                    return false;
                }
            }
            return base.IsValid(value);
        }
    }

    public class CustomShortFormatAttribute : RegularExpressionAttribute
    {
        private readonly int _code;

        public CustomShortFormatAttribute(int code) : base(ConstantValues.ShortFormat)
        {
            _code = code;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (!Regex.IsMatch(Convert.ToString(value), ConstantValues.ShortFormat, RegexOptions.IgnoreCase))
                {
                    ErrorMessage = $"{CustomValidatorAttribute.CodeMessage(_code)}";
                    return false;
                }
            }
            return base.IsValid(value);
        }
    }

    public class CustomIntFormatAttribute : RegularExpressionAttribute
    {
        private readonly int _code;

        public CustomIntFormatAttribute(int code) : base(ConstantValues.IntFormat)
        {
            _code = code;
        }

        public override bool IsValid(object value)
        {
            if (value == null || !Regex.IsMatch(Convert.ToString(value), ConstantValues.IntFormat, RegexOptions.IgnoreCase))
            {
                ErrorMessage = $"{CustomValidatorAttribute.CodeMessage(_code)}";
                return false;
            }
            return base.IsValid(value);
        }
    }

    public class CustomLongFormatAttribute : RegularExpressionAttribute
    {
        private readonly int _code;

        public CustomLongFormatAttribute(int code) : base(ConstantValues.LongFormat)
        {
            _code = code;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (!Regex.IsMatch(value.ToString(), ConstantValues.LongFormat, RegexOptions.IgnoreCase))
                {
                    ErrorMessage = $"{CustomValidatorAttribute.CodeMessage(_code)}";
                    return false;
                }
            }
            return base.IsValid(value);
        }
    }

    public class CustomDecimalFormatAttribute : RegularExpressionAttribute
    {
        private readonly int _code;

        public CustomDecimalFormatAttribute(int code) : base(ConstantValues.DecimalFormat)
        {
            _code = code;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (!Regex.IsMatch(value.ToString(), ConstantValues.DecimalFormat, RegexOptions.IgnoreCase))
                {
                    ErrorMessage = $"{CustomValidatorAttribute.CodeMessage(_code)}";
                    return false;
                }
            }
            return base.IsValid(value);
        }
    }    

    public class CustomRegularExpressionArrayAttribute : ValidationAttribute
    {
        private readonly int _code;
        private readonly string[] _pattern;

        public CustomRegularExpressionArrayAttribute(int code, params string[] pattern)
        {
            _code = code;
            _pattern = pattern;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(value)))
            {
                if (!_pattern.Any(x => Regex.IsMatch(value.ToString(), x, RegexOptions.IgnoreCase)))
                {
                    return new ValidationResult($"{CustomValidatorAttribute.CodeMessage(_code)}");
                }
            }
            return ValidationResult.Success;
        }
    }

    public class CustomStringLengthAttribute : StringLengthAttribute
    {
        private readonly int _code;
        private readonly int _min;
        private readonly int _max;

        public CustomStringLengthAttribute(int code, int minLength = 1, int maxLength = 1000) : base(maxLength)
        {
            _code = code;
            _min = minLength;
            _max = maxLength;
        }

        public override bool IsValid(object value)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(value)))
            {
                if (!(value.ToString().Length >= _min && value.ToString().Length <= _max))
                {
                    ErrorMessage = $"{CustomValidatorAttribute.CodeMessage(_code)}";
                    return false;
                }
            }
            return base.IsValid(value);
        }
    }

    public class CustomRequiredAtleastOneAttribute : ValidationAttribute
    {
        private readonly int _code;
        private readonly string[] _ignore;

        public CustomRequiredAtleastOneAttribute(int code, params string[] ignore)
        {
            _code = code;
            _ignore = ignore;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            IEnumerable<PropertyInfo> propertyInfo = context.ObjectInstance.GetType().GetProperties().Where(x => !_ignore.Contains(x.Name));
            bool val = false;
            foreach (PropertyInfo property in propertyInfo)
            {
                if (property.GetValue(context.ObjectInstance) != null)
                {
                    val = true;
                    break;
                }
            }
            if (!val)
            {
                return new ValidationResult($"{CustomValidatorAttribute.CodeMessage(_code)}");
            }
            return ValidationResult.Success;
        }
    }    

    public class CustomRequiredIfAnyAttribute : ValidationAttribute
    {
        private readonly int _code;
        private readonly string[] _pattern;
        private readonly string _dependent;

        public CustomRequiredIfAnyAttribute(int code, string dependent, params string[] pattern)
        {
            _code = code;
            _pattern = pattern;
            _dependent = dependent;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            PropertyInfo propertyInfo = context.ObjectInstance.GetType().GetProperty(_dependent);
            string val = Convert.ToString(propertyInfo.GetValue(context.ObjectInstance));
            bool empty = true;
            bool match = false;
            foreach (string str in _pattern)
            {
                if (Regex.IsMatch(val, str, RegexOptions.IgnoreCase))
                {
                    match = true;
                    if (!string.IsNullOrEmpty(Convert.ToString(value)))
                    {
                        empty = false;
                        break;
                    }
                }
            }
            if (match && empty)
            {
                return new ValidationResult($"{CustomValidatorAttribute.CodeMessage(_code)}");
            }
            return ValidationResult.Success;
        }
    }

    public class CustomStringArrayAttribute : ValidationAttribute
    {
        private readonly int _code;
        private readonly string[] _array;

        public CustomStringArrayAttribute(int code, params string[] array)
        {
            _code = code;
            _array = array;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(value)))
            {
                if (!_array.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"{CustomValidatorAttribute.CodeMessage(_code)}");
                }
            }
            return ValidationResult.Success;
        }
    } */
    #endregion Future Use

}

