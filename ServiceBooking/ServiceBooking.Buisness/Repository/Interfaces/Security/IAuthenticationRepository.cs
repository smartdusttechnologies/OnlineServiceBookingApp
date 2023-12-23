using ServiceBooking.Buisness.Core.Model.Security;

namespace ServiceBooking.Business.Data.Repository.Interfaces.Security
{
    public interface IAuthenticationRepository
    {
        PasswordLogin GetLoginPassword(string userName);
        int SaveLoginToken(LoginToken loginToken);
    }
}
