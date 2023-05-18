using StackExchange.Redis;

namespace Deadline.redis
{
    public interface IRedisDataBaseResolver
    {
        IDatabase GetDatabase(string connection, int dbNumber = 0);
    }
}