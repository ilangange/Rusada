using Microsoft.Extensions.DependencyInjection;
using Rusada.Aviation.Core.Interface;
using Rusada.Aviation.Core.Interfaces;
using Rusada.Aviation.Core.Service;
using Rusada.Aviation.Core.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Rusada.Aviation.API.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services) 
        {
            services.AddScoped<JwtSecurityTokenHandler, JwtSecurityTokenHandler>();
            services.AddScoped<ISightingService, SightingService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
