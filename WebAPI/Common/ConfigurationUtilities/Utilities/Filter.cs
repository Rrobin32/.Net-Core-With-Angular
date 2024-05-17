using System.Text;

namespace ConfigurationUtilities.Utilities
{
    public abstract class Filter
    {
        public static bool NoRecord<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }
            else if (list.Count == 0)
            {
                return true;
            }
            return false;
        }

        protected static string FilterCriteria(Dictionary<string, ValuePair> keyValuePairs, out string[] param)
        {
            StringBuilder query = new StringBuilder();
            param = new string[keyValuePairs.Count];
            int i = 0;
            foreach (KeyValuePair<string, ValuePair> keyValue in keyValuePairs)
            {
                if (!string.IsNullOrEmpty(keyValue.Value.Value))
                {
                    if (keyValue.Value.Numeric)
                    {
                        query.Append($"{keyValue.Key} = @{i} AND ");
                        param[i] = keyValue.Value.Value;
                    }
                    else if (keyValue.Value.DateTime)
                    {
                        query.Append($"{keyValue.Key} = @{i} AND ");
                        param[i] = keyValue.Value.Value;
                    }
                    else if (keyValue.Value.Like)
                    {
                        query.Append($"{keyValue.Key}.ToUpper().Contains(@{i}) AND ");
                        param[i] = keyValue.Value.Value.ToUpper();
                    }
                    else
                    {
                        query.Append($"{keyValue.Key}.ToUpper() = @{i} AND ");
                        param[i] = keyValue.Value.Value.ToUpper();
                    }
                    i++;
                }
            }
            if (query.Length > 0)
            {
                return query.Remove(query.Length - 5, 5).ToString();
            }
            return string.Empty;
        }

        protected static string FilterCriteria<T>(KeyValuePair<string, string> keyValuePair, T value) where T : IComparable
        {
            if (!string.IsNullOrEmpty(Convert.ToString(value)))
            {
                StringBuilder query = new StringBuilder();
                return query.Append($"{keyValuePair.Key} <= @0 AND {keyValuePair.Value} >= @0").ToString();
            }
            return string.Empty;
        }

        protected static string FilterCriteria<T>(KeyValuePair<string, T> keyValuePair, out string value) where T : IComparable
        {
            value = Convert.ToString(keyValuePair.Value);
            if (!string.IsNullOrEmpty(Convert.ToString(keyValuePair.Value)))
            {
                StringBuilder query = new StringBuilder();
                if (typeof(T).Equals(typeof(string)))
                {
                    value = value.ToUpper();
                    return query.Append($"{keyValuePair.Key}.ToUpper() = @0").ToString();
                }
                else
                {
                    return query.Append($"{keyValuePair.Key} = @0").ToString();
                }
            }
            return string.Empty;
        }
    }

    public class ValuePair
    {
        public string Value { get; set; }
        public bool Like { get; set; }
        public bool Numeric { get; set; }
        public bool DateTime { get; set; }

        public ValuePair()
        {
            Like = false;
            Numeric = false;
            DateTime = false;
        }
    }
}
