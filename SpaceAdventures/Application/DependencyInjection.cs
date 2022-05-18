using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services;
using Application.Common.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SpaceAdventures.Application.Common.Behaviours;
using SpaceAdventures.Application.Common.Services;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace Application
{
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

            services.AddHttpClient<IUsersManagementApiService, UsersManagementApiService>();
            services.AddHttpClient<IISSCLService, ISSCLService>();

            // Behaviors

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            return services;

        }
    }
}
