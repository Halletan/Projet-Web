using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Bookings;
using SpaceAdventures.Application.Common.Queries.Bookings;
namespace SpaceAdventures.API.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookingsController : ControllerBase
    {


        private readonly IMediator _mediator;
        private readonly ILogger<BookingsController> _logger;

        /// <summary>
        /// Bookings Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public BookingsController(IMediator mediator, ILogger<BookingsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all Bookings
        /// </summary>
        //[HttpGet]
        [HttpGet]
        [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BookingsVm> GetBookings()
        {

            return await _mediator.Send(new GetBookingsQuery());
        }

        /// <summary>
        /// Get a sepicif Booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BookingDto> GetBookingById(int id)
        {
            return await _mediator.Send(new GetBookingByIdQuery(id));
        }

        /// <summary>
        /// Create a new Booking
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<BookingDto> CreateBooking([FromBody] CreateBookingCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Update an existing Booking
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BookingDto> UpdateBooking([FromBody] UpdateBookingCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Delete an existing Booking
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task DeleteBooking([FromBody] DeleteBookingCommand command)
        {
            await _mediator.Send(command);
        }



    }
}
