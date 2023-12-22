using System.Security.Claims;

namespace ServiceBooking.Business.Core.Model
{
    public class SdtPrincipal : ClaimsPrincipal
    {
        public SdtPrincipal(SdtUserIdentity userIdentity) : base(userIdentity)
        {

        }
    }
}
