using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServcieBooking.Business.Interface;

namespace ServcieBooking.Business.PipelineBehaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILoggerRule<TRequest> _loggerHandler;
        private readonly IHttpContextAccessor _contextAccessor;
        public LoggingBehavior(ILoggerRule<TRequest> loggerRule, IHttpContextAccessor context)
        {
            _loggerHandler = loggerRule;
            _contextAccessor = context;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            await _loggerHandler.Authorize(request, cancellationToken, _contextAccessor);
            // Continue to the next handler in the pipeline
            return await next();
        }
    }
}
