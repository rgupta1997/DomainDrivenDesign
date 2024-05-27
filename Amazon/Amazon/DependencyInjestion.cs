using Amazon.Application.Authentication.Commands.Register;
using Amazon.Application.Authentication.Common;
using Amazon.Infrastructure.Authentication;
using Amazon.Mapping;
using Amazon.Validator;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon
{
    public static class DependencyInjestion
    {
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddPresentation(this Microsoft.Extensions.DependencyInjection.IServiceCollection services,ConfigurationManager configuration)
        {
            services.Configure<TokenBucketSettings>(configuration.GetSection("TokenBucketSettings"));
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetSection("TokenBucketSettings:RedisConnectionString").Value));
            services.AddSingleton<IDatabase>(provider => provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase());


            services.AddScoped<AbstractValidator<ClaimSearch>, ConditionalValidation>(); 
            services.AddScoped<AbstractValidator<MedicalDetails>, MedicalValidation>(); 
            services.AddControllers();
            services.AddMapping();
            return services;
        }
    }
}
