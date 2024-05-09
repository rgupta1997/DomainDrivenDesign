using Mapster;
using MapsterMapper;
using MediatR;
using System.Reflection;

namespace Amazon.Mapping
{

    public static class DependencyInjestion
    {
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddMapping(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddSingleton<IMapper, Mapper>();
            return services;
        }
    }
}
