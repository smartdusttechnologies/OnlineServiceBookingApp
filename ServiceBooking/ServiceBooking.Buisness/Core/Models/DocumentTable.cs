using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("DocumentTable")]
public partial class DocumentTable
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? FileType { get; set; }

    public byte[]? DataFiles { get; set; }
}
