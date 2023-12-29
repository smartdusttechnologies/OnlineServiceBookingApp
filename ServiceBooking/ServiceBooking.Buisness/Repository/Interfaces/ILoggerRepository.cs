using ServiceBooking.Buisness.Core.Model.Security;

namespace ServiceBooking.Buisness.Repository.Interfaces
{
    public interface ILoggerRepository
    {
        int LoginLog(LoginRequest loginRequest);
        int LoginTokenLog(LoginToken loginToken);
    }
}