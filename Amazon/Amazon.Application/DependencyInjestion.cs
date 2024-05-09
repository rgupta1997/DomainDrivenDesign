using Amazon.Application.Authentication.Commands.Register;
using Amazon.Application.Authentication.Common;
using Amazon.Application.Authentication.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application
{
    public static class DependencyInjestion
    {
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddApplication(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.AddScoped<IPipelineBehavior<RegisterCommand, AuthenticationResult>, ValidationRegisterCommandBehavior>();

            services.AddMediatR(typeof(DependencyInjestion).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
