﻿namespace ServiceBooking.Buisness.Core.Model.Security
{
    /// <summary>
    /// Claims Types
    /// </summary>

    //public class CustomClaimTypes
    //{
    //    public const string Permission = "Application.Permission";
    //    public const string UserId = "UserId";
    //    public const string OrganizationId = "OrganizationId";
    //}

    public enum CustomClaimType
    {
        DefaultClaim,
        ApplicationPermission,
        UserId,
        OrganizationId
    }
}
