using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Core.Model.Resturant;

namespace ServiceBooking.Buisness.Repository.Interfaces
{
    public interface IResturantRepository
    {
        ResturantModel Get();
        ResturantDetailModel Get(string resturantId);
    }
}