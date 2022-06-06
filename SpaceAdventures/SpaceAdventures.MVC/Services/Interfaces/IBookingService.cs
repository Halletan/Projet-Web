using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces;

public interface IBookingService
{
    Task<Bookings> GetAllBookings(string? accessToken);
    Task<Bookings> GetBookingsByClient(int clientId, string? accessToken);
    Task<Booking> CreateBooking(Booking booking, string? accessToken);
}