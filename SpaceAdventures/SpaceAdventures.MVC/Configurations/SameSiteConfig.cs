namespace SpaceAdventures.MVC.Configurations
{
    public static class SameSiteConfig
    {
        // The ConfigureSameSiteNoneCookies method used above was added as part of the sample application in order to (make cookies with SameSite=None work over HTTP when using Chrome).
        // We recommend using HTTPS instead of HTTP, which removes the need for the ConfigureSameSiteNoneCookies method.
        
        public static IServiceCollection AddSameSiteNoneCookiesServiceCollection(this IServiceCollection services)  
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext => CheckSameSite(cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext => CheckSameSite(cookieContext.CookieOptions);
            });

            return services;
        }

        public static void CheckSameSite(CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None && options.Secure == false)
            {
                options.SameSite = SameSiteMode.Unspecified;
            }
        }
    }
}
