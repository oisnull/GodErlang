using GodErlang.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GodErlang.Service.Storage
{
    public class ProductManager
    {
        public const string URLS_QUEUE_NAME = "product:urls:queue";
        public const string URLS_TRIGGER_NAME = "product:urls:trigger";

        public static string SetUrlsQueueValue(int userId, int statusId, string productUrl)
        {
            return $"{userId},{statusId},{productUrl}";
        }

        /// <summary>
        /// The format: userId,statusId,productUrl
        /// </summary>
        /// <param name="queueValue"></param>
        /// <returns></returns>
        public static Tuple<int, int, string> GetUrlsQueueValue(string queueValue)
        {
            string[] values = queueValue?.Split(',');
            return new Tuple<int, int, string>(values?.Length >= 1 ? CommonTools.GetInt(values[0]) : 0,
                                                values?.Length >= 2 ? CommonTools.GetInt(values[1]) : 0,
                                                values?.Length >= 3 ? values[2] : null);
        }

        public static void AddUrlAndTrigger(int userId, int statusId, string productUrl)
        {
            if (userId <= 0 || statusId <= 0 || string.IsNullOrEmpty(productUrl)) return;

            string queueValue = SetUrlsQueueValue(userId, statusId, productUrl);
            RedisClient.ProdcutUrlsInstance.Exec(db => db.ListRightPush(URLS_QUEUE_NAME, queueValue));
            RedisClient.ProdcutUrlsInstance.Publish(URLS_TRIGGER_NAME, userId.ToString());
        }

        public static void RemoveUrl(int userId, int statusId, string productUrl)
        {
            if (userId <= 0 || statusId <= 0 || string.IsNullOrEmpty(productUrl)) return;

            string queueValue = SetUrlsQueueValue(userId, statusId, productUrl);
            RedisClient.ProdcutUrlsInstance.Exec(db => db.ListRemove(URLS_QUEUE_NAME, queueValue));
        }
    }
}
