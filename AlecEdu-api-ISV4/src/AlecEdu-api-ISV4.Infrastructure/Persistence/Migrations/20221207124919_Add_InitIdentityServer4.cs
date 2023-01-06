using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AlecEduapi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInitIdentityServer4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowedAccessTokenSigningAlgorithms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Emphasize = table.Column<bool>(type: "bit", nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProtocolType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequireClientSecret = table.Column<bool>(type: "bit", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequireConsent = table.Column<bool>(type: "bit", nullable: false),
                    AllowRememberConsent = table.Column<bool>(type: "bit", nullable: false),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(type: "bit", nullable: false),
                    RequirePkce = table.Column<bool>(type: "bit", nullable: false),
                    AllowPlainTextPkce = table.Column<bool>(type: "bit", nullable: false),
                    RequireRequestObject = table.Column<bool>(type: "bit", nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(type: "bit", nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(type: "bit", nullable: false),
                    BackChannelLogoutUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(type: "bit", nullable: false),
                    AllowOfflineAccess = table.Column<bool>(type: "bit", nullable: false),
                    IdentityTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    AllowedIdentityTokenSigningAlgorithms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(type: "int", nullable: false),
                    ConsentLifetime = table.Column<int>(type: "int", nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    RefreshTokenUsage = table.Column<int>(type: "int", nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(type: "bit", nullable: false),
                    RefreshTokenExpiration = table.Column<int>(type: "int", nullable: false),
                    AccessTokenType = table.Column<int>(type: "int", nullable: false),
                    EnableLocalLogin = table.Column<bool>(type: "bit", nullable: false),
                    IncludeJwtId = table.Column<bool>(type: "bit", nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(type: "bit", nullable: false),
                    ClientClaimsPrefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserSsoLifetime = table.Column<int>(type: "int", nullable: true),
                    UserCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceCodeLifetime = table.Column<int>(type: "int", nullable: false),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceFlowCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceFlowCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Emphasize = table.Column<bool>(type: "bit", nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TienTe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTienTe = table.Column<string>(type: "varchar(10)", nullable: false),
                    TenTienTe = table.Column<string>(type: "varchar(100)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TienTe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tinh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTinh = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccessTokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshTokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceClaims_ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceProperties_ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceScopes_ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceSecrets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceSecrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceSecrets_ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScopeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiScopeClaims_ApiScopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "ApiScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScopeId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiScopeProperties_ApiScopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "ApiScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientClaims_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientCorsOrigins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCorsOrigins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCorsOrigins_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientGrantTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrantType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGrantTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientGrantTypes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientIdPRestrictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIdPRestrictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientIdPRestrictions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientPostLogoutRedirectUris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostLogoutRedirectUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPostLogoutRedirectUris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPostLogoutRedirectUris_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProperties_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRedirectUris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RedirectUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRedirectUris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRedirectUris_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientScopes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSecrets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSecrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSecrets_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResourceClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityResourceId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResourceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityResourceClaims_IdentityResources_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "IdentityResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResourceProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityResourceId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResourceProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityResourceProperties_IdentityResources_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "IdentityResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuanHuyen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuanHuyen = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TinhId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanHuyen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuanHuyen_Tinh_TinhId",
                        column: x => x.TinhId,
                        principalTable: "Tinh",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasswordOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    GioTinh = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TinhId = table.Column<int>(type: "int", nullable: false),
                    QuanHuyenId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LoaiTaiKhoan = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_QuanHuyen_QuanHuyenId",
                        column: x => x.QuanHuyenId,
                        principalTable: "QuanHuyen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Tinh_TinhId",
                        column: x => x.TinhId,
                        principalTable: "Tinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: false),
                    LuongCoBan = table.Column<decimal>(type: "decimal(19,0)", nullable: false),
                    SoTaiKhoan = table.Column<string>(type: "varchar(30)", nullable: false),
                    NganHang = table.Column<string>(type: "varchar(50)", nullable: false),
                    IdDinhDanh = table.Column<string>(type: "varchar(50)", nullable: false),
                    NoiCap = table.Column<string>(type: "varchar(255)", nullable: false),
                    QueQuan = table.Column<string>(type: "varchar(255)", nullable: false),
                    TienTeId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanVien_TienTe_TienTeId",
                        column: x => x.TienTeId,
                        principalTable: "TienTe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_User_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "31352285-0ae0-44d1-8362-723109f8dbc7", "Admin", "ADMIN" },
                    { 2, "51748c76-05a4-4dad-b8de-26780c54db59", "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "Tinh",
                columns: new[] { "Id", "TenTinh", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Thành phố Hà Nội", 1 },
                    { 2, "Tỉnh Hà Giang", 1 },
                    { 3, "Tỉnh Cao Bằng", 1 },
                    { 4, "Tỉnh Bắc Kạn", 1 },
                    { 5, "Tỉnh Tuyên Quang", 1 },
                    { 6, "Tỉnh Lào Cai", 1 },
                    { 7, "Tỉnh Điện Biên", 1 },
                    { 8, "Tỉnh Lai Châu", 1 },
                    { 9, "Tỉnh Sơn La", 1 },
                    { 10, "Tỉnh Yên Bái", 1 },
                    { 11, "Tỉnh Hoà Bình", 1 },
                    { 12, "Tỉnh Thái Nguyên", 1 },
                    { 13, "Tỉnh Lạng Sơn", 1 },
                    { 14, "Tỉnh Quảng Ninh", 1 },
                    { 15, "Tỉnh Bắc Giang", 1 },
                    { 16, "Tỉnh Phú Thọ", 1 },
                    { 17, "Tỉnh Vĩnh Phúc", 1 },
                    { 18, "Tỉnh Bắc Ninh", 1 },
                    { 19, "Tỉnh Hải Dương", 1 },
                    { 20, "Thành phố Hải Phòng", 1 },
                    { 21, "Tỉnh Hưng Yên", 1 },
                    { 22, "Tỉnh Thái Bình", 1 },
                    { 23, "Tỉnh Hà Nam", 1 },
                    { 24, "Tỉnh Nam Định", 1 },
                    { 25, "Tỉnh Ninh Bình", 1 },
                    { 26, "Tỉnh Thanh Hóa", 1 },
                    { 27, "Tỉnh Nghệ An", 1 },
                    { 28, "Tỉnh Hà Tĩnh", 1 },
                    { 29, "Tỉnh Quảng Bình", 1 },
                    { 30, "Tỉnh Quảng Trị", 1 },
                    { 31, "Tỉnh Thừa Thiên Huế", 1 },
                    { 32, "Thành phố Đà Nẵng", 1 },
                    { 33, "Tỉnh Quảng Nam", 1 },
                    { 34, "Tỉnh Quảng Ngãi", 1 },
                    { 35, "Tỉnh Bình Định", 1 },
                    { 36, "Tỉnh Phú Yên", 1 },
                    { 37, "Tỉnh Khánh Hòa", 1 },
                    { 38, "Tỉnh Ninh Thuận", 1 },
                    { 39, "Tỉnh Bình Thuận", 1 },
                    { 40, "Tỉnh Kon Tum", 1 },
                    { 41, "Tỉnh Gia Lai", 1 },
                    { 42, "Tỉnh Đắk Lắk", 1 },
                    { 43, "Tỉnh Đắk Nông", 1 },
                    { 44, "Tỉnh Lâm Đồng", 1 },
                    { 45, "Tỉnh Bình Phước", 1 },
                    { 46, "Tỉnh Tây Ninh", 1 },
                    { 47, "Tỉnh Bình Dương", 1 },
                    { 48, "Tỉnh Đồng Nai", 1 },
                    { 49, "Tỉnh Bà Rịa - Vũng Tàu", 1 },
                    { 50, "Thành phố Hồ Chí Minh", 1 },
                    { 51, "Tỉnh Long An", 1 },
                    { 52, "Tỉnh Tiền Giang", 1 },
                    { 53, "Tỉnh Bến Tre", 1 },
                    { 54, "Tỉnh Trà Vinh", 1 },
                    { 55, "Tỉnh Vĩnh Long", 1 },
                    { 56, "Tỉnh Đồng Tháp", 1 },
                    { 57, "Tỉnh An Giang", 1 },
                    { 58, "Tỉnh Kiên Giang", 1 },
                    { 59, "Thành phố Cần Thơ", 1 },
                    { 60, "Tỉnh Hậu Giang", 1 },
                    { 61, "Tỉnh Sóc Trăng", 1 },
                    { 62, "Tỉnh Bạc Liêu", 1 },
                    { 63, "Tỉnh Cà Mau", 1 }
                });

            migrationBuilder.InsertData(
                table: "QuanHuyen",
                columns: new[] { "Id", "TenQuanHuyen", "TinhId", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Quận Ba Đình", 1, 1 },
                    { 2, "Quận Hoàn Kiếm", 1, 1 },
                    { 3, "Quận Tây Hồ", 1, 1 },
                    { 4, "Quận Long Biên", 1, 1 },
                    { 5, "Quận Cầu Giấy", 1, 1 },
                    { 6, "Quận Đống Đa", 1, 1 },
                    { 7, "Quận Hai Bà Trưng", 1, 1 },
                    { 8, "Quận Hoàng Mai", 1, 1 },
                    { 9, "Quận Thanh Xuân", 1, 1 },
                    { 10, "Huyện Sóc Sơn", 1, 1 },
                    { 11, "Huyện Đông Anh", 1, 1 },
                    { 12, "Huyện Gia Lâm", 1, 1 },
                    { 13, "Quận Nam Từ Liêm", 1, 1 },
                    { 14, "Huyện Thanh Trì", 1, 1 },
                    { 15, "Quận Bắc Từ Liêm", 1, 1 },
                    { 16, "Huyện Mê Linh", 1, 1 },
                    { 17, "Quận Hà Đông", 1, 1 },
                    { 18, "Thị xã Sơn Tây", 1, 1 },
                    { 19, "Huyện Ba Vì", 1, 1 },
                    { 20, "Huyện Phúc Thọ", 1, 1 },
                    { 21, "Huyện Đan Phượng", 1, 1 },
                    { 22, "Huyện Hoài Đức", 1, 1 },
                    { 23, "Huyện Quốc Oai", 1, 1 },
                    { 24, "Huyện Thạch Thất", 1, 1 },
                    { 25, "Huyện Chương Mỹ", 1, 1 },
                    { 26, "Huyện Thanh Oai", 1, 1 },
                    { 27, "Huyện Thường Tín", 1, 1 },
                    { 28, "Huyện Phú Xuyên", 1, 1 },
                    { 29, "Huyện Ứng Hòa", 1, 1 },
                    { 30, "Huyện Mỹ Đức", 1, 1 },
                    { 31, "Thành phố Hà Giang", 2, 1 },
                    { 32, "Huyện Đồng Văn", 2, 1 },
                    { 33, "Huyện Mèo Vạc", 2, 1 },
                    { 34, "Huyện Yên Minh", 2, 1 },
                    { 35, "Huyện Quản Bạ", 2, 1 },
                    { 36, "Huyện Vị Xuyên", 2, 1 },
                    { 37, "Huyện Bắc Mê", 2, 1 },
                    { 38, "Huyện Hoàng Su Phì", 2, 1 },
                    { 39, "Huyện Xín Mần", 2, 1 },
                    { 40, "Huyện Bắc Quang", 2, 1 },
                    { 41, "Huyện Quang Bình", 2, 1 },
                    { 42, "Thành phố Cao Bằng", 3, 1 },
                    { 43, "Huyện Bảo Lâm", 3, 1 },
                    { 44, "Huyện Bảo Lạc", 3, 1 },
                    { 45, "Huyện Thông Nông", 3, 1 },
                    { 46, "Huyện Hà Quảng", 3, 1 },
                    { 47, "Huyện Trà Lĩnh", 3, 1 },
                    { 48, "Huyện Trùng Khánh", 3, 1 },
                    { 49, "Huyện Hạ Lang", 3, 1 },
                    { 50, "Huyện Quảng Uyên", 3, 1 },
                    { 51, "Huyện Phục Hoà", 3, 1 },
                    { 52, "Huyện Hoà An", 3, 1 },
                    { 53, "Huyện Nguyên Bình", 3, 1 },
                    { 54, "Huyện Thạch An", 3, 1 },
                    { 55, "Thành Phố Bắc Kạn", 4, 1 },
                    { 56, "Huyện Pác Nặm", 4, 1 },
                    { 57, "Huyện Ba Bể", 4, 1 },
                    { 58, "Huyện Ngân Sơn", 4, 1 },
                    { 59, "Huyện Bạch Thông", 4, 1 },
                    { 60, "Huyện Chợ Đồn", 4, 1 },
                    { 61, "Huyện Chợ Mới", 4, 1 },
                    { 62, "Huyện Na Rì", 4, 1 },
                    { 63, "Thành phố Tuyên Quang", 5, 1 },
                    { 64, "Huyện Lâm Bình", 5, 1 },
                    { 65, "Huyện Nà Hang", 5, 1 },
                    { 66, "Huyện Chiêm Hóa", 5, 1 },
                    { 67, "Huyện Hàm Yên", 5, 1 },
                    { 68, "Huyện Yên Sơn", 5, 1 },
                    { 69, "Huyện Sơn Dương", 5, 1 },
                    { 70, "Thành phố Lào Cai", 6, 1 },
                    { 71, "Huyện Bát Xát", 6, 1 },
                    { 72, "Huyện Mường Khương", 6, 1 },
                    { 73, "Huyện Si Ma Cai", 6, 1 },
                    { 74, "Huyện Bắc Hà", 6, 1 },
                    { 75, "Huyện Bảo Thắng", 6, 1 },
                    { 76, "Huyện Bảo Yên", 6, 1 },
                    { 77, "Huyện Sa Pa", 6, 1 },
                    { 78, "Huyện Văn Bàn", 6, 1 },
                    { 79, "Thành phố Điện Biên Phủ", 7, 1 },
                    { 80, "Thị Xã Mường Lay", 7, 1 },
                    { 81, "Huyện Mường Nhé", 7, 1 },
                    { 82, "Huyện Mường Chà", 7, 1 },
                    { 83, "Huyện Tủa Chùa", 7, 1 },
                    { 84, "Huyện Tuần Giáo", 7, 1 },
                    { 85, "Huyện Điện Biên", 7, 1 },
                    { 86, "Huyện Điện Biên Đông", 7, 1 },
                    { 87, "Huyện Mường Ảng", 7, 1 },
                    { 88, "Huyện Nậm Pồ", 7, 1 },
                    { 89, "Thành phố Lai Châu", 8, 1 },
                    { 90, "Huyện Tam Đường", 8, 1 },
                    { 91, "Huyện Mường Tè", 8, 1 },
                    { 92, "Huyện Sìn Hồ", 8, 1 },
                    { 93, "Huyện Phong Thổ", 8, 1 },
                    { 94, "Huyện Than Uyên", 8, 1 },
                    { 95, "Huyện Tân Uyên", 8, 1 },
                    { 96, "Huyện Nậm Nhùn", 8, 1 },
                    { 97, "Thành phố Sơn La", 9, 1 },
                    { 98, "Huyện Quỳnh Nhai", 9, 1 },
                    { 99, "Huyện Thuận Châu", 9, 1 },
                    { 100, "Huyện Mường La", 9, 1 },
                    { 101, "Huyện Bắc Yên", 9, 1 },
                    { 102, "Huyện Phù Yên", 9, 1 },
                    { 103, "Huyện Mộc Châu", 9, 1 },
                    { 104, "Huyện Yên Châu", 9, 1 },
                    { 105, "Huyện Mai Sơn", 9, 1 },
                    { 106, "Huyện Sông Mã", 9, 1 },
                    { 107, "Huyện Sốp Cộp", 9, 1 },
                    { 108, "Huyện Vân Hồ", 9, 1 },
                    { 109, "Thành phố Yên Bái", 10, 1 },
                    { 110, "Thị xã Nghĩa Lộ", 10, 1 },
                    { 111, "Huyện Lục Yên", 10, 1 },
                    { 112, "Huyện Văn Yên", 10, 1 },
                    { 113, "Huyện Mù Căng Chải", 10, 1 },
                    { 114, "Huyện Trấn Yên", 10, 1 },
                    { 115, "Huyện Trạm Tấu", 10, 1 },
                    { 116, "Huyện Văn Chấn", 10, 1 },
                    { 117, "Huyện Yên Bình", 10, 1 },
                    { 118, "Thành phố Hòa Bình", 11, 1 },
                    { 119, "Huyện Đà Bắc", 11, 1 },
                    { 120, "Huyện Kỳ Sơn", 11, 1 },
                    { 121, "Huyện Lương Sơn", 11, 1 },
                    { 122, "Huyện Kim Bôi", 11, 1 },
                    { 123, "Huyện Cao Phong", 11, 1 },
                    { 124, "Huyện Tân Lạc", 11, 1 },
                    { 125, "Huyện Mai Châu", 11, 1 },
                    { 126, "Huyện Lạc Sơn", 11, 1 },
                    { 127, "Huyện Yên Thủy", 11, 1 },
                    { 128, "Huyện Lạc Thủy", 11, 1 },
                    { 129, "Thành phố Thái Nguyên", 12, 1 },
                    { 130, "Thành phố Sông Công", 12, 1 },
                    { 131, "Huyện Định Hóa", 12, 1 },
                    { 132, "Huyện Phú Lương", 12, 1 },
                    { 133, "Huyện Đồng Hỷ", 12, 1 },
                    { 134, "Huyện Võ Nhai", 12, 1 },
                    { 135, "Huyện Đại Từ", 12, 1 },
                    { 136, "Thị xã Phổ Yên", 12, 1 },
                    { 137, "Huyện Phú Bình", 12, 1 },
                    { 138, "Thành phố Lạng Sơn", 13, 1 },
                    { 139, "Huyện Tràng Định", 13, 1 },
                    { 140, "Huyện Bình Gia", 13, 1 },
                    { 141, "Huyện Văn Lãng", 13, 1 },
                    { 142, "Huyện Cao Lộc", 13, 1 },
                    { 143, "Huyện Văn Quan", 13, 1 },
                    { 144, "Huyện Bắc Sơn", 13, 1 },
                    { 145, "Huyện Hữu Lũng", 13, 1 },
                    { 146, "Huyện Chi Lăng", 13, 1 },
                    { 147, "Huyện Lộc Bình", 13, 1 },
                    { 148, "Huyện Đình Lập", 13, 1 },
                    { 149, "Thành phố Hạ Long", 14, 1 },
                    { 150, "Thành phố Móng Cái", 14, 1 },
                    { 151, "Thành phố Cẩm Phả", 14, 1 },
                    { 152, "Thành phố Uông Bí", 14, 1 },
                    { 153, "Huyện Bình Liêu", 14, 1 },
                    { 154, "Huyện Tiên Yên", 14, 1 },
                    { 155, "Huyện Đầm Hà", 14, 1 },
                    { 156, "Huyện Hải Hà", 14, 1 },
                    { 157, "Huyện Ba Chẽ", 14, 1 },
                    { 158, "Huyện Vân Đồn", 14, 1 },
                    { 159, "Huyện Hoành Bồ", 14, 1 },
                    { 160, "Thị xã Đông Triều", 14, 1 },
                    { 161, "Thị xã Quảng Yên", 14, 1 },
                    { 162, "Huyện Cô Tô", 14, 1 },
                    { 163, "Thành phố Bắc Giang", 15, 1 },
                    { 164, "Huyện Yên Thế", 15, 1 },
                    { 165, "Huyện Tân Yên", 15, 1 },
                    { 166, "Huyện Lạng Giang", 15, 1 },
                    { 167, "Huyện Lục Nam", 15, 1 },
                    { 168, "Huyện Lục Ngạn", 15, 1 },
                    { 169, "Huyện Sơn Động", 15, 1 },
                    { 170, "Huyện Yên Dũng", 15, 1 },
                    { 171, "Huyện Việt Yên", 15, 1 },
                    { 172, "Huyện Hiệp Hòa", 15, 1 },
                    { 173, "Thành phố Việt Trì", 16, 1 },
                    { 174, "Thị xã Phú Thọ", 16, 1 },
                    { 175, "Huyện Đoan Hùng", 16, 1 },
                    { 176, "Huyện Hạ Hoà", 16, 1 },
                    { 177, "Huyện Thanh Ba", 16, 1 },
                    { 178, "Huyện Phù Ninh", 16, 1 },
                    { 179, "Huyện Yên Lập", 16, 1 },
                    { 180, "Huyện Cẩm Khê", 16, 1 },
                    { 181, "Huyện Tam Nông", 16, 1 },
                    { 182, "Huyện Lâm Thao", 16, 1 },
                    { 183, "Huyện Thanh Sơn", 16, 1 },
                    { 184, "Huyện Thanh Thuỷ", 16, 1 },
                    { 185, "Huyện Tân Sơn", 16, 1 },
                    { 186, "Thành phố Vĩnh Yên", 17, 1 },
                    { 187, "Thị xã Phúc Yên", 17, 1 },
                    { 188, "Huyện Lập Thạch", 17, 1 },
                    { 189, "Huyện Tam Dương", 17, 1 },
                    { 190, "Huyện Tam Đảo", 17, 1 },
                    { 191, "Huyện Bình Xuyên", 17, 1 },
                    { 192, "Huyện Yên Lạc", 17, 1 },
                    { 193, "Huyện Vĩnh Tường", 17, 1 },
                    { 194, "Huyện Sông Lô", 17, 1 },
                    { 195, "Thành phố Bắc Ninh", 18, 1 },
                    { 196, "Huyện Yên Phong", 18, 1 },
                    { 197, "Huyện Quế Võ", 18, 1 },
                    { 198, "Huyện Tiên Du", 18, 1 },
                    { 199, "Thị xã Từ Sơn", 18, 1 },
                    { 200, "Huyện Thuận Thành", 18, 1 },
                    { 201, "Huyện Gia Bình", 18, 1 },
                    { 202, "Huyện Lương Tài", 18, 1 },
                    { 203, "Thành phố Hải Dương", 19, 1 },
                    { 204, "Thị xã Chí Linh", 19, 1 },
                    { 205, "Huyện Nam Sách", 19, 1 },
                    { 206, "Huyện Kinh Môn", 19, 1 },
                    { 207, "Huyện Kim Thành", 19, 1 },
                    { 208, "Huyện Thanh Hà", 19, 1 },
                    { 209, "Huyện Cẩm Giàng", 19, 1 },
                    { 210, "Huyện Bình Giang", 19, 1 },
                    { 211, "Huyện Gia Lộc", 19, 1 },
                    { 212, "Huyện Tứ Kỳ", 19, 1 },
                    { 213, "Huyện Ninh Giang", 19, 1 },
                    { 214, "Huyện Thanh Miện", 19, 1 },
                    { 215, "Quận Hồng Bàng", 20, 1 },
                    { 216, "Quận Ngô Quyền", 20, 1 },
                    { 217, "Quận Lê Chân", 20, 1 },
                    { 218, "Quận Hải An", 20, 1 },
                    { 219, "Quận Kiến An", 20, 1 },
                    { 220, "Quận Đồ Sơn", 20, 1 },
                    { 221, "Quận Dương Kinh", 20, 1 },
                    { 222, "Huyện Thuỷ Nguyên", 20, 1 },
                    { 223, "Huyện An Dương", 20, 1 },
                    { 224, "Huyện An Lão", 20, 1 },
                    { 225, "Huyện Kiến Thuỵ", 20, 1 },
                    { 226, "Huyện Tiên Lãng", 20, 1 },
                    { 227, "Huyện Vĩnh Bảo", 20, 1 },
                    { 228, "Huyện Cát Hải", 20, 1 },
                    { 229, "Thành phố Hưng Yên", 21, 1 },
                    { 230, "Huyện Văn Lâm", 21, 1 },
                    { 231, "Huyện Văn Giang", 21, 1 },
                    { 232, "Huyện Yên Mỹ", 21, 1 },
                    { 233, "Huyện Mỹ Hào", 21, 1 },
                    { 234, "Huyện Ân Thi", 21, 1 },
                    { 235, "Huyện Khoái Châu", 21, 1 },
                    { 236, "Huyện Kim Động", 21, 1 },
                    { 237, "Huyện Tiên Lữ", 21, 1 },
                    { 238, "Huyện Phù Cừ", 21, 1 },
                    { 239, "Thành phố Thái Bình", 22, 1 },
                    { 240, "Huyện Quỳnh Phụ", 22, 1 },
                    { 241, "Huyện Hưng Hà", 22, 1 },
                    { 242, "Huyện Đông Hưng", 22, 1 },
                    { 243, "Huyện Thái Thụy", 22, 1 },
                    { 244, "Huyện Tiền Hải", 22, 1 },
                    { 245, "Huyện Kiến Xương", 22, 1 },
                    { 246, "Huyện Vũ Thư", 22, 1 },
                    { 247, "Thành phố Phủ Lý", 23, 1 },
                    { 248, "Huyện Duy Tiên", 23, 1 },
                    { 249, "Huyện Kim Bảng", 23, 1 },
                    { 250, "Huyện Thanh Liêm", 23, 1 },
                    { 251, "Huyện Bình Lục", 23, 1 },
                    { 252, "Huyện Lý Nhân", 23, 1 },
                    { 253, "Thành phố Nam Định", 24, 1 },
                    { 254, "Huyện Mỹ Lộc", 24, 1 },
                    { 255, "Huyện Vụ Bản", 24, 1 },
                    { 256, "Huyện Ý Yên", 24, 1 },
                    { 257, "Huyện Nghĩa Hưng", 24, 1 },
                    { 258, "Huyện Nam Trực", 24, 1 },
                    { 259, "Huyện Trực Ninh", 24, 1 },
                    { 260, "Huyện Xuân Trường", 24, 1 },
                    { 261, "Huyện Giao Thủy", 24, 1 },
                    { 262, "Huyện Hải Hậu", 24, 1 },
                    { 263, "Thành phố Ninh Bình", 25, 1 },
                    { 264, "Thành phố Tam Điệp", 25, 1 },
                    { 265, "Huyện Nho Quan", 25, 1 },
                    { 266, "Huyện Gia Viễn", 25, 1 },
                    { 267, "Huyện Hoa Lư", 25, 1 },
                    { 268, "Huyện Yên Khánh", 25, 1 },
                    { 269, "Huyện Kim Sơn", 25, 1 },
                    { 270, "Huyện Yên Mô", 25, 1 },
                    { 271, "Thành phố Thanh Hóa", 26, 1 },
                    { 272, "Thị xã Bỉm Sơn", 26, 1 },
                    { 273, "Thành phố Sầm Sơn", 26, 1 },
                    { 274, "Huyện Mường Lát", 26, 1 },
                    { 275, "Huyện Quan Hóa", 26, 1 },
                    { 276, "Huyện Bá Thước", 26, 1 },
                    { 277, "Huyện Quan Sơn", 26, 1 },
                    { 278, "Huyện Lang Chánh", 26, 1 },
                    { 279, "Huyện Ngọc Lặc", 26, 1 },
                    { 280, "Huyện Cẩm Thủy", 26, 1 },
                    { 281, "Huyện Thạch Thành", 26, 1 },
                    { 282, "Huyện Hà Trung", 26, 1 },
                    { 283, "Huyện Vĩnh Lộc", 26, 1 },
                    { 284, "Huyện Yên Định", 26, 1 },
                    { 285, "Huyện Thọ Xuân", 26, 1 },
                    { 286, "Huyện Thường Xuân", 26, 1 },
                    { 287, "Huyện Triệu Sơn", 26, 1 },
                    { 288, "Huyện Thiệu Hóa", 26, 1 },
                    { 289, "Huyện Hoằng Hóa", 26, 1 },
                    { 290, "Huyện Hậu Lộc", 26, 1 },
                    { 291, "Huyện Nga Sơn", 26, 1 },
                    { 292, "Huyện Như Xuân", 26, 1 },
                    { 293, "Huyện Như Thanh", 26, 1 },
                    { 294, "Huyện Nông Cống", 26, 1 },
                    { 295, "Huyện Đông Sơn", 26, 1 },
                    { 296, "Huyện Quảng Xương", 26, 1 },
                    { 297, "Huyện Tĩnh Gia", 26, 1 },
                    { 298, "Thành phố Vinh", 27, 1 },
                    { 299, "Thị xã Cửa Lò", 27, 1 },
                    { 300, "Thị xã Thái Hoà", 27, 1 },
                    { 301, "Huyện Quế Phong", 27, 1 },
                    { 302, "Huyện Quỳ Châu", 27, 1 },
                    { 303, "Huyện Kỳ Sơn", 27, 1 },
                    { 304, "Huyện Tương Dương", 27, 1 },
                    { 305, "Huyện Nghĩa Đàn", 27, 1 },
                    { 306, "Huyện Quỳ Hợp", 27, 1 },
                    { 307, "Huyện Quỳnh Lưu", 27, 1 },
                    { 308, "Huyện Con Cuông", 27, 1 },
                    { 309, "Huyện Tân Kỳ", 27, 1 },
                    { 310, "Huyện Anh Sơn", 27, 1 },
                    { 311, "Huyện Diễn Châu", 27, 1 },
                    { 312, "Huyện Yên Thành", 27, 1 },
                    { 313, "Huyện Đô Lương", 27, 1 },
                    { 314, "Huyện Thanh Chương", 27, 1 },
                    { 315, "Huyện Nghi Lộc", 27, 1 },
                    { 316, "Huyện Nam Đàn", 27, 1 },
                    { 317, "Huyện Hưng Nguyên", 27, 1 },
                    { 318, "Thị xã Hoàng Mai", 27, 1 },
                    { 319, "Thành phố Hà Tĩnh", 28, 1 },
                    { 320, "Thị xã Hồng Lĩnh", 28, 1 },
                    { 321, "Huyện Hương Sơn", 28, 1 },
                    { 322, "Huyện Đức Thọ", 28, 1 },
                    { 323, "Huyện Vũ Quang", 28, 1 },
                    { 324, "Huyện Nghi Xuân", 28, 1 },
                    { 325, "Huyện Can Lộc", 28, 1 },
                    { 326, "Huyện Hương Khê", 28, 1 },
                    { 327, "Huyện Thạch Hà", 28, 1 },
                    { 328, "Huyện Cẩm Xuyên", 28, 1 },
                    { 329, "Huyện Kỳ Anh", 28, 1 },
                    { 330, "Huyện Lộc Hà", 28, 1 },
                    { 331, "Thị xã Kỳ Anh", 28, 1 },
                    { 332, "Thành Phố Đồng Hới", 29, 1 },
                    { 333, "Huyện Minh Hóa", 29, 1 },
                    { 334, "Huyện Tuyên Hóa", 29, 1 },
                    { 335, "Huyện Quảng Trạch", 29, 1 },
                    { 336, "Huyện Bố Trạch", 29, 1 },
                    { 337, "Huyện Quảng Ninh", 29, 1 },
                    { 338, "Huyện Lệ Thủy", 29, 1 },
                    { 339, "Thị xã Ba Đồn", 29, 1 },
                    { 340, "Thành phố Đông Hà", 30, 1 },
                    { 341, "Thị xã Quảng Trị", 30, 1 },
                    { 342, "Huyện Vĩnh Linh", 30, 1 },
                    { 343, "Huyện Hướng Hóa", 30, 1 },
                    { 344, "Huyện Gio Linh", 30, 1 },
                    { 345, "Huyện Đa Krông", 30, 1 },
                    { 346, "Huyện Cam Lộ", 30, 1 },
                    { 347, "Huyện Triệu Phong", 30, 1 },
                    { 348, "Huyện Hải Lăng", 30, 1 },
                    { 349, "Thành phố Huế", 31, 1 },
                    { 350, "Huyện Phong Điền", 31, 1 },
                    { 351, "Huyện Quảng Điền", 31, 1 },
                    { 352, "Huyện Phú Vang", 31, 1 },
                    { 353, "Thị xã Hương Thủy", 31, 1 },
                    { 354, "Thị xã Hương Trà", 31, 1 },
                    { 355, "Huyện A Lưới", 31, 1 },
                    { 356, "Huyện Phú Lộc", 31, 1 },
                    { 357, "Huyện Nam Đông", 31, 1 },
                    { 358, "Quận Liên Chiểu", 32, 1 },
                    { 359, "Quận Thanh Khê", 32, 1 },
                    { 360, "Quận Hải Châu", 32, 1 },
                    { 361, "Quận Sơn Trà", 32, 1 },
                    { 362, "Quận Ngũ Hành Sơn", 32, 1 },
                    { 363, "Quận Cẩm Lệ", 32, 1 },
                    { 364, "Huyện Hòa Vang", 32, 1 },
                    { 365, "Thành phố Tam Kỳ", 33, 1 },
                    { 366, "Thành phố Hội An", 33, 1 },
                    { 367, "Huyện Tây Giang", 33, 1 },
                    { 368, "Huyện Đông Giang", 33, 1 },
                    { 369, "Huyện Đại Lộc", 33, 1 },
                    { 370, "Thị xã Điện Bàn", 33, 1 },
                    { 371, "Huyện Duy Xuyên", 33, 1 },
                    { 372, "Huyện Quế Sơn", 33, 1 },
                    { 373, "Huyện Nam Giang", 33, 1 },
                    { 374, "Huyện Phước Sơn", 33, 1 },
                    { 375, "Huyện Hiệp Đức", 33, 1 },
                    { 376, "Huyện Thăng Bình", 33, 1 },
                    { 377, "Huyện Tiên Phước", 33, 1 },
                    { 378, "Huyện Bắc Trà My", 33, 1 },
                    { 379, "Huyện Nam Trà My", 33, 1 },
                    { 380, "Huyện Núi Thành", 33, 1 },
                    { 381, "Huyện Phú Ninh", 33, 1 },
                    { 382, "Huyện Nông Sơn", 33, 1 },
                    { 383, "Thành phố Quảng Ngãi", 34, 1 },
                    { 384, "Huyện Bình Sơn", 34, 1 },
                    { 385, "Huyện Trà Bồng", 34, 1 },
                    { 386, "Huyện Tây Trà", 34, 1 },
                    { 387, "Huyện Sơn Tịnh", 34, 1 },
                    { 388, "Huyện Tư Nghĩa", 34, 1 },
                    { 389, "Huyện Sơn Hà", 34, 1 },
                    { 390, "Huyện Sơn Tây", 34, 1 },
                    { 391, "Huyện Minh Long", 34, 1 },
                    { 392, "Huyện Nghĩa Hành", 34, 1 },
                    { 393, "Huyện Mộ Đức", 34, 1 },
                    { 394, "Huyện Đức Phổ", 34, 1 },
                    { 395, "Huyện Ba Tơ", 34, 1 },
                    { 396, "Huyện Lý Sơn", 34, 1 },
                    { 397, "Thành phố Qui Nhơn", 35, 1 },
                    { 398, "Huyện An Lão", 35, 1 },
                    { 399, "Huyện Hoài Nhơn", 35, 1 },
                    { 400, "Huyện Hoài Ân", 35, 1 },
                    { 401, "Huyện Phù Mỹ", 35, 1 },
                    { 402, "Huyện Vĩnh Thạnh", 35, 1 },
                    { 403, "Huyện Tây Sơn", 35, 1 },
                    { 404, "Huyện Phù Cát", 35, 1 },
                    { 405, "Thị xã An Nhơn", 35, 1 },
                    { 406, "Huyện Tuy Phước", 35, 1 },
                    { 407, "Huyện Vân Canh", 35, 1 },
                    { 408, "Thành phố Tuy Hoà", 36, 1 },
                    { 409, "Thị xã Sông Cầu", 36, 1 },
                    { 410, "Huyện Đồng Xuân", 36, 1 },
                    { 411, "Huyện Tuy An", 36, 1 },
                    { 412, "Huyện Sơn Hòa", 36, 1 },
                    { 413, "Huyện Sông Hinh", 36, 1 },
                    { 414, "Huyện Tây Hoà", 36, 1 },
                    { 415, "Huyện Phú Hoà", 36, 1 },
                    { 416, "Huyện Đông Hòa", 36, 1 },
                    { 417, "Thành phố Nha Trang", 37, 1 },
                    { 418, "Thành phố Cam Ranh", 37, 1 },
                    { 419, "Huyện Cam Lâm", 37, 1 },
                    { 420, "Huyện Vạn Ninh", 37, 1 },
                    { 421, "Thị xã Ninh Hòa", 37, 1 },
                    { 422, "Huyện Khánh Vĩnh", 37, 1 },
                    { 423, "Huyện Diên Khánh", 37, 1 },
                    { 424, "Huyện Khánh Sơn", 37, 1 },
                    { 425, "Huyện Trường Sa", 37, 1 },
                    { 426, "Thành phố Phan Rang-Tháp Chàm", 38, 1 },
                    { 427, "Huyện Bác Ái", 38, 1 },
                    { 428, "Huyện Ninh Sơn", 38, 1 },
                    { 429, "Huyện Ninh Hải", 38, 1 },
                    { 430, "Huyện Ninh Phước", 38, 1 },
                    { 431, "Huyện Thuận Bắc", 38, 1 },
                    { 432, "Huyện Thuận Nam", 38, 1 },
                    { 433, "Thành phố Phan Thiết", 39, 1 },
                    { 434, "Thị xã La Gi", 39, 1 },
                    { 435, "Huyện Tuy Phong", 39, 1 },
                    { 436, "Huyện Bắc Bình", 39, 1 },
                    { 437, "Huyện Hàm Thuận Bắc", 39, 1 },
                    { 438, "Huyện Hàm Thuận Nam", 39, 1 },
                    { 439, "Huyện Tánh Linh", 39, 1 },
                    { 440, "Huyện Đức Linh", 39, 1 },
                    { 441, "Huyện Hàm Tân", 39, 1 },
                    { 442, "Huyện Phú Quí", 39, 1 },
                    { 443, "Thành phố Kon Tum", 40, 1 },
                    { 444, "Huyện Đắk Glei", 40, 1 },
                    { 445, "Huyện Ngọc Hồi", 40, 1 },
                    { 446, "Huyện Đắk Tô", 40, 1 },
                    { 447, "Huyện Kon Plông", 40, 1 },
                    { 448, "Huyện Kon Rẫy", 40, 1 },
                    { 449, "Huyện Đắk Hà", 40, 1 },
                    { 450, "Huyện Sa Thầy", 40, 1 },
                    { 451, "Huyện Tu Mơ Rông", 40, 1 },
                    { 452, "Huyện Ia H'' Drai", 40, 1 },
                    { 453, "Thành phố Pleiku", 41, 1 },
                    { 454, "Thị xã An Khê", 41, 1 },
                    { 455, "Thị xã Ayun Pa", 41, 1 },
                    { 456, "Huyện KBang", 41, 1 },
                    { 457, "Huyện Đăk Đoa", 41, 1 },
                    { 458, "Huyện Chư Păh", 41, 1 },
                    { 459, "Huyện Ia Grai", 41, 1 },
                    { 460, "Huyện Mang Yang", 41, 1 },
                    { 461, "Huyện Kông Chro", 41, 1 },
                    { 462, "Huyện Đức Cơ", 41, 1 },
                    { 463, "Huyện Chư Prông", 41, 1 },
                    { 464, "Huyện Chư Sê", 41, 1 },
                    { 465, "Huyện Đăk Pơ", 41, 1 },
                    { 466, "Huyện Ia Pa", 41, 1 },
                    { 467, "Huyện Krông Pa", 41, 1 },
                    { 468, "Huyện Phú Thiện", 41, 1 },
                    { 469, "Huyện Chư Pưh", 41, 1 },
                    { 470, "Thành phố Buôn Ma Thuột", 42, 1 },
                    { 471, "Thị Xã Buôn Hồ", 42, 1 },
                    { 472, "Huyện Ea H''leo", 42, 1 },
                    { 473, "Huyện Ea Súp", 42, 1 },
                    { 474, "Huyện Buôn Đôn", 42, 1 },
                    { 475, "Huyện Cư M''gar", 42, 1 },
                    { 476, "Huyện Krông Búk", 42, 1 },
                    { 477, "Huyện Krông Năng", 42, 1 },
                    { 478, "Huyện Ea Kar", 42, 1 },
                    { 479, "Huyện M''Đrắk", 42, 1 },
                    { 480, "Huyện Krông Bông", 42, 1 },
                    { 481, "Huyện Krông Pắc", 42, 1 },
                    { 482, "Huyện Krông A Na", 42, 1 },
                    { 483, "Huyện Lắk", 42, 1 },
                    { 484, "Huyện Cư Kuin", 42, 1 },
                    { 485, "Thị xã Gia Nghĩa", 43, 1 },
                    { 486, "Huyện Đăk Glong", 43, 1 },
                    { 487, "Huyện Cư Jút", 43, 1 },
                    { 488, "Huyện Đắk Mil", 43, 1 },
                    { 489, "Huyện Krông Nô", 43, 1 },
                    { 490, "Huyện Đắk Song", 43, 1 },
                    { 491, "Huyện Đắk R''Lấp", 43, 1 },
                    { 492, "Huyện Tuy Đức", 43, 1 },
                    { 493, "Thành phố Đà Lạt", 44, 1 },
                    { 494, "Thành phố Bảo Lộc", 44, 1 },
                    { 495, "Huyện Đam Rông", 44, 1 },
                    { 496, "Huyện Lạc Dương", 44, 1 },
                    { 497, "Huyện Lâm Hà", 44, 1 },
                    { 498, "Huyện Đơn Dương", 44, 1 },
                    { 499, "Huyện Đức Trọng", 44, 1 },
                    { 500, "Huyện Di Linh", 44, 1 },
                    { 501, "Huyện Bảo Lâm", 44, 1 },
                    { 502, "Huyện Đạ Huoai", 44, 1 },
                    { 503, "Huyện Đạ Tẻh", 44, 1 },
                    { 504, "Huyện Cát Tiên", 44, 1 },
                    { 505, "Thị xã Phước Long", 45, 1 },
                    { 506, "Thị xã Đồng Xoài", 45, 1 },
                    { 507, "Thị xã Bình Long", 45, 1 },
                    { 508, "Huyện Bù Gia Mập", 45, 1 },
                    { 509, "Huyện Lộc Ninh", 45, 1 },
                    { 510, "Huyện Bù Đốp", 45, 1 },
                    { 511, "Huyện Hớn Quản", 45, 1 },
                    { 512, "Huyện Đồng Phú", 45, 1 },
                    { 513, "Huyện Bù Đăng", 45, 1 },
                    { 514, "Huyện Chơn Thành", 45, 1 },
                    { 515, "Huyện Phú Riềng", 45, 1 },
                    { 516, "Thành phố Tây Ninh", 46, 1 },
                    { 517, "Huyện Tân Biên", 46, 1 },
                    { 518, "Huyện Tân Châu", 46, 1 },
                    { 519, "Huyện Dương Minh Châu", 46, 1 },
                    { 520, "Huyện Châu Thành", 46, 1 },
                    { 521, "Huyện Hòa Thành", 46, 1 },
                    { 522, "Huyện Gò Dầu", 46, 1 },
                    { 523, "Huyện Bến Cầu", 46, 1 },
                    { 524, "Huyện Trảng Bàng", 46, 1 },
                    { 525, "Thành phố Thủ Dầu Một", 47, 1 },
                    { 526, "Huyện Bàu Bàng", 47, 1 },
                    { 527, "Huyện Dầu Tiếng", 47, 1 },
                    { 528, "Thị xã Bến Cát", 47, 1 },
                    { 529, "Huyện Phú Giáo", 47, 1 },
                    { 530, "Thị xã Tân Uyên", 47, 1 },
                    { 531, "Thị xã Dĩ An", 47, 1 },
                    { 532, "Thị xã Thuận An", 47, 1 },
                    { 533, "Huyện Bắc Tân Uyên", 47, 1 },
                    { 534, "Thành phố Biên Hòa", 48, 1 },
                    { 535, "Thị xã Long Khánh", 48, 1 },
                    { 536, "Huyện Tân Phú", 48, 1 },
                    { 537, "Huyện Vĩnh Cửu", 48, 1 },
                    { 538, "Huyện Định Quán", 48, 1 },
                    { 539, "Huyện Trảng Bom", 48, 1 },
                    { 540, "Huyện Thống Nhất", 48, 1 },
                    { 541, "Huyện Cẩm Mỹ", 48, 1 },
                    { 542, "Huyện Long Thành", 48, 1 },
                    { 543, "Huyện Xuân Lộc", 48, 1 },
                    { 544, "Huyện Nhơn Trạch", 48, 1 },
                    { 545, "Thành phố Vũng Tàu", 49, 1 },
                    { 546, "Thành phố Bà Rịa", 49, 1 },
                    { 547, "Huyện Châu Đức", 49, 1 },
                    { 548, "Huyện Xuyên Mộc", 49, 1 },
                    { 549, "Huyện Long Điền", 49, 1 },
                    { 550, "Huyện Đất Đỏ", 49, 1 },
                    { 551, "Huyện Tân Thành", 49, 1 },
                    { 552, "Quận 1", 50, 1 },
                    { 553, "Quận 12", 50, 1 },
                    { 554, "Quận Thủ Đức", 50, 1 },
                    { 555, "Quận 9", 50, 1 },
                    { 556, "Quận Gò Vấp", 50, 1 },
                    { 557, "Quận Bình Thạnh", 50, 1 },
                    { 558, "Quận Tân Bình", 50, 1 },
                    { 559, "Quận Tân Phú", 50, 1 },
                    { 560, "Quận Phú Nhuận", 50, 1 },
                    { 561, "Quận 2", 50, 1 },
                    { 562, "Quận 3", 50, 1 },
                    { 563, "Quận 10", 50, 1 },
                    { 564, "Quận 11", 50, 1 },
                    { 565, "Quận 4", 50, 1 },
                    { 566, "Quận 5", 50, 1 },
                    { 567, "Quận 6", 50, 1 },
                    { 568, "Quận 8", 50, 1 },
                    { 569, "Quận Bình Tân", 50, 1 },
                    { 570, "Quận 7", 50, 1 },
                    { 571, "Huyện Củ Chi", 50, 1 },
                    { 572, "Huyện Hóc Môn", 50, 1 },
                    { 573, "Huyện Bình Chánh", 50, 1 },
                    { 574, "Huyện Nhà Bè", 50, 1 },
                    { 575, "Huyện Cần Giờ", 50, 1 },
                    { 576, "Thành phố Tân An", 51, 1 },
                    { 577, "Thị xã Kiến Tường", 51, 1 },
                    { 578, "Huyện Tân Hưng", 51, 1 },
                    { 579, "Huyện Vĩnh Hưng", 51, 1 },
                    { 580, "Huyện Mộc Hóa", 51, 1 },
                    { 581, "Huyện Tân Thạnh", 51, 1 },
                    { 582, "Huyện Thạnh Hóa", 51, 1 },
                    { 583, "Huyện Đức Huệ", 51, 1 },
                    { 584, "Huyện Đức Hòa", 51, 1 },
                    { 585, "Huyện Bến Lức", 51, 1 },
                    { 586, "Huyện Thủ Thừa", 51, 1 },
                    { 587, "Huyện Tân Trụ", 51, 1 },
                    { 588, "Huyện Cần Đước", 51, 1 },
                    { 589, "Huyện Cần Giuộc", 51, 1 },
                    { 590, "Huyện Châu Thành", 51, 1 },
                    { 591, "Thành phố Mỹ Tho", 52, 1 },
                    { 592, "Thị xã Gò Công", 52, 1 },
                    { 593, "Thị xã Cai Lậy", 52, 1 },
                    { 594, "Huyện Tân Phước", 52, 1 },
                    { 595, "Huyện Cái Bè", 52, 1 },
                    { 596, "Huyện Cai Lậy", 52, 1 },
                    { 597, "Huyện Châu Thành", 52, 1 },
                    { 598, "Huyện Chợ Gạo", 52, 1 },
                    { 599, "Huyện Gò Công Tây", 52, 1 },
                    { 600, "Huyện Gò Công Đông", 52, 1 },
                    { 601, "Huyện Tân Phú Đông", 52, 1 },
                    { 602, "Thành phố Bến Tre", 53, 1 },
                    { 603, "Huyện Châu Thành", 53, 1 },
                    { 604, "Huyện Chợ Lách", 53, 1 },
                    { 605, "Huyện Mỏ Cày Nam", 53, 1 },
                    { 606, "Huyện Giồng Trôm", 53, 1 },
                    { 607, "Huyện Bình Đại", 53, 1 },
                    { 608, "Huyện Ba Tri", 53, 1 },
                    { 609, "Huyện Thạnh Phú", 53, 1 },
                    { 610, "Huyện Mỏ Cày Bắc", 53, 1 },
                    { 611, "Thành phố Trà Vinh", 54, 1 },
                    { 612, "Huyện Càng Long", 54, 1 },
                    { 613, "Huyện Cầu Kè", 54, 1 },
                    { 614, "Huyện Tiểu Cần", 54, 1 },
                    { 615, "Huyện Châu Thành", 54, 1 },
                    { 616, "Huyện Cầu Ngang", 54, 1 },
                    { 617, "Huyện Trà Cú", 54, 1 },
                    { 618, "Huyện Duyên Hải", 54, 1 },
                    { 619, "Thị xã Duyên Hải", 54, 1 },
                    { 620, "Thành phố Vĩnh Long", 55, 1 },
                    { 621, "Huyện Long Hồ", 55, 1 },
                    { 622, "Huyện Mang Thít", 55, 1 },
                    { 623, "Huyện  Vũng Liêm", 55, 1 },
                    { 624, "Huyện Tam Bình", 55, 1 },
                    { 625, "Thị xã Bình Minh", 55, 1 },
                    { 626, "Huyện Trà Ôn", 55, 1 },
                    { 627, "Huyện Bình Tân", 55, 1 },
                    { 628, "Thành phố Cao Lãnh", 56, 1 },
                    { 629, "Thành phố Sa Đéc", 56, 1 },
                    { 630, "Thị xã Hồng Ngự", 56, 1 },
                    { 631, "Huyện Tân Hồng", 56, 1 },
                    { 632, "Huyện Hồng Ngự", 56, 1 },
                    { 633, "Huyện Tam Nông", 56, 1 },
                    { 634, "Huyện Tháp Mười", 56, 1 },
                    { 635, "Huyện Cao Lãnh", 56, 1 },
                    { 636, "Huyện Thanh Bình", 56, 1 },
                    { 637, "Huyện Lấp Vò", 56, 1 },
                    { 638, "Huyện Lai Vung", 56, 1 },
                    { 639, "Huyện Châu Thành", 56, 1 },
                    { 640, "Thành phố Long Xuyên", 57, 1 },
                    { 641, "Thành phố Châu Đốc", 57, 1 },
                    { 642, "Huyện An Phú", 57, 1 },
                    { 643, "Thị xã Tân Châu", 57, 1 },
                    { 644, "Huyện Phú Tân", 57, 1 },
                    { 645, "Huyện Châu Phú", 57, 1 },
                    { 646, "Huyện Tịnh Biên", 57, 1 },
                    { 647, "Huyện Tri Tôn", 57, 1 },
                    { 648, "Huyện Châu Thành", 57, 1 },
                    { 649, "Huyện Chợ Mới", 57, 1 },
                    { 650, "Huyện Thoại Sơn", 57, 1 },
                    { 651, "Thành phố Rạch Giá", 58, 1 },
                    { 652, "Thị xã Hà Tiên", 58, 1 },
                    { 653, "Huyện Kiên Lương", 58, 1 },
                    { 654, "Huyện Hòn Đất", 58, 1 },
                    { 655, "Huyện Tân Hiệp", 58, 1 },
                    { 656, "Huyện Châu Thành", 58, 1 },
                    { 657, "Huyện Giồng Riềng", 58, 1 },
                    { 658, "Huyện Gò Quao", 58, 1 },
                    { 659, "Huyện An Biên", 58, 1 },
                    { 660, "Huyện An Minh", 58, 1 },
                    { 661, "Huyện Vĩnh Thuận", 58, 1 },
                    { 662, "Huyện Phú Quốc", 58, 1 },
                    { 663, "Huyện Kiên Hải", 58, 1 },
                    { 664, "Huyện U Minh Thượng", 58, 1 },
                    { 665, "Huyện Giang Thành", 58, 1 },
                    { 666, "Quận Ninh Kiều", 59, 1 },
                    { 667, "Quận Ô Môn", 59, 1 },
                    { 668, "Quận Bình Thuỷ", 59, 1 },
                    { 669, "Quận Cái Răng", 59, 1 },
                    { 670, "Quận Thốt Nốt", 59, 1 },
                    { 671, "Huyện Vĩnh Thạnh", 59, 1 },
                    { 672, "Huyện Cờ Đỏ", 59, 1 },
                    { 673, "Huyện Phong Điền", 59, 1 },
                    { 674, "Huyện Thới Lai", 59, 1 },
                    { 675, "Thành phố Vị Thanh", 60, 1 },
                    { 676, "Thị xã Ngã Bảy", 60, 1 },
                    { 677, "Huyện Châu Thành A", 60, 1 },
                    { 678, "Huyện Châu Thành", 60, 1 },
                    { 679, "Huyện Phụng Hiệp", 60, 1 },
                    { 680, "Huyện Vị Thuỷ", 60, 1 },
                    { 681, "Huyện Long Mỹ", 60, 1 },
                    { 682, "Thị xã Long Mỹ", 60, 1 },
                    { 683, "Thành phố Sóc Trăng", 61, 1 },
                    { 684, "Huyện Châu Thành", 61, 1 },
                    { 685, "Huyện Kế Sách", 61, 1 },
                    { 686, "Huyện Mỹ Tú", 61, 1 },
                    { 687, "Huyện Cù Lao Dung", 61, 1 },
                    { 688, "Huyện Long Phú", 61, 1 },
                    { 689, "Huyện Mỹ Xuyên", 61, 1 },
                    { 690, "Thị xã Ngã Năm", 61, 1 },
                    { 691, "Huyện Thạnh Trị", 61, 1 },
                    { 692, "Thị xã Vĩnh Châu", 61, 1 },
                    { 693, "Huyện Trần Đề", 61, 1 },
                    { 694, "Thành phố Bạc Liêu", 62, 1 },
                    { 695, "Huyện Hồng Dân", 62, 1 },
                    { 696, "Huyện Phước Long", 62, 1 },
                    { 697, "Huyện Vĩnh Lợi", 62, 1 },
                    { 698, "Thị xã Giá Rai", 62, 1 },
                    { 699, "Huyện Đông Hải", 62, 1 },
                    { 700, "Huyện Hoà Bình", 62, 1 },
                    { 701, "Thành phố Cà Mau", 63, 1 },
                    { 702, "Huyện U Minh", 63, 1 },
                    { 703, "Huyện Thới Bình", 63, 1 },
                    { 704, "Huyện Trần Văn Thời", 63, 1 },
                    { 705, "Huyện Cái Nước", 63, 1 },
                    { 706, "Huyện Đầm Dơi", 63, 1 },
                    { 707, "Huyện Năm Căn", 63, 1 },
                    { 708, "Huyện Phú Tân", 63, 1 },
                    { 709, "Huyện Ngọc Hiển", 63, 1 }
                });

            migrationBuilder.InsertData(
                table: "RoleClaim",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "users", "users.list", 1 },
                    { 2, "users", "users.create", 1 },
                    { 3, "users", "users.update", 1 },
                    { 4, "users", "users.delete", 1 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "DiaChi", "Email", "EmailConfirmed", "GioTinh", "HoTen", "LockoutEnabled", "LockoutEnd", "NgaySinh", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PasswordOrigin", "PhoneNumber", "PhoneNumberConfirmed", "QuanHuyenId", "SecurityStamp", "TinhId", "TrangThai", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[] { 1, 0, "3c19cf43-31d5-40b6-8ab8-1add1d946994", new DateTime(2022, 12, 7, 12, 49, 19, 60, DateTimeKind.Utc).AddTicks(9050), null, "ABC", "huycanh14@gmail.com", false, 1, "Admin dev", false, null, new DateTime(1998, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "HUYCANH14@GMAIL.COM", "DEV", "AQAAAAEAACcQAAAAEHIokiouRnK+o3++FmVtSlBa3U5ZFcI516cqWMQX0etLrEGQRJkyJwn0Sgl7W/uW1A==", "123321", null, false, 1, "JWKXHWH4JQ7XMPIWWHLW43VI4LWDWJAJ", 1, 1, false, null, "dev" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "DiaChi", "Email", "EmailConfirmed", "GioTinh", "HoTen", "LoaiTaiKhoan", "LockoutEnabled", "LockoutEnd", "NgaySinh", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PasswordOrigin", "PhoneNumber", "PhoneNumberConfirmed", "QuanHuyenId", "SecurityStamp", "TinhId", "TrangThai", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[] { 2, 0, "2016710c-2fda-4968-bc4f-1910f9dbb311", new DateTime(2022, 12, 7, 12, 49, 19, 61, DateTimeKind.Utc).AddTicks(330), null, "ABC", "dev1@gmail.com", false, 1, "Admin dev", -2, false, null, new DateTime(1998, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "DEV1@GMAIL.COM", "DEV1", "AQAAAAEAACcQAAAAECBlyat5+H0SslKbuG+/RXShQOB/XF727fdmINc2i9NUIuypwtXB6wxNqTSIMMSsnw==", "123321", null, false, 1, "JWKXHWH4JQ7XMPIWWHLW43VI4LWDWJAJ", 1, 1, false, null, "dev1" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceClaims_ApiResourceId",
                table: "ApiResourceClaims",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceProperties_ApiResourceId",
                table: "ApiResourceProperties",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceScopes_ApiResourceId",
                table: "ApiResourceScopes",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceSecrets_ApiResourceId",
                table: "ApiResourceSecrets",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeClaims_ScopeId",
                table: "ApiScopeClaims",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeProperties_ScopeId",
                table: "ApiScopeProperties",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientClaims_ClientId",
                table: "ClientClaims",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCorsOrigins_ClientId",
                table: "ClientCorsOrigins",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientGrantTypes_ClientId",
                table: "ClientGrantTypes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientIdPRestrictions_ClientId",
                table: "ClientIdPRestrictions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPostLogoutRedirectUris_ClientId",
                table: "ClientPostLogoutRedirectUris",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProperties_ClientId",
                table: "ClientProperties",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRedirectUris_ClientId",
                table: "ClientRedirectUris",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientScopes_ClientId",
                table: "ClientScopes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSecrets_ClientId",
                table: "ClientSecrets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceFlowCodes_DeviceCode",
                table: "DeviceFlowCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceFlowCodes_Expiration",
                table: "DeviceFlowCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResourceClaims_IdentityResourceId",
                table: "IdentityResourceClaims",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResourceProperties_IdentityResourceId",
                table: "IdentityResourceProperties",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_TaiKhoanId",
                table: "NhanVien",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_TienTeId",
                table: "NhanVien",
                column: "TienTeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_QuanHuyen_TinhId",
                table: "QuanHuyen",
                column: "TinhId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_User_QuanHuyenId",
                table: "User",
                column: "QuanHuyenId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TinhId",
                table: "User",
                column: "TinhId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiResourceClaims");

            migrationBuilder.DropTable(
                name: "ApiResourceProperties");

            migrationBuilder.DropTable(
                name: "ApiResourceScopes");

            migrationBuilder.DropTable(
                name: "ApiResourceSecrets");

            migrationBuilder.DropTable(
                name: "ApiScopeClaims");

            migrationBuilder.DropTable(
                name: "ApiScopeProperties");

            migrationBuilder.DropTable(
                name: "ClientClaims");

            migrationBuilder.DropTable(
                name: "ClientCorsOrigins");

            migrationBuilder.DropTable(
                name: "ClientGrantTypes");

            migrationBuilder.DropTable(
                name: "ClientIdPRestrictions");

            migrationBuilder.DropTable(
                name: "ClientPostLogoutRedirectUris");

            migrationBuilder.DropTable(
                name: "ClientProperties");

            migrationBuilder.DropTable(
                name: "ClientRedirectUris");

            migrationBuilder.DropTable(
                name: "ClientScopes");

            migrationBuilder.DropTable(
                name: "ClientSecrets");

            migrationBuilder.DropTable(
                name: "DeviceFlowCodes");

            migrationBuilder.DropTable(
                name: "IdentityResourceClaims");

            migrationBuilder.DropTable(
                name: "IdentityResourceProperties");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "ApiResources");

            migrationBuilder.DropTable(
                name: "ApiScopes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "IdentityResources");

            migrationBuilder.DropTable(
                name: "TienTe");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "QuanHuyen");

            migrationBuilder.DropTable(
                name: "Tinh");
        }
    }
}
