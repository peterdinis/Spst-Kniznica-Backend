﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LibrarySPSTApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table =>
                    new
                    {
                        Id = table.Column<string>(type: "text", nullable: false),
                        Name = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        NormalizedName = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table =>
                    new
                    {
                        Id = table.Column<string>(type: "text", nullable: false),
                        FirstName = table.Column<string>(type: "text", nullable: false),
                        LastName = table.Column<string>(type: "text", nullable: false),
                        Discriminator = table.Column<string>(
                            type: "character varying(21)",
                            maxLength: 21,
                            nullable: false
                        ),
                        UserName = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        NormalizedUserName = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        Email = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        NormalizedEmail = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                        PasswordHash = table.Column<string>(type: "text", nullable: true),
                        SecurityStamp = table.Column<string>(type: "text", nullable: true),
                        ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                        PhoneNumber = table.Column<string>(type: "text", nullable: true),
                        PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                        TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                        LockoutEnd = table.Column<DateTimeOffset>(
                            type: "timestamp with time zone",
                            nullable: true
                        ),
                        LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                        AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "authors",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Name = table.Column<string>(type: "text", nullable: false),
                        AuthorDescription = table.Column<string>(type: "text", nullable: false),
                        AuthorImage = table.Column<string>(type: "text", nullable: false),
                        LitPeriod = table.Column<string>(type: "text", nullable: false),
                        Birth = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        Death = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Description = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        RoleId = table.Column<string>(type: "text", nullable: false),
                        ClaimType = table.Column<string>(type: "text", nullable: true),
                        ClaimValue = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        UserId = table.Column<string>(type: "text", nullable: false),
                        ClaimType = table.Column<string>(type: "text", nullable: true),
                        ClaimValue = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table =>
                    new
                    {
                        LoginProvider = table.Column<string>(type: "text", nullable: false),
                        ProviderKey = table.Column<string>(type: "text", nullable: false),
                        ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                        UserId = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_AspNetUserLogins",
                        x => new { x.LoginProvider, x.ProviderKey }
                    );
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table =>
                    new
                    {
                        UserId = table.Column<string>(type: "text", nullable: false),
                        RoleId = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table =>
                    new
                    {
                        UserId = table.Column<string>(type: "text", nullable: false),
                        LoginProvider = table.Column<string>(type: "text", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Value = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_AspNetUserTokens",
                        x =>
                            new
                            {
                                x.UserId,
                                x.LoginProvider,
                                x.Name
                            }
                    );
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "books",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Description = table.Column<string>(type: "text", nullable: false),
                        AuthorName = table.Column<string>(type: "text", nullable: false),
                        Pages = table.Column<string>(type: "text", nullable: false),
                        Status = table.Column<string>(type: "text", nullable: false),
                        Year = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        Image = table.Column<string>(type: "text", nullable: false),
                        Quantity = table.Column<string>(type: "text", nullable: false),
                        Publisher = table.Column<string>(type: "text", nullable: false),
                        CategoryId = table.Column<int>(type: "integer", nullable: false),
                        AuthorId = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_books_authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_books_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail"
            );

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_books_AuthorId",
                table: "books",
                column: "AuthorId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_books_CategoryId",
                table: "books",
                column: "CategoryId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AspNetRoleClaims");

            migrationBuilder.DropTable(name: "AspNetUserClaims");

            migrationBuilder.DropTable(name: "AspNetUserLogins");

            migrationBuilder.DropTable(name: "AspNetUserRoles");

            migrationBuilder.DropTable(name: "AspNetUserTokens");

            migrationBuilder.DropTable(name: "books");

            migrationBuilder.DropTable(name: "AspNetRoles");

            migrationBuilder.DropTable(name: "AspNetUsers");

            migrationBuilder.DropTable(name: "authors");

            migrationBuilder.DropTable(name: "categories");
        }
    }
}
