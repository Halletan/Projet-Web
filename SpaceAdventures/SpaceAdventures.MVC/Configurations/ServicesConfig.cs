using Microsoft.Extensions.DependencyInjection.Extensions;
using SpaceAdventures.MVC.Services;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Configurations
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient<IClientService, ClientService>();
            services.AddHttpClient<IGlobalService, GlobalService>();
            return services;

        }
    }
}
