using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Bookings
{
    public record GetBookingsByClientQuery(int clientId) : IRequest<BookingsVm>
    {
    }

    public class GetBookingsByClientQueryHandler : IRequestHandler<GetBookingsByClientQuery, BookingsVm>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsByClientQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<BookingsVm> Handle(GetBookingsByClientQuery request, CancellationToken cancellationToken)
        {
            return await _bookingService.GetBookingsByClient(request.clientId, cancellationToken);
        }
    }

}
