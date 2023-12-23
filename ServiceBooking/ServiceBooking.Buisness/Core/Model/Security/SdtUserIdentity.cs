using System.Collections.Generic;
using System.Security.Claims;

namespace ServiceBooking.Buisness.Core.Model.Security
{
    public class SdtUserIdentity : ClaimsIdentity
    {
        public int OrganizationId { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

    }
}
