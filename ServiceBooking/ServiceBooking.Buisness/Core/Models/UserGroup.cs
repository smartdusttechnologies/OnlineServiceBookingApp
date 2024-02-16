using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("UserGroup")]
public partial class UserGroup
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public long UserId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
