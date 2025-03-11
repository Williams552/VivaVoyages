using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VivaVoyages.Migrations
{
    /// <inheritdoc />
    public partial class restore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponCode = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Discount = table.Column<decimal>(type: "money", nullable: false),
                    DateStart = table.Column<DateOnly>(type: "date", nullable: false),
                    DateEnd = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Coupons__D34908015FA41212", x => x.CouponCode);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ResetCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64B8310C889D", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ImagePath = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Places__D5222B4EB2A9451F", x => x.PlaceID);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Staff__96D4AAF74815B8DD", x => x.StaffID);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    TourID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ExpectedProfit = table.Column<decimal>(type: "money", nullable: false),
                    DateStart = table.Column<DateOnly>(type: "date", nullable: false),
                    TourDates = table.Column<int>(type: "int", nullable: false),
                    MaxPasseger = table.Column<int>(type: "int", nullable: false),
                    TourGuide = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    SingleRoomCost = table.Column<decimal>(type: "money", nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CouponCode = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tour__604CEA10B19D8111", x => x.TourID);
                    table.ForeignKey(
                        name: "FK__Tour__CouponCode__2A4B4B5E",
                        column: x => x.CouponCode,
                        principalTable: "Coupons",
                        principalColumn: "CouponCode");
                });

            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    DestinationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceID = table.Column<int>(type: "int", nullable: true),
                    TourID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DateVisit = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Destinat__DB5FE4AC6BFA7178", x => x.DestinationID);
                    table.ForeignKey(
                        name: "FK__Destinati__Place__398D8EEE",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "PlaceID");
                    table.ForeignKey(
                        name: "FK__Destinati__TourI__38996AB5",
                        column: x => x.TourID,
                        principalTable: "Tour",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    StaffID = table.Column<int>(type: "int", nullable: true),
                    TourID = table.Column<int>(type: "int", nullable: false),
                    CouponCode = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BAFF992B534", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__Orders__CouponCo__300424B4",
                        column: x => x.CouponCode,
                        principalTable: "Coupons",
                        principalColumn: "CouponCode");
                    table.ForeignKey(
                        name: "FK__Orders__Customer__2D27B809",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Orders__StaffID__2E1BDC42",
                        column: x => x.StaffID,
                        principalTable: "Staff",
                        principalColumn: "StaffID");
                    table.ForeignKey(
                        name: "FK__Orders__TourID__2F10007B",
                        column: x => x.TourID,
                        principalTable: "Tour",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SingleRoom = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Passenge__88915F908ADF224B", x => x.PassengerID);
                    table.ForeignKey(
                        name: "FK__Passenger__Custo__35BCFE0A",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Passenger__Order__34C8D9D1",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destination_PlaceID",
                table: "Destination",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Destination_TourID",
                table: "Destination",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponCode",
                table: "Orders",
                column: "CouponCode");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StaffID",
                table: "Orders",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TourID",
                table: "Orders",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_CustomerID",
                table: "Passengers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_OrderID",
                table: "Passengers",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_CouponCode",
                table: "Tour",
                column: "CouponCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
