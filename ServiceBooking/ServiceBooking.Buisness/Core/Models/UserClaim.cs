using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("UserClaim")]
public partial class UserClaim
{
    public int Id { get; set; }

    public long UserId { get; set; }

    public int PermissionId { get; set; }

    public bool IsDeleted { get; set; }

    public int ClaimTypeId { get; set; }

    public virtual ClaimType ClaimType { get; set; } = null!;

    public virtual Permission Permission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
