﻿namespace ServiceBooking.Business.Core.Model
{
    public class GroupClaim : Entity
    {
        public CustomClaimType ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}

