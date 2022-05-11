using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using SpaceAdventures.API.Configurations;
using SpaceAdventures.API.Handlers;
using SpaceAdventures.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;


// Serilog Service

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(configuration)
    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
        .WithDefaultDestructurers()
        .WithDestructurers(new []{new DbUpdateExceptionDestructurer()})));

// Jwt Bearer Authentication
builder.Services.AddAuthenticationJwtBearer(configuration);


// Injection DB Service
builder.Services.AddInfrastructure(configuration);

// Injection Application Services
builder.Services.AddApplication();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


builder.Services.AddMvc(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
    options.ReturnHttpNotAcceptable = true;
}).AddFluentValidation();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Our Custom Exception Middleware
app.UseExceptionMiddleware();

//  Our Log Request Middleware
// app.UseLogRequestMiddleware();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(opt =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
            opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.ApiVersion.ToString());
        }
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();




