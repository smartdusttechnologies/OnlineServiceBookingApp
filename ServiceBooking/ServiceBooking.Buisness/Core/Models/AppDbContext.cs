using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace ServiceBooking.Buisness.Core.Models.Context;

public partial class AppDbContext : DbContext
{
    //public static AppDbContext Create(IMongoDatabase database) =>
    //    new(new DbContextOptionsBuilder<AppDbContext>()
    //        .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
    //        .Options);

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClaimType> ClaimTypes { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<DocumentTable> DocumentTables { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupClaim> GroupClaims { get; set; }

    public virtual DbSet<LoginLog> LoginLogs { get; set; }

    public virtual DbSet<LoginToken> LoginTokens { get; set; }

    public virtual DbSet<LoginTokenLog> LoginTokenLogs { get; set; }

    public virtual DbSet<Lookup> Lookups { get; set; }

    public virtual DbSet<LookupCategory> LookupCategories { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<PasswordLogin> PasswordLogins { get; set; }

    public virtual DbSet<PasswordPolicy> PasswordPolicies { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionModuleType> PermissionModuleTypes { get; set; }

    public virtual DbSet<PermissionType> PermissionTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleClaim> RoleClaims { get; set; }

    public virtual DbSet<SubPermissionModuleType> SubPermissionModuleTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserClaim> UserClaims { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=raj;Username=postgres;Password=root;");
       
       // optionsBuilder.UseSqlServer("Server=LAPTOP-RLHH2AOR\\SQLEXPRESS;Database=MauiNew; User ID=sa;Password=admin@123;TrustServerCertificate=True").LogTo(Console.WriteLine, LogLevel.Information);
       // optionsBuilder.UseMongoDB("connectionstr","raj").LogTo(Console.WriteLine,LogLevel.Information);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClaimType>(entity =>
        {
            entity.ToTable("ClaimType");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contact");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Message)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DocumentTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC2750CDCC8A");

            entity.ToTable("DocumentTable");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FileType).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GroupClaim>(entity =>
        {
            entity.ToTable("GroupClaim");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ClaimType).WithMany(p => p.GroupClaims)
                .HasForeignKey(d => d.ClaimTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupClaim_ClaimType");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupClaims)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupClaim_Group");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.GroupClaim)
                .HasForeignKey<GroupClaim>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupClaim_Permission");
        });

        modelBuilder.Entity<LoginLog>(entity =>
        {
            entity.ToTable("LoginLog");

            entity.Property(e => e.Browser).HasMaxLength(50);
            entity.Property(e => e.DeviceCode).HasMaxLength(20);
            entity.Property(e => e.DeviceName).HasMaxLength(20);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.LoginDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.LoginLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginLog_User");
        });

        modelBuilder.Entity<LoginToken>(entity =>
        {
            entity.ToTable("LoginToken");

            entity.Property(e => e.AccessToken).HasMaxLength(1000);
            entity.Property(e => e.AccessTokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.DeviceCode).HasMaxLength(50);
            entity.Property(e => e.DeviceName).HasMaxLength(50);
            entity.Property(e => e.RefreshToken).HasMaxLength(1000);
            entity.Property(e => e.RefreshTokenExpiry).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.LoginTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginToken_User");
        });

        modelBuilder.Entity<LoginTokenLog>(entity =>
        {
            entity.ToTable("LoginTokenLog");

            entity.Property(e => e.AccessTokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.DeviceCode).HasMaxLength(50);
            entity.Property(e => e.DeviceName).HasMaxLength(50);
            entity.Property(e => e.RefreshTokenExpiry).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.LoginTokenLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginTokenLog_User");
        });

        modelBuilder.Entity<Lookup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lookup__3214EC2733770E6A");

            entity.ToTable("Lookup");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LookupCategoryId).HasColumnName("LookupCategoryID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.LookupCategory).WithMany(p => p.Lookups)
                .HasForeignKey(d => d.LookupCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lookup__LookupCa__2FCF1A8A");
        });

        modelBuilder.Entity<LookupCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LookupCa__3214EC2758F40F06");

            entity.ToTable("LookupCategory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.ToTable("Organization");

            entity.Property(e => e.OrgCode).HasMaxLength(10);
            entity.Property(e => e.OrgName).HasMaxLength(250);
        });

        modelBuilder.Entity<PasswordLogin>(entity =>
        {
            entity.ToTable("PasswordLogin");

            entity.Property(e => e.ChangeDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(1000);
            entity.Property(e => e.PasswordSalt).HasMaxLength(1000);

            entity.HasOne(d => d.User).WithMany(p => p.PasswordLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PasswordLogin_User");
        });

        modelBuilder.Entity<PasswordPolicy>(entity =>
        {
            entity.ToTable("PasswordPolicy");

            entity.Property(e => e.DisAllowedChars).HasMaxLength(50);

            entity.HasOne(d => d.Org).WithMany(p => p.PasswordPolicies)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PasswordPolicy_Organization");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permission");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PermissionType).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permission_PermissionType_Id");
        });

        modelBuilder.Entity<PermissionModuleType>(entity =>
        {
            entity.ToTable("PermissionModuleType");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PermissionType>(entity =>
        {
            entity.ToTable("PermissionType");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.ToTable("RoleClaim");

            entity.HasOne(d => d.ClaimType).WithMany(p => p.RoleClaims)
                .HasForeignKey(d => d.ClaimTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleClaim_ClaimType_Id");

            entity.HasOne(d => d.Permission).WithMany(p => p.RoleClaims)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleClaim_Permission_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleClaims)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleClaim_Role_Id");
        });

        modelBuilder.Entity<SubPermissionModuleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SubPermissionModuleTypeId");

            entity.ToTable("SubPermissionModuleType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.PermissionModuleType).WithMany(p => p.SubPermissionModuleTypes)
                .HasForeignKey(d => d.PermissionModuleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubPermissionModuleTypeId_PermissionModuleType");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Isdcode)
                .HasMaxLength(50)
                .HasColumnName("ISDCode");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Org).WithMany(p => p.Users)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Organization");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAddr__3214EC0771CBBDEE");

            entity.Property(e => e.CoordinatesLatitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.CoordinatesLongitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.DeliveryAddress).HasMaxLength(255);
            entity.Property(e => e.Details).HasMaxLength(255);
            entity.Property(e => e.Label).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserAddre__UserI__43D61337");
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserPermission");

            entity.ToTable("UserClaim");

            entity.HasOne(d => d.ClaimType).WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.ClaimTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserClaim_ClaimType_Id");

            entity.HasOne(d => d.Permission).WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserClaim_Permission_Id");

            entity.HasOne(d => d.User).WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserClaim_User_Id");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.ToTable("UserGroup");

            entity.HasOne(d => d.Group).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroup_Group_Id");

            entity.HasOne(d => d.User).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroup_User_Id");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Role_UserRole");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserRole");
        });
        modelBuilder.Entity<Role>()
      .HasData(
          new Role
          {
              Id = 1,
              Name = "John Doe",
              Level = 30,
              IsDeleted = false
          },
          new Role
          {
              Id = 2,
              Name = "Raj Doe",
              Level = 30,
              IsDeleted = false
          }
      );
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
