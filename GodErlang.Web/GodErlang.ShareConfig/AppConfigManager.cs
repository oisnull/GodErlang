using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GodErlang.ShareConfig
{
    public class AppConfigManager
    {
        private static IConfiguration _configuration;

        protected static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    InitConfiguration();
                }

                return _configuration;
            }
        }

        public static void InitConfiguration()
        {
            if (string.IsNullOrEmpty(CurrentEnvironment) || string.IsNullOrWhiteSpace(CurrentEnvironment))
            {
                throw new ArgumentNullException("Environment");
            }

            string settingFilePath = Path.Combine(AppContext.BaseDirectory, $"sharesettings.{CurrentEnvironment}.json");

            _configuration = new ConfigurationBuilder()
                .AddJsonFile(settingFilePath, false)
                .Build();
        }

        public static string CurrentEnvironment { get { return Environment.GetEnvironmentVariable("Environment")?.ToLower(); } }

        protected static IConfigurationSection RedisSettings { get; } = Configuration.GetSection("RedisSettings");

        protected static IConfigurationSection AppSettings { get; } = Configuration.GetSection("AppSettings");

        public static string DBConnectionString { get; } = Configuration.GetConnectionString("GDDBConnectionString");

        protected static IConfigurationSection LogSettings { get; } = Configuration.GetSection("LogSettings");

        public static string ProductUrlsRedisHosts
        {
            get
            {
                return RedisSettings["ProductUrlsRedisHosts"];
            }
        }

        public static string GEUserSessionRedisHosts
        {
            get
            {
                return RedisSettings["GEUserSessionRedisHosts"];
            }
        }

        public static string GEUserSessionRedisInstance
        {
            get
            {
                return RedisSettings["GEUserSessionRedisInstance"];
            }
        }

        public static int GEUserSessionTimeOut
        {
            get
            {
                return Convert.ToInt32(RedisSettings["GEUserSessionTimeOut"]);
            }
        }
    }
}
