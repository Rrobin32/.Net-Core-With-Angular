using System.Text;
using ConfigurationUtilities.Settings;

namespace ConfigurationUtilities.Utilities
{
    public sealed class Validators : Filter
    {
        public static bool Equals<T>(T constant, T value) where T : IComparable
        {
            if (!string.IsNullOrEmpty(Convert.ToString(value)))
            {
                if (typeof(T).Equals(typeof(string)))
                {
                    if (Convert.ToString(constant).Equals(Convert.ToString(value), StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                else
                {
                    if (constant.Equals(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Equals(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Equals(ConstantValues.DefaultFlag))
            {
                return true;
            }
            return false;
        }

        public static void Message(StringBuilder stringBuilder, int code, params string[] array)
        {
            Responses.ValidationMessage(stringBuilder, code, array);
        }
    }
}
