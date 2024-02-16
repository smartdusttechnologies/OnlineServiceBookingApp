using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("Contact")]
public partial class Contact
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public int Phone { get; set; }

    public string? Address { get; set; }
    public string? UserName { get; set; }
}
