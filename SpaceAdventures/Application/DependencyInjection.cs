using System.Reflection;
using Application.Common.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SpaceAdventures.Application.Common.Behaviours;
using SpaceAdventures.Application.Common.RetryPolicies;
using SpaceAdventures.Application.Common.Services;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IPlanetService, PlanetService>();
        services.AddScoped<IAirportService, AirportService>();
        services.AddScoped<IItineraryService, ItineraryService>();
        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IAircraftService, AircraftService>();
        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAircraftSeatService, AircraftSeatService>();
        services.AddScoped<IUsersManagementApiService, UsersManagementApiService>();


        services.AddHttpClient<IISSCLService, ISSCLService>();
        services.AddHttpClient<INasaApiService, NasaApiService>();


        // Policy Service
        services.AddSingleton(new ClientPolicy());
        services.AddHttpClient("RetryPolicy").AddPolicyHandler(request => new ClientPolicy().ExponentialHttpRetry);

        // Behaviors
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

        return services;
    }
}