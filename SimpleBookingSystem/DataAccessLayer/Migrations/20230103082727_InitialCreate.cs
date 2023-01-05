using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleBookingSystem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "resource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "booking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    BookedQuantity = table.Column<int>(nullable: false),
                    ResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_booking_resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "resource",
                columns: new[] { "Id", "Name", "Quantity" },
                values: new object[] { 1, "Resource1", 5 });

            migrationBuilder.InsertData(
                table: "resource",
                columns: new[] { "Id", "Name", "Quantity" },
                values: new object[] { 2, "Resource2", 10 });

            migrationBuilder.InsertData(
                table: "resource",
                columns: new[] { "Id", "Name", "Quantity" },
                values: new object[] { 3, "Resource3", 15 });

            migrationBuilder.InsertData(
                table: "booking",
                columns: new[] { "Id", "BookedQuantity", "DateFrom", "DateTo", "ResourceId" },
                values: new object[] { 1, 2, new DateTime(2023, 1, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 3, 12, 45, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "booking",
                columns: new[] { "Id", "BookedQuantity", "DateFrom", "DateTo", "ResourceId" },
                values: new object[] { 2, 4, new DateTime(2023, 1, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "booking",
                columns: new[] { "Id", "BookedQuantity", "DateFrom", "DateTo", "ResourceId" },
                values: new object[] { 3, 6, new DateTime(2023, 1, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 3, 15, 15, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_booking_ResourceId",
                table: "booking",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking");

            migrationBuilder.DropTable(
                name: "resource");
        }
    }
}
