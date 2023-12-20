using ServiceBooking.Buisness.Models;

namespace ServiceBooking.Buisness.Repository.Interface
{
    public interface IResturantRepository
    {
        ResturantModel Get();
        object Get(string resturantId);
    }
}