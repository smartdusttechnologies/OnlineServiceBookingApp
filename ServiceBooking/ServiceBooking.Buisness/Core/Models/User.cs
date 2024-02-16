using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("User")]
public partial class User
{
    public long Id { get; set; }

    public string? UserName { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string? Country { get; set; }

    public string? Isdcode { get; set; }

    public bool? TwoFactor { get; set; }

    public bool? Locked { get; set; }

    public bool? IsActive { get; set; }

    public short? EmailValidationStatus { get; set; }

    public short? MobileValidationStatus { get; set; }

    public int OrgId { get; set; }

    public short AdminLevel { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<LoginLog> LoginLogs { get; set; } = new List<LoginLog>();

    public virtual ICollection<LoginTokenLog> LoginTokenLogs { get; set; } = new List<LoginTokenLog>();

    public virtual ICollection<LoginToken> LoginTokens { get; set; } = new List<LoginToken>();

    public virtual Organization Org { get; set; } = null!;

    public virtual ICollection<PasswordLogin> PasswordLogins { get; set; } = new List<PasswordLogin>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
