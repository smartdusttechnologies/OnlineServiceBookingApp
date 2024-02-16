using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("ClaimType")]
public partial class ClaimType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string Value { get; set; } = null!;

    public virtual ICollection<GroupClaim> GroupClaims { get; set; } = new List<GroupClaim>();

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
}
