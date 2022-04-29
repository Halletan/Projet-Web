using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

// Injection DB Service
builder.Services.AddInfrastructure(configuration);

// Injection Application Services
builder.Services.AddApplication();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
    options.ReturnHttpNotAcceptable = true;
}).AddFluentValidation();

var app = builder.Build();

//  Our Log Request Middleware
app.UseLogRequestMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
