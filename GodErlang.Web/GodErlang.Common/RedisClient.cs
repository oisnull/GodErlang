using System;

namespace GodErlang.Common
{
    public class RedisClient
    {
        private static readonly object _locker = new object();
        private static RedisConnection _instance1 = null;

        public static RedisConnection ProdcutUrlsInstance
        {
            get
            {
                if (_instance1 == null)
                {
                    lock (_locker)
                    {
                        if (_instance1 == null || !_instance1.IsConnected)
                        {
                            _instance1 = new RedisConnection(ShareConfig.AppConfigManager.ProductUrlsRedisHosts);
                        }
                    }
                }
                return _instance1;
            }
        }
    }
}
