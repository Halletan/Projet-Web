using Microsoft.AspNetCore.Authentication.JwtBearer;
using SpaceAdventures.API.Handlers;

namespace SpaceAdventures.API.Configurations;

public static class AuthenticationJwtBearerConfig
{
    public static IServiceCollection AddAuthenticationJwtBearer(this IServiceCollection services,
        IConfiguration configuration)
    {
        var domain = configuration["Auth0:Domain"];
        var audience = configuration["Auth0:Audience"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = domain;
            options.Audience = audience;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("read:messages",
                policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
            options.AddPolicy("write:messages",
                policy => policy.Requirements.Add(new HasScopeRequirement("write:messages", domain)));
            options.AddPolicy("read:users",
                policy => policy.Requirements.Add(new HasScopeRequirement("read:users", domain)));
            options.AddPolicy("write:users",
                policy => policy.Requirements.Add(new HasScopeRequirement("write:users", domain)));
        });

        return services;
    }
}