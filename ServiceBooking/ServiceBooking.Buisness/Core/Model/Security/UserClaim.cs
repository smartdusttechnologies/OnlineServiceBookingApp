using ServiceBooking.Business.Core.Model;

namespace ServiceBooking.Buisness.Core.Model.Security
{
    public class UserClaim : Entity
    {
        public CustomClaimType ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
