using ServiceBooking.Business.Models;
using ServiceBooking.Business.Models.Resturant;

namespace ServiceBooking.Business.Repository.Interface
{
    public interface IResturantRepository
    {
        ResturantModel Get();
        ResturantDetailModel Get(string resturantId);
    }
}