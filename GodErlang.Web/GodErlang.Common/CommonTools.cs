using System;
using System.Security.Cryptography;
using System.Text;
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

        public static bool isValidPhone(string phone)
        {
            phone = phone?.Trim();
            if (string.IsNullOrEmpty(phone)) return false;

            return Regex.IsMatch(phone, "^(0|86|17951)?(13[0-9]|15[0-9]|17[03678]|18[0-9]|19[0-9]|14[579])[0-9]{8}$");
        }

        public static bool isValidEmail(string email)
        {
            email = email?.Trim();
            if (string.IsNullOrEmpty(email)) return false;

            return Regex.IsMatch(email, "^[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$");
        }

        #region md5 16位 32位 加密
        /// <summary>
        /// MD5 16位加密 加密后密码为小写
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string GetMd5Str16(string ConvertString)
        {
            try
            {
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
                    return t2.Replace("-", "").ToLower();
                }
            }
            catch { return null; }
        }

        /// <summary>
        /// MD5 32位加密 加密后密码为小写
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetMd5Str32(string text)
        {
            try
            {
                using (MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider())
                {
                    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    return sBuilder.ToString().ToLower();
                }
            }
            catch { return null; }
        }
        #endregion
    }
}
