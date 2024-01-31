using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBooking.Buisness.Core.Model.Profile
{
    public class ProfileModel
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public bool phoneIsVerified { get; set; }
        public string email { get; set; }
        public bool emailIsVerified { get; set; }
        public string notificationToken { get; set; }
        public bool isOrderNotification { get; set; }
        public bool isOfferNotification { get; set; }
        public List<Address> addresses { get; set; }
        public bool favourite { get; set; }
    }

    public class Address
    {
        public string _id { get; set; }
        public string label { get; set; }
        public string deliveryAddress { get; set; }
        public string details { get; set; }
        public Location location { get; set; }
        public bool selected { get; set; }
    }

    public class Location
    {
        public List<double> coordinates { get; set; }
    }
}
