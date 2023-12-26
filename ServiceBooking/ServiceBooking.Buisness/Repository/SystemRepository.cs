using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Repository.Interfaces;

namespace ServiceBooking.Buisness.Repository
{
    public class SystemRepository : ISystemRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public SystemRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public ConfigurationModel Get()
        {
            try
            {
                // Specify the path to the JSON file in wwwroot
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "configuration.json");

                // Read the JSON file
                var jsonContent = File.ReadAllText(filePath);
                // return jsonContent;
                // Deserialize JSON to C# object
                return JsonConvert.DeserializeObject<ConfigurationModel>(jsonContent);

                // Use 'myObject' as needed
                //return myObject;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new ConfigurationModel();
            }
        }
    }
}
