using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CakeFactoryProd.Migrations
{
    public partial class di : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filling",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flavor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    priceFactor = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filling", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Shape",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    priceFactor = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shape", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    cakeBasicPrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Topping",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flavor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    priceFactor = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topping", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    preferredName = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    phoneNumber = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cake",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    imagePath = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fillingId = table.Column<int>(type: "int", nullable: false),
                    sizeId = table.Column<int>(type: "int", nullable: false),
                    shapeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cake", x => x.id);
                    table.ForeignKey(
                        name: "FK__Cake__fillingId__35BCFE0A",
                        column: x => x.fillingId,
                        principalTable: "Filling",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Cake__shapeId__37A5467C",
                        column: x => x.shapeId,
                        principalTable: "Shape",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Cake__sizeId__36B12243",
                        column: x => x.sizeId,
                        principalTable: "Size",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchaseDate = table.Column<DateTime>(type: "date", nullable: true),
                    pickupDate = table.Column<DateTime>(type: "date", nullable: true),
                    isPicked = table.Column<bool>(type: "bit", nullable: false),
                    totalAmount = table.Column<decimal>(type: "money", nullable: false),
                    openOrder = table.Column<bool>(type: "bit", nullable: false),
                    openOrderDate = table.Column<DateTime>(type: "date", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK__Order__userId__3F466844",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CakeHasToppings",
                columns: table => new
                {
                    cakeId = table.Column<int>(type: "int", nullable: false),
                    toppingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__CakeHasTo__cakeI__398D8EEE",
                        column: x => x.cakeId,
                        principalTable: "Cake",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__CakeHasTo__toppi__3A81B327",
                        column: x => x.toppingId,
                        principalTable: "Topping",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderHasCakes",
                columns: table => new
                {
                    quantity = table.Column<int>(type: "int", nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    cakeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__OrderHasC__cakeI__4222D4EF",
                        column: x => x.cakeId,
                        principalTable: "Cake",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__OrderHasC__order__412EB0B6",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cake_fillingId",
                table: "Cake",
                column: "fillingId");

            migrationBuilder.CreateIndex(
                name: "IX_Cake_shapeId",
                table: "Cake",
                column: "shapeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cake_sizeId",
                table: "Cake",
                column: "sizeId");

            migrationBuilder.CreateIndex(
                name: "IX_CakeHasToppings_cakeId",
                table: "CakeHasToppings",
                column: "cakeId");

            migrationBuilder.CreateIndex(
                name: "IX_CakeHasToppings_toppingId",
                table: "CakeHasToppings",
                column: "toppingId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHasCakes_cakeId",
                table: "OrderHasCakes",
                column: "cakeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHasCakes_orderId",
                table: "OrderHasCakes",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E6164BF58D47A",
                table: "User",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CakeHasToppings");

            migrationBuilder.DropTable(
                name: "OrderHasCakes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Topping");

            migrationBuilder.DropTable(
                name: "Cake");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Filling");

            migrationBuilder.DropTable(
                name: "Shape");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
