using ServiceBooking.Buisness.Core.Model.Orders;
using ServiceBooking.Business.Common;

namespace ServiceBooking.Buisness.Repository.Interfaces
{
    public interface IOrderRepository
    {
        RequestResult<bool> NewOrder();
        OrdersModel OrdersList();
        RequestResult<bool> ApplyCoupon(string coupon);
    }
}