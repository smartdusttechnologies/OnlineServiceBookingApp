using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("RoleClaim")]
public partial class RoleClaim
{
    public int Id { get; set; }

    public long RoleId { get; set; }

    public int PermissionId { get; set; }

    public int ClaimTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ClaimType ClaimType { get; set; } = null!;

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
