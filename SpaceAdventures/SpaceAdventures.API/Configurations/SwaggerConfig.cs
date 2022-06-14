using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SpaceAdventures.API.Configurations;

public class SwaggerConfig : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerConfig(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Version = description.ApiVersion.ToString(),
            Title = "Space Adventures API",
            Description = "An API managing travel and reservations to planets",
            TermsOfService = new Uri("https://github.com/hchadli/Projet-Web/blob/main/README.md"),

            Contact = new OpenApiContact
            {
                Name = "Antoine HALLET | Corentin DECONNINCK | Hammadi CHADLI",
                Url = new Uri("https://github.com/hchadli/Projet-Web")
            },
            License = new OpenApiLicense
            {
                Name = "PROJET WEB | EPHEC 2021-2022",
                Url = new Uri(
                    "https://www.ephec.be/sites/default/files/cours/grille-informatique-de-gestion-2018.pdf")
            }
        };

        return info;
    }
}