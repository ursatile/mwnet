using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class AddShowsAndTickets : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "CountryCode",
				table: "Venue");

			migrationBuilder.AddColumn<string>(
				name: "CultureName",
				table: "Venue",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.CreateTable(
				name: "Show",
				columns: table => new {
					Date = table.Column<DateOnly>(type: "date", nullable: false),
					VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					HeadlineArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Show", x => new { x.VenueId, x.Date });
					table.ForeignKey(
						name: "FK_Show_Artist_HeadlineArtistId",
						column: x => x.HeadlineArtistId,
						principalTable: "Artist",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Show_Venue_VenueId",
						column: x => x.VenueId,
						principalTable: "Venue",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "SupportSlot",
				columns: table => new {
					SlotNumber = table.Column<int>(type: "int", nullable: false),
					ShowVenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ShowDate = table.Column<DateOnly>(type: "date", nullable: false),
					ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_SupportSlot", x => new { x.ShowVenueId, x.ShowDate, x.SlotNumber });
					table.ForeignKey(
						name: "FK_SupportSlot_Artist_ArtistId",
						column: x => x.ArtistId,
						principalTable: "Artist",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_SupportSlot_Show_ShowVenueId_ShowDate",
						columns: x => new { x.ShowVenueId, x.ShowDate },
						principalTable: "Show",
						principalColumns: new[] { "VenueId", "Date" },
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "TicketType",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ShowVenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ShowDate = table.Column<DateOnly>(type: "date", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_TicketType", x => x.Id);
					table.ForeignKey(
						name: "FK_TicketType_Show_ShowVenueId_ShowDate",
						columns: x => new { x.ShowVenueId, x.ShowDate },
						principalTable: "Show",
						principalColumns: new[] { "VenueId", "Date" },
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "Artist",
				columns: new[] { "Id", "Description", "Name", "Slug" },
				values: new object[,]
				{
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa28"), "dot-dot-dot to their friends (and fans), ... are a Canadian drone jazz combo formed in Vancouver in 1998, known for their thunderous horn section and innovative visuals.", "...", "dot-dot-dot" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa29"), "Known as the \"Silver Mountain Strings\" if you're into the whole brevity thing, Kentucky's answer to the Leningrad Cowboys has gone from strength to strength, from the Superbowl half time show to their sold-out Vegas residency in 2023.", "The Most Excellent Incredible Electric Silver Mountain String Band featuring Timber J. MacCorkindale and the Kentucky Mountain Hornswogglers", "silver-mountain-string-band" }
				});

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "rockaway-sample-admin-user",
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "4c3dd557-5697-4d01-889f-7aff849a1406", "AQAAAAIAAYagAAAAELbT4lwOtSThY+FvORiWbzGz/D58Fq+2w2GcWIX8qQIrCuIOX9fWYZBjFpmc4owBwg==", "7aa2dd18-c158-4629-90a9-3d6780854d57" });

			migrationBuilder.InsertData(
				table: "Show",
				columns: new[] { "Date", "VenueId", "HeadlineArtistId" },
				values: new object[,]
				{
					{ new DateOnly(2024, 5, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 5, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 5, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 5, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 5, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 5, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 5, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") }
				});

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
				column: "CultureName",
				value: "en-GB");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
				column: "CultureName",
				value: "fr-FR");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
				column: "CultureName",
				value: "de-DE");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
				column: "CultureName",
				value: "el-GR");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"),
				column: "CultureName",
				value: "nn-NO");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb6"),
				column: "CultureName",
				value: "dk-DK");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"),
				column: "CultureName",
				value: "pt-PT");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"),
				column: "CultureName",
				value: "sv-SE");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"),
				column: "CultureName",
				value: "en-GB");

			migrationBuilder.InsertData(
				table: "TicketType",
				columns: new[] { "Id", "Name", "Price", "ShowDate", "ShowVenueId" },
				values: new object[,]
				{
					{ new Guid("0064541a-d96c-40c4-b7b6-2d1ec363d151"), "General Admission", 25m, new DateOnly(2024, 5, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7") },
					{ new Guid("1fc2a9e0-6b89-4577-9fec-d5f3fe807601"), "General Admission", 25m, new DateOnly(2024, 5, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4") },
					{ new Guid("3f899279-10b7-4778-bb6e-67be90ca9322"), "General Admission", 300m, new DateOnly(2024, 5, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8") },
					{ new Guid("4a30cae6-5a19-4dfc-9566-0bb12c0ac74b"), "VIP Meet & Greet", 75m, new DateOnly(2024, 5, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3") },
					{ new Guid("5b2e8663-b943-4fda-b977-cb8543647d77"), "VIP Meet & Greet", 750m, new DateOnly(2024, 5, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5") },
					{ new Guid("7124be0d-2faa-441d-a119-ba344d4b8580"), "VIP Meet & Greet", 720m, new DateOnly(2024, 5, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8") },
					{ new Guid("76f6cf40-0386-431c-935a-aca0b3fe7c88"), "General Admission", 350m, new DateOnly(2024, 5, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5") },
					{ new Guid("7f04da2e-2fa1-4812-b773-c60bdb25e259"), "VIP Meet & Greet", 75m, new DateOnly(2024, 5, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2") },
					{ new Guid("b13def9c-8f6c-4c00-ac7c-64fbef6bdda6"), "General Admission", 35m, new DateOnly(2024, 5, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3") },
					{ new Guid("c84009a8-d003-432f-9dbf-4dc044458557"), "General Admission", 25m, new DateOnly(2024, 5, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9") },
					{ new Guid("c86bc60a-f910-4ba9-b196-b09b837ff602"), "VIP Meet & Greet", 55m, new DateOnly(2024, 5, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9") },
					{ new Guid("ea188990-fa4d-46ca-9fbb-372fcbc8981c"), "General Admission", 35m, new DateOnly(2024, 5, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2") }
				});

			migrationBuilder.CreateIndex(
				name: "IX_Show_HeadlineArtistId",
				table: "Show",
				column: "HeadlineArtistId");

			migrationBuilder.CreateIndex(
				name: "IX_SupportSlot_ArtistId",
				table: "SupportSlot",
				column: "ArtistId");

			migrationBuilder.CreateIndex(
				name: "IX_TicketType_ShowVenueId_ShowDate",
				table: "TicketType",
				columns: new[] { "ShowVenueId", "ShowDate" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "SupportSlot");

			migrationBuilder.DropTable(
				name: "TicketType");

			migrationBuilder.DropTable(
				name: "Show");

			migrationBuilder.DeleteData(
				table: "Artist",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa28"));

			migrationBuilder.DeleteData(
				table: "Artist",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa29"));

			migrationBuilder.DropColumn(
				name: "CultureName",
				table: "Venue");

			migrationBuilder.AddColumn<string>(
				name: "CountryCode",
				table: "Venue",
				type: "varchar(2)",
				unicode: false,
				maxLength: 2,
				nullable: false,
				defaultValue: "");

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "rockaway-sample-admin-user",
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "ec9d7c74-d3f3-4b0e-bac1-9c2a393cc55c", "AQAAAAIAAYagAAAAELzmg82ZfBDgbYxzXE7Ew/hIVsDQ6kXEQE9TAJ3Kz12S/L5Ay9lHXAstreED/up5uw==", "abe08569-b6f1-411d-aa87-fa529538a7c7" });

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
				column: "CountryCode",
				value: "GB");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
				column: "CountryCode",
				value: "FR");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
				column: "CountryCode",
				value: "DE");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
				column: "CountryCode",
				value: "GR");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"),
				column: "CountryCode",
				value: "NO");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb6"),
				column: "CountryCode",
				value: "DK");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"),
				column: "CountryCode",
				value: "PT");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"),
				column: "CountryCode",
				value: "SE");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"),
				column: "CountryCode",
				value: "GB");
		}
	}
}
