using System;
using System.IO;

namespace GodErlang.ShareConfig
{
    public class LogConfigManager
    {
        public static string LogDirectory
        {
            get
            {
                string dir = "logs";
                if (string.IsNullOrEmpty(dir))
                {
                    return Path.GetPathRoot(AppContext.BaseDirectory);
                }

                if (Path.IsPathRooted(dir))
                {
                    return dir;
                }

                return Path.Combine(Path.GetPathRoot(AppContext.BaseDirectory), dir);
            }
        }
        public static bool EnableError { get; } = true;
        public static bool EnableDebug { get; } = true;
        public static bool EnableInfo { get; } = true;
        public static bool EnableWarn { get; } = true;
        public static bool EnableFatal { get; } = true;
    }
}
