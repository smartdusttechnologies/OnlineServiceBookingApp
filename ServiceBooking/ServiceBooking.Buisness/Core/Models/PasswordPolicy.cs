using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("PasswordPolicy")]
public partial class PasswordPolicy
{
    public int Id { get; set; }

    public short MinCaps { get; set; }

    public short MinSmallChars { get; set; }

    public short MinSpecialChars { get; set; }

    public short MinNumber { get; set; }

    public short MinLength { get; set; }

    public bool AllowUserName { get; set; }

    public short DisAllPastPassword { get; set; }

    public string? DisAllowedChars { get; set; }

    public short ChangeIntervalDays { get; set; }

    public int OrgId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Organization Org { get; set; } = null!;
}
