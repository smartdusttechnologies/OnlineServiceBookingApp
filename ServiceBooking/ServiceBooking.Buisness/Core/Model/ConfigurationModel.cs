using ServiceBooking.Business.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBooking.Buisness.Core.Model
{
    public class ConfigurationModel : Entity
    {
        public string Currency { get; set; }
        public string TypeName { get; set; }
        public string CurrencySymbol { get; set; }
        public string DeliveryRate { get; set; }
    }
}
