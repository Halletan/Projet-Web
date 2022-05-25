namespace SpaceAdventures.API.Configurations;

public static class VersionApiExplorerConfig
{
    public static IServiceCollection AddVersionedApiExplorerConfig(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(setup =>
        {
            // Instruct Swagger/ApiVersionExplorer how it will handle this concept of different versions that we have.

            setup.GroupNameFormat = "'v'VVV"; // How to read this version 
            setup.SubstituteApiVersionInUrl =
                true; // When we actually run our api we want that the version should be substituted auto. in our URLs
        });

        return services;
    }
}