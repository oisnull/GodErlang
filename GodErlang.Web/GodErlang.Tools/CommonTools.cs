using System;
using System.Text.RegularExpressions;

namespace GodErlang.Tools
{
    public class CommonTools
    {
        public static decimal GetDecimal(string content)
        {
            decimal.TryParse(content, out decimal result);
            return result;
        }

        public static float GetFloat(string content)
        {
            float.TryParse(content, out float result);
            return result;
        }

        public static decimal ExtractFirstPrice(string content)
        {
            content = content?.Trim();
            if (string.IsNullOrEmpty(content)) return 0;

            Match match = Regex.Match(content, @"([0-9,]+(\.[0-9]{2})?)");
            if (match.Success)
            {
                return GetDecimal(match.Value);
            }

            return 0;
        }

    }
}
