using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("Permission")]
public partial class Permission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PermissionModuleTypeId { get; set; }

    public int PermissionTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual GroupClaim? GroupClaim { get; set; }

    public virtual PermissionType PermissionType { get; set; } = null!;

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
}
