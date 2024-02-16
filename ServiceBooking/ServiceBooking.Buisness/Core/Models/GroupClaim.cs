using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("GroupClaim")]
public partial class GroupClaim
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int ClaimTypeId { get; set; }

    public int PermissionId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ClaimType ClaimType { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual Permission IdNavigation { get; set; } = null!;
}
