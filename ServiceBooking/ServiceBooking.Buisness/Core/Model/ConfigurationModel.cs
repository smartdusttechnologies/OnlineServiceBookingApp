using ServiceBooking.Business.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBooking.Buisness.Core.Model
{
    public class ConfigurationModel 
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public string currency { get; set; }
        public string currencySymbol { get; set; }
        public int deliveryRate { get; set; }
    }
}
