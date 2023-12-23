using ServiceBooking.Business.Core.Model;

namespace ServiceBooking.Web.Models
{
    public class OrganizationDTO : Entity
    {
        public string OrgCode { get; set; }
        public string OrgName { get; set; }
    }
}
