using System.Collections.Generic;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Core.Model.Profile;
using ServiceBooking.Buisness.Core.Model.Security;

namespace ServiceBooking.Business.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        List<string> Get();
        ProfileModel Get(int id);
        UserModel Get(string userName);
        int Insert(UserModel user, PasswordLogin passwordLogin);
        int Update(ChangePasswordModel newpassword);
        UserModel EmailExist(string email);
    }
}