using System.Security.Claims;

namespace ServiceBooking.Buisness.Core.Model.Security
{
    public class SdtPrincipal : ClaimsPrincipal
    {
        public SdtPrincipal(SdtUserIdentity userIdentity) : base(userIdentity)
        {

        }
    }
}
