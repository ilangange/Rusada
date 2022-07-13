using Microsoft.Extensions.DependencyInjection;
using Rusada.Aviation.Core.Entities;
using Rusada.Aviation.Core.Interface;
using Rusada.Aviation.Infrastructure.Repository;

namespace Rusada.Aviation.Infrastructure.Extensions
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection RegisterInfraDependencies(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<Sighting>, GenericRepository<Sighting>>();

            return services;
        }
    }
}
