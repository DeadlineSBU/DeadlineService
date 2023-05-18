using StackExchange.Redis;

namespace Deadline.redis
{
    public class RedisDataBaseResolver : IRedisDataBaseResolver
    {
        public IDatabase GetDatabase(string connection, int dbNumber = 0)
        {
            return ConnectionMultiplexer.Connect(connection).GetDatabase(dbNumber);
        }
    }
}