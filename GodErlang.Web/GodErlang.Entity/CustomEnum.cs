using System;
using System.Collections.Generic;
using System.Text;

namespace GodErlang.Entity
{
    public class CustomEnum
    {
        public static string GetSourceTypeName(ProductSourceType sourceType)
        {
            string desc = null;
            switch (sourceType)
            {
                case ProductSourceType.Microsoft:
                    desc = "microsoft";
                    break;
                case ProductSourceType.Amazon:
                    desc = "amazon";
                    break;
                case ProductSourceType.EBay:
                    desc = "ebay";
                    break;
                case ProductSourceType.Unknow:
                default:
                    desc = "unknow";
                    break;
            }
            return desc;
        }

        public static ProductSourceType GetSourceTypeByUrl(string url)
        {
            if (string.IsNullOrEmpty(url?.Trim()))
                return ProductSourceType.Unknow;

            Uri uri = new Uri(url);
            if (uri.Host.Contains("microsoft"))
            {
                return ProductSourceType.Microsoft;
            }
            else if (uri.Host.Contains("amazon"))
            {
                return ProductSourceType.Amazon;
            }
            else if (uri.Host.Contains("ebay"))
            {
                return ProductSourceType.EBay;
            }
            else
            {
                return ProductSourceType.Unknow;
            }
        }
    }

    public enum ProductSourceType
    {
        Unknow = 0,
        Microsoft = 1,
        Amazon = 2,
        EBay = 3,
    }

    public enum ProductExecState
    {
        NotStart = 0,
        Running = 1,
        RunFailed = 2,
        Completed = 3,
    }

    public enum UserState
    {
        Normal = 0,
        Disable = 1,
    }

    public enum UserSex
    {
        Women = 0,
        Man = 1,
        Unknow = 2,
    }
}
