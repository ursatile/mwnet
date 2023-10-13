using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rockaway.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateShows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeadlinerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoorsOpen = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShowBegins = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShowEnds = table.Column<TimeSpan>(type: "time", nullable: false),
                    SalesBegin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => new { x.VenueId, x.Date });
                    table.ForeignKey(
                        name: "FK_Shows_Artists_HeadlinerId",
                        column: x => x.HeadlinerId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shows_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportSlot",
                columns: table => new
                {
                    SlotNumber = table.Column<int>(type: "int", nullable: false),
                    ShowVenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportSlot", x => new { x.ShowVenueId, x.ShowDate, x.SlotNumber });
                    table.ForeignKey(
                        name: "FK_SupportSlot_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportSlot_Shows_ShowVenueId_ShowDate",
                        columns: x => new { x.ShowVenueId, x.ShowDate },
                        principalTable: "Shows",
                        principalColumns: new[] { "VenueId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowVenueId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowDate1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Shows_ShowVenueId1_ShowDate1",
                        columns: x => new { x.ShowVenueId1, x.ShowDate1 },
                        principalTable: "Shows",
                        principalColumns: new[] { "VenueId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-ccccccccccc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9a8eef2-340a-497f-95e3-fe5f781b95dc", "AQAAAAIAAYagAAAAEIdEh/Tjes+um7V4geLo5d4BURJ/kvK8vDl0wBatPrGhqTe7AlZR6cNNynq4xC1mkg==", "c2c5ce8f-310c-47e1-9343-af35d12ee2e5" });

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "Date", "VenueId", "DoorsOpen", "HeadlinerId", "SalesBegin", "ShowBegins", "ShowEnds" },
                values: new object[,]
                {
                    { new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), new TimeSpan(0, 18, 0, 0, 0), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 23, 0, 0, 0) },
                    { new DateTime(2023, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), new TimeSpan(0, 18, 0, 0, 0), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 23, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shows_HeadlinerId",
                table: "Shows",
                column: "HeadlinerId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportSlot_ArtistId",
                table: "SupportSlot",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ShowVenueId1_ShowDate1",
                table: "Ticket",
                columns: new[] { "ShowVenueId1", "ShowDate1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportSlot");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-ccccccccccc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "127273e7-97c3-405d-aef0-4934e68fa951", "AQAAAAIAAYagAAAAEHEyrn2rilT2YQzV1MBhRgNFA8QWIAdiWnzEp4mGXHnpUEzie9ZTgKNK5Y2QjQoKLQ==", "308224a6-38ba-4a51-a185-4e764264fd9e" });
        }
    }
}
