using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Amazon.Contract.Common
{ 
    public class TokenBucket
    {
        private readonly IDatabase _database;
        private readonly string _baseKey;
        private readonly int _maxTokens;
        private readonly double _refillRate; // tokens per second

        public TokenBucket(IDatabase database, string baseKey, int maxTokens, double refillRate)
        {
            _database = database;
            _baseKey = baseKey;
            _maxTokens = maxTokens;
            _refillRate = refillRate;
        }

        private string GetKey(string clientId,  string apiName)
        {
            return $"{_baseKey}:{clientId}:{apiName}";
        }

        private double Refill(string key)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var bucket = _database.HashGetAll(key);
            double tokens;
            long lastRefill;

            if (bucket.Length == 0)
            {
                tokens = _maxTokens;
                lastRefill = now;
                _database.HashSet(key, new HashEntry[] {
                new HashEntry("tokens", tokens),
                new HashEntry("lastRefill", lastRefill)
            });
            }
            else
            {
                tokens = (double)bucket.FirstOrDefault(entry => entry.Name == "tokens").Value;
                lastRefill = (long)bucket.FirstOrDefault(entry => entry.Name == "lastRefill").Value;

                var elapsedTime = now - lastRefill;
                var refillTokens = elapsedTime * _refillRate;
                tokens = Math.Min(_maxTokens, tokens + refillTokens);
                _database.HashSet(key, new HashEntry[] {
                new HashEntry("tokens", tokens),
                new HashEntry("lastRefill", now)
            });
            }

            return tokens;
        }

        public bool Consume(string clientId, string apiName, int tokens)
        {
            var key = GetKey(clientId, apiName);
            var currentTokens = Refill(key);
            if (currentTokens > tokens)
            {
                _database.HashDecrement(key, "tokens", tokens);
                return true;
            }
            return false;
        }
    }


}
