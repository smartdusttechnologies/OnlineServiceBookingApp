using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("LookupCategory")]
public partial class LookupCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Lookup> Lookups { get; set; } = new List<Lookup>();
}
