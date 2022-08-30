using StackExchange.Redis;

namespace POC.DataAccess.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public RedisService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public async Task AddAsync(string key, string value)
        {
            var db = _redisConnection.GetDatabase();
            await db.StringSetAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            var db = _redisConnection.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task PublishAsync(string channel, string value)
        {
            var db = _redisConnection.GetDatabase();
            await db.PublishAsync(channel, value);
        }

        public void Suscribe(string channel, Action<RedisChannel, RedisValue> handler)
        {
            ISubscriber sub = _redisConnection.GetSubscriber();
            sub.Subscribe(channel, handler);
        }

    }
}
