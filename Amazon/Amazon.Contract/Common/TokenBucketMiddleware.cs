using Amazon.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Net;

namespace Amazon.Contract.Common
{
    public class TokenBucketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenBucket _tokenBucket;
        private readonly TokenBucket _tokenBucket_ip;
        private readonly TokenBucket _tokenBucket_global;


        public TokenBucketMiddleware(RequestDelegate next, IDatabase database, IOptions<TokenBucketSettings> settings)
        {
            _next = next;
            var config = settings.Value;
            _tokenBucket = new TokenBucket(database, config.KeyPrefix, config.MaxTokens, config.RefillRate); 
            _tokenBucket_ip = new TokenBucket(database, config.KeyPrefixIp, config.MaxTokensIp, config.RefillRate); 
            _tokenBucket_global = new TokenBucket(database, config.KeyPrefixGlobal, config.MaxTokensGlobal, config.RefillRate); 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var param = context.Request.Headers["Client-ID"].ToString();
            var apiName = context.Request.Path.Value;
            var ipAddress = context.Connection.RemoteIpAddress?.ToString(); ;

            if (string.IsNullOrEmpty(param))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Client-ID header is missing");
                return;
            }

            if (_tokenBucket.Consume(param, apiName, 1))
            {
                await _next(context);
            }
            else if (_tokenBucket_ip.Consume(ipAddress, apiName, 1))
            {
                await _next(context);
            }
            else if (_tokenBucket_global.Consume(null, null, 1))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded");
            }
        }
    }
}
