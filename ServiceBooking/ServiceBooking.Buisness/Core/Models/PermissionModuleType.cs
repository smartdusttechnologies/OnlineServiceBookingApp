using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("PermissionModuleType")]
public partial class PermissionModuleType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<SubPermissionModuleType> SubPermissionModuleTypes { get; set; } = new List<SubPermissionModuleType>();
}
