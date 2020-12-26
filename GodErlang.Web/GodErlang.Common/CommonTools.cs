using System;
using System.Text.RegularExpressions;

namespace GodErlang.Common
{
    public class CommonTools
    {
        public const string DEFAULT_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public static string GetDefaultFormatNowTime()
        {
            return DateTime.Now.ToString(DEFAULT_TIME_FORMAT);
        }

        public static string GetDefaultFormatTime(DateTime dt)
        {
            return dt.ToString(DEFAULT_TIME_FORMAT);
        }

        public static int? GetTimeSeconds(DateTime? start, DateTime? end)
        {
            if (start == null || end == null) return null;

            TimeSpan ts = end.Value - start.Value;
            if (ts.TotalSeconds > 0)
            {
                return GetInt(ts.TotalSeconds.ToString("0"));
            }
            return null;
        }

        public static decimal GetDecimal(string content)
        {
            decimal.TryParse(content, out decimal result);
            return result;
        }

        public static int GetInt(string content)
        {
            int.TryParse(content, out int result);
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
