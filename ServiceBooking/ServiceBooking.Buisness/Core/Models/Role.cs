using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("Role")]
public partial class Role
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public short? Level { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
