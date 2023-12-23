using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using ServiceBooking.Buisness.Repository.Interfaces;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Core.Model.Resturant;

namespace ServiceBooking.Buisness.Repository
{
    public class ResturantRepository : IResturantRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ResturantRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public ResturantModel Get()
        {
            try
            {
                // Specify the path to the JSON file in wwwroot
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "resturant.json");

                // Read the JSON file
                var jsonContent = File.ReadAllText(filePath);
                // return jsonContent;
                // Deserialize JSON to C# object
                return JsonConvert.DeserializeObject<ResturantModel>(jsonContent);

                // Use 'myObject' as needed
                //return myObject;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new ResturantModel();
            }
        }
        public ResturantDetailModel Get(string resturantId)
        {
            try
            {
                // Specify the path to the JSON file in wwwroot
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "resturantDetails.json");

                // Read the JSON file
                var jsonContent = File.ReadAllText(filePath);

                // Deserialize JSON to C# object
                var myObject = JsonConvert.DeserializeObject<ResturantDetailModel>(jsonContent);

                // Use 'myObject' as needed
                return myObject;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new ResturantDetailModel();
            }
        }
    }
}
