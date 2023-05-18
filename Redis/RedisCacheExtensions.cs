using Microsoft.Extensions.DependencyInjection;
using System;

namespace Deadline.redis
{
    public static class RedisCacheExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, Action<RedisCacheConfiguration> options)
        {
            var redisCacheConfiguration = new RedisCacheConfiguration();

            options.Invoke(redisCacheConfiguration);

            services.AddSingleton(redisCacheConfiguration);

            services.AddSingleton<IRedisDataBaseResolver, RedisDataBaseResolver>();

            services.AddSingleton<IRedisCache, RedisCache>();

            return services;
        }

    }
}