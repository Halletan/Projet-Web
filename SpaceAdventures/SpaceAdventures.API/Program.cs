using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using SpaceAdventures.API.Configurations;
using SpaceAdventures.API.Filters;
using SpaceAdventures.API.Handlers;
using SpaceAdventures.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Serilog Service

builder.Host.UseSerilog((context,logger) => logger
    .ReadFrom.Configuration(configuration)
    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
        .WithDefaultDestructurers()
        .WithDestructurers(new[] {new DbUpdateExceptionDestructurer()})));


// Jwt Bearer Authentication
builder.Services.AddAuthenticationJwtBearer(configuration);

// Injection DB Service
builder.Services.AddInfrastructure(configuration);

// Injection Application Services
builder.Services.AddApplication();

// Exception Middleware on top of all controllers
builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

// Swagger EndpointsApiExplorer Service
builder.Services.AddEndpointsApiExplorer();

// SwaggerGen Configuration Service
builder.Services.AddSwaggerGenServiceCollection();

// Swagger Description Configuration
builder.Services.ConfigureOptions<SwaggerConfig>();

// Api Versioning Configuration
builder.Services.AddApiVersioningConfig();

// Api Versioned Explorer Configuration  -- Swagger Config
builder.Services.AddVersionedApiExplorerConfig();

// Scope Authorization Handler
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


var app = builder.Build();


/*********  Middleware  **********/


app.UseSerilogRequestLogging();


//Our Log Request Middleware
app.UseLogRequestMiddleware();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(opt =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
            opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.ApiVersion.ToString());
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();