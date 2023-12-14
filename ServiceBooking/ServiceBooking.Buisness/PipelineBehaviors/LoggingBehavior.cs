﻿using MediatR;
using Microsoft.Extensions.Logging;
namespace ServcieBooking.Buisness.PipelineBehaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Handling {typeof(TRequest).Name}");

            var response = await next();

            _logger.LogInformation($"Handled {typeof(TRequest).Name}");

            return response;
        }
    }
}
