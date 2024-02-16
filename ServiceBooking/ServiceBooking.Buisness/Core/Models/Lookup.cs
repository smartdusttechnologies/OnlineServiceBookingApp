using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("Lookup")]
public partial class Lookup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int LookupCategoryId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual LookupCategory LookupCategory { get; set; } = null!;
}
