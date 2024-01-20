using Microsoft.AspNetCore.Http;

namespace ServcieBooking.Business.Interface
{
    public interface ILoggerRule<in TRequest>
    {
        Task Authorize(TRequest request,CancellationToken cancellationToken,IHttpContextAccessor contex);
    }
}
