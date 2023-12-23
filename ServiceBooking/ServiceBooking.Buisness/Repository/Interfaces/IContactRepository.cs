using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Business.Common;

namespace ServiceBooking.Business.Data.Repository.Interfaces
{
    public interface IContactRepository
    {
        RequestResult<bool> Save(ContactModel contact);
    }
}
