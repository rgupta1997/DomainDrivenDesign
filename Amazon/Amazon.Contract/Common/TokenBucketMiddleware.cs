using Amazon.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Amazon.Contract.Common
{
    public class TokenBucketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenBucket _tokenBucket;


        public TokenBucketMiddleware(RequestDelegate next, IDatabase database, IOptions<TokenBucketSettings> settings)
        {
            _next = next;
            var config = settings.Value;
            _tokenBucket = new TokenBucket(database, config.KeyPrefix, config.MaxTokens, config.RefillRate); 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientId = context.Request.Headers["Client-ID"].ToString();
            var apiName = context.Request.Path.Value;

            if (string.IsNullOrEmpty(clientId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Client-ID header is missing");
                return;
            }

            if (_tokenBucket.Consume(clientId, apiName, 1))
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
