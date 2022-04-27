using Application;
using Infrastructure;
using SpaceAdventures.Application.Common.Policies;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection DB 
builder.Services.AddInfrastructure(configuration);

// Injection Application
builder.Services.AddApplication();

builder.Services.AddHttpClient("RetryPolicy").AddPolicyHandler(request => new ClientPolicy().ExponentialHttpRetry);

var app = builder.Build();

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
