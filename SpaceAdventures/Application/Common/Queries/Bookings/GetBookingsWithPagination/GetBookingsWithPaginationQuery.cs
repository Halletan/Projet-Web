using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Bookings.GetBookingsWithPagination
{
    public class GetBookingsWithPaginationQuery : IRequest<PaginatedList<BookingDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class
        GetBookingsWithPaginationQueryHandler : IRequestHandler<GetBookingsWithPaginationQuery,
            PaginatedList<BookingDto>>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsWithPaginationQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<PaginatedList<BookingDto>> Handle(GetBookingsWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            return await _bookingService.GetBookingsWithPagination(request.PageNumber, request.PageSize,
                cancellationToken);
        }
    }
}
