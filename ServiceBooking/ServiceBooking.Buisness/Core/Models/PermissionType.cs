using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("PermissionType")]
public partial class PermissionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
