using MongoDB.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServiceBooking.Buisness.Core.Models;
[Collection("Organization")]
public partial class Organization
{
    public int Id { get; set; }

    public string OrgCode { get; set; } = null!;

    public string OrgName { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<PasswordPolicy> PasswordPolicies { get; set; } = new List<PasswordPolicy>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
