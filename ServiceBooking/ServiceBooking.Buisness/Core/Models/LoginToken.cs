using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("LoginToken")]
public partial class LoginToken
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string AccessToken { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime AccessTokenExpiry { get; set; }

    public string? DeviceCode { get; set; }

    public string? DeviceName { get; set; }

    public DateTime? RefreshTokenExpiry { get; set; }

    public virtual User User { get; set; } = null!;
}
