using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("PasswordLogin")]
public partial class PasswordLogin
{
    public long Id { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public long UserId { get; set; }

    public DateTime? ChangeDate { get; set; }

    public virtual User User { get; set; } = null!;
}
