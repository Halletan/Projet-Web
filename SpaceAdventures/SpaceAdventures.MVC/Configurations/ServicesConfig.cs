﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using SpaceAdventures.MVC.Services;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Configurations
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient<IGlobalService, GlobalService>();
            services.AddHttpClient<IClientService, ClientService>();
            services.AddHttpClient<IAircraftService, AircraftService>();
            services.AddHttpClient<IBookingService, BookingService>();
            services.AddHttpClient<IFlightService, FlightService>();
            return services;

        }
    }
}
