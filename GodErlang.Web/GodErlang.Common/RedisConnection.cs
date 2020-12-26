using StackExchange.Redis;
using System;

namespace GodErlang.Common
{
    public class RedisConnection
    {
        public ConnectionMultiplexer CurrentClient { get; private set; }

        public RedisConnection(string connectionString)
        {
            this.CurrentClient = ConnectionMultiplexer.Connect(connectionString);
        }

        public bool IsConnected
        {
            get { return this.CurrentClient != null && this.CurrentClient.IsConnected; }
        }

        public T Exec<T>(Func<IDatabase, T> func)
        {
            return Exec<T>(0, func);
        }

        public T Exec<T>(int dbNum, Func<IDatabase, T> func)
        {
            if (IsConnected)
            {
                var db = this.CurrentClient.GetDatabase(dbNum);
                return func(db);
            }

            return default(T);
        }

        public void Exec(Action<IDatabase> act)
        {
            Exec(0, act);
        }

        public void Exec(int dbNum, Action<IDatabase> act)
        {
            if (IsConnected)
            {
                var db = this.CurrentClient.GetDatabase(dbNum);
                act(db);
            }
        }

        public long Publish(string channel, string message)
        {
            ISubscriber subscriber = this.CurrentClient.GetSubscriber();
            return subscriber.Publish(channel, message);
        }

        public void SubscribeAsync(string channel, Action<string> callback)
        {
            ISubscriber subscriber = this.CurrentClient.GetSubscriber();
            subscriber.SubscribeAsync(channel, (ch, msg) =>
            {
                callback(msg);
            });
        }
    }
}
