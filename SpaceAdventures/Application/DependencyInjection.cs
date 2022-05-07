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

            // Behaviors

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            return services;

        }
    }
}
