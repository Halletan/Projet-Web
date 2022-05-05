
using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SpaceAdventures.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {

        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Pre Logic
            _logger.LogInformation("{Request} is starting.", request.GetType().Name);
            var timer = Stopwatch.StartNew();

            var response = await next();

            timer.Stop();

            // Post logic
            _logger.LogInformation("{Request} has finished in {Time}.", request.GetType().Name, timer.ElapsedMilliseconds);

            return response;
        }
    }
}
