using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpaceAdventures.Application.Common.Interfaces;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SpaceAdventuresDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(SpaceAdventuresDbContext).Assembly.FullName)));

        services.AddScoped<ISpaceAdventureDbContext>(p => p.GetService<SpaceAdventuresDbContext>());

        return services;
    }
}