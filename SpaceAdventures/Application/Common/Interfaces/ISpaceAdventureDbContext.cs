using SpaceAdventures.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpaceAdventures.Application.Common.Interfaces;

public interface ISpaceAdventureDbContext
{
    DbSet<Aircraft> Aircraft { get; set; }
    
    DbSet<Airport> Airports { get; set; }
    DbSet<Booking> Bookings { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<Flight> Flights { get; set; }
    DbSet<Itinerary> Itineraries { get; set; }
    
    DbSet<Planet> Planets { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellation);
}
