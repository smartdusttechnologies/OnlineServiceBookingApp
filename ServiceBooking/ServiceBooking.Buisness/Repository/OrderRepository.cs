using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Repository.Interfaces;
using ServiceBooking.Business.Common;
using ServiceBooking.Buisness.Core.Model.Orders;
using ServiceBooking.Buisness.Core.Model.Profile;

namespace ServiceBooking.Buisness.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public OrderRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public RequestResult<bool> NewOrder()
        {
            return new RequestResult<bool>(true);
        }
        public OrdersModel OrdersList()
        {
            try
            {
                // Specify the path to the JSON file in wwwroot
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "orders.json");

                // Read the JSON file
                var jsonContent = File.ReadAllText(filePath);

                // Deserialize JSON to C# object
                var myObject = JsonConvert.DeserializeObject<OrdersModel>(jsonContent);

                // Use 'myObject' as needed
                return myObject;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new OrdersModel();
            }
        }
        public RequestResult<bool> ApplyCoupon(string coupon)
        {
            return new RequestResult<bool>(true);
        }
    }
}
