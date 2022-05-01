using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace SpaceAdventures.API.Configurations
{
    public static class ApiVersioningConfig     
    {
        public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
        {
            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;             // In case if the user doesn't specify the version, so we assume using the default one (v1)
                cfg.ReportApiVersions = true;                               // This will mention which API the user is currently using (Header).

                // 1-  api/v1/clients/ => In order to read the segment that contains the version eg.
                cfg.ApiVersionReader = new UrlSegmentApiVersionReader();

                // 2- api-version : 1.0 => In case the user provides the version as header  
                // cfg.ApiVersionReader = new HeaderApiVersionReader();    

                // 3- ?api-version=1.0 => From query approach
                // cfg.ApiVersionReader = new QueryStringApiVersionReader();
            });

            return services;
        }
    }
}
