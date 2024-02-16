using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("UserAddress")]
public partial class UserAddress
{
    public int Id { get; set; }

    public long? UserId { get; set; }

    public string? Label { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? Details { get; set; }

    public decimal? CoordinatesLatitude { get; set; }

    public decimal? CoordinatesLongitude { get; set; }

    public bool? IsSelected { get; set; }

    public virtual User? User { get; set; }
}
