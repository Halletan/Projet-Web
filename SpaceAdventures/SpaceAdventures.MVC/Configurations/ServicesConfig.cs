using SpaceAdventures.MVC.Services;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Configurations;

public static class ServicesConfig
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddHttpClient<IGlobalService, GlobalService>();
        services.AddHttpClient<IClientService, ClientService>();
        services.AddHttpClient<IAircraftService, AircraftService>();
        services.AddHttpClient<IAirportService, AirportService>();
        services.AddHttpClient<IBookingService, BookingService>();
        services.AddHttpClient<IFlightService, FlightService>();
        services.AddHttpClient<IItineraryService, ItineraryService>();
        services.AddHttpClient<IPlanetService, PlanetService>();
        services.AddHttpClient<INASAService, NASAService>();

        
        services.AddHttpClient<IUserManagementMvcService, UserManagementMvcService>();

        return services;
    }
}