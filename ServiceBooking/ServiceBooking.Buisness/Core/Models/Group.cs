using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("Group")]
public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<GroupClaim> GroupClaims { get; set; } = new List<GroupClaim>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}
