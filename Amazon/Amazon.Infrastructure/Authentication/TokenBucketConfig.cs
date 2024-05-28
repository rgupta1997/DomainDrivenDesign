using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.Authentication
{
    public class TokenBucketSettings
    {
        public string RedisConnectionString { get; set; }
        public string KeyPrefix { get; set; }
        public string KeyPrefixIp { get; set; }
        public string KeyPrefixGlobal { get; set; }
        public int MaxTokens { get; set; }
        public int MaxTokensIp { get; set; }
        public int MaxTokensGlobal { get; set; }
        public double RefillRate { get; set; }
    }
}
