using Shared.CustomExceptions;

namespace Shared
{
    public static class ValidationHelper
    {
        public static void ValidateRequiredStringColumnLength(string value, string field, int maxNumChars)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DataException($"{field} is requiered!");
            }

            if (value.Length > maxNumChars)
            {
                throw new DataException($"{field} must contain less than {maxNumChars} characters");
            }
        }
        public static void ValidateStringColumnLength(string value, string field, int maxNumChars)
        {
            if (value.Length > maxNumChars)
            {
                throw new DataException($"{field} must contain less than {maxNumChars} characters");
            }
        }
    }
}
