using System.Text.RegularExpressions;

namespace RestfulAPIWithAspNet.Helper
{
    public class DatabaseHelper
    {
        public static string SanitizeSqlParameter(string stringValue)
        {
            if (null == stringValue)
                return stringValue;

            stringValue = Regex.Replace(stringValue, "-{2,}", "-"); // transforms multiple --- in - use to comment in sql scripts
            stringValue = Regex.Replace(stringValue, @"[*/]+", string.Empty);// removes / and * used also to comment in sql scripts
            stringValue = Regex.Replace(stringValue, @"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase);

            return stringValue;
        }
    }
}
