using ServiceBooking.Business.Core.Model;

namespace ServiceBooking.Buisness.Core.Model
{
    public class Organization : Entity
    {
        public string OrgCode { get; set; }
        public string OrgName { get; set; }
    }
}
