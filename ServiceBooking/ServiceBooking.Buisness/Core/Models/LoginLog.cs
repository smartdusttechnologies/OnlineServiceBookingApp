using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("LoginLog")]
public partial class LoginLog
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime LoginDate { get; set; }

    public short Status { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Ipaddress { get; set; }

    public string? Browser { get; set; }

    public string? DeviceCode { get; set; }

    public string? DeviceName { get; set; }

    public virtual User User { get; set; } = null!;
}
