using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceBooking.Buisness.Migrations
{
    /// <inheritdoc />
    public partial class yashmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaimType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Value = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Mail = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "character varying(400)", unicode: false, maxLength: 400, nullable: false),
                    Phone = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FileType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataFiles = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__3214EC2750CDCC8A", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookupCategory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LookupCa__3214EC2758F40F06", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrgCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    OrgName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionModuleType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionModuleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lookup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    LookupCategoryID = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lookup__3214EC2733770E6A", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Lookup__LookupCa__2FCF1A8A",
                        column: x => x.LookupCategoryID,
                        principalTable: "LookupCategory",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PasswordPolicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MinCaps = table.Column<short>(type: "smallint", nullable: false),
                    MinSmallChars = table.Column<short>(type: "smallint", nullable: false),
                    MinSpecialChars = table.Column<short>(type: "smallint", nullable: false),
                    MinNumber = table.Column<short>(type: "smallint", nullable: false),
                    MinLength = table.Column<short>(type: "smallint", nullable: false),
                    AllowUserName = table.Column<bool>(type: "boolean", nullable: false),
                    DisAllPastPassword = table.Column<short>(type: "smallint", nullable: false),
                    DisAllowedChars = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ChangeIntervalDays = table.Column<short>(type: "smallint", nullable: false),
                    OrgId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordPolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordPolicy_Organization",
                        column: x => x.OrgId,
                        principalTable: "Organization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mobile = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ISDCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TwoFactor = table.Column<bool>(type: "boolean", nullable: true),
                    Locked = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    EmailValidationStatus = table.Column<short>(type: "smallint", nullable: true),
                    MobileValidationStatus = table.Column<short>(type: "smallint", nullable: true),
                    OrgId = table.Column<int>(type: "integer", nullable: false),
                    AdminLevel = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Organization",
                        column: x => x.OrgId,
                        principalTable: "Organization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubPermissionModuleType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    PermissionModuleTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubPermissionModuleTypeId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubPermissionModuleTypeId_PermissionModuleType",
                        column: x => x.PermissionModuleTypeId,
                        principalTable: "PermissionModuleType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    PermissionModuleTypeId = table.Column<int>(type: "integer", nullable: false),
                    PermissionTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_PermissionType_Id",
                        column: x => x.PermissionTypeId,
                        principalTable: "PermissionType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoginLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IPAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Browser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DeviceCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DeviceName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginLog_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoginToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AccessToken = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    AccessTokenExpiry = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DeviceCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DeviceName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginToken_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoginTokenLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeviceCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DeviceName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    AccessTokenExpiry = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginTokenLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginTokenLog_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PasswordLogin",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PasswordHash = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PasswordSalt = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordLogin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordLogin_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DeliveryAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CoordinatesLatitude = table.Column<decimal>(type: "numeric(9,6)", nullable: true),
                    CoordinatesLongitude = table.Column<decimal>(type: "numeric(9,6)", nullable: true),
                    IsSelected = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserAddr__3214EC0771CBBDEE", x => x.Id);
                    table.ForeignKey(
                        name: "FK__UserAddre__UserI__43D61337",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroup_Group_Id",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserGroup_User_Id",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_UserRole",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_UserRole",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    ClaimTypeId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupClaim_ClaimType",
                        column: x => x.ClaimTypeId,
                        principalTable: "ClaimType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupClaim_Group",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupClaim_Permission",
                        column: x => x.Id,
                        principalTable: "Permission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    ClaimTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_ClaimType_Id",
                        column: x => x.ClaimTypeId,
                        principalTable: "ClaimType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleClaim_Permission_Id",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_Id",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ClaimTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_ClaimType_Id",
                        column: x => x.ClaimTypeId,
                        principalTable: "ClaimType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserClaim_Permission_Id",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserClaim_User_Id",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "IsDeleted", "Level", "Name" },
                values: new object[,]
                {
                    { 1L, false, (short)30, "John Doe" },
                    { 2L, false, (short)30, "Raj Doe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupClaim_ClaimTypeId",
                table: "GroupClaim",
                column: "ClaimTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupClaim_GroupId",
                table: "GroupClaim",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginLog_UserId",
                table: "LoginLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginToken_UserId",
                table: "LoginToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginTokenLog_UserId",
                table: "LoginTokenLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lookup_LookupCategoryID",
                table: "Lookup",
                column: "LookupCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordLogin_UserId",
                table: "PasswordLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordPolicy_OrgId",
                table: "PasswordPolicy",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionTypeId",
                table: "Permission",
                column: "PermissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_ClaimTypeId",
                table: "RoleClaim",
                column: "ClaimTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_PermissionId",
                table: "RoleClaim",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubPermissionModuleType_PermissionModuleTypeId",
                table: "SubPermissionModuleType",
                column: "PermissionModuleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_OrgId",
                table: "User",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_ClaimTypeId",
                table: "UserClaim",
                column: "ClaimTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_PermissionId",
                table: "UserClaim",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                table: "UserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId",
                table: "UserGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "DocumentTable");

            migrationBuilder.DropTable(
                name: "GroupClaim");

            migrationBuilder.DropTable(
                name: "LoginLog");

            migrationBuilder.DropTable(
                name: "LoginToken");

            migrationBuilder.DropTable(
                name: "LoginTokenLog");

            migrationBuilder.DropTable(
                name: "Lookup");

            migrationBuilder.DropTable(
                name: "PasswordLogin");

            migrationBuilder.DropTable(
                name: "PasswordPolicy");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "SubPermissionModuleType");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "LookupCategory");

            migrationBuilder.DropTable(
                name: "PermissionModuleType");

            migrationBuilder.DropTable(
                name: "ClaimType");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "PermissionType");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
