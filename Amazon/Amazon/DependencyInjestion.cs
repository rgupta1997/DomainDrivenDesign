using Amazon.Application.Authentication.Commands.Register;
using Amazon.Application.Authentication.Common;
using Amazon.Mapping;
using Amazon.Validator;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon
{
    public static class DependencyInjestion
    {
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddPresentation(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.AddScoped<AbstractValidator<ClaimSearch>, ConditionalValidation>(); 
            services.AddScoped<AbstractValidator<MedicalDetails>, MedicalValidation>(); 
            services.AddControllers();
            services.AddMapping();
            return services;
        }
    }
}
