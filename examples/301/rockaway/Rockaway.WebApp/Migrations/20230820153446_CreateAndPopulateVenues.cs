using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class CreateAndPopulateVenues : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Venues",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
					City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					CountryCode = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
					PostalCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
					WebsiteUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					Telephone = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Venues", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "Venues",
				columns: new[] { "Id", "Address", "City", "CountryCode", "Name", "PostalCode", "Telephone", "WebsiteUrl" },
				values: new object[,]
				{
					{ new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), "157 Charing Cross Road", "London", "GB", "The Astoria", "WC2H 0EL", "020 7412 3400", "https://www.astoria.co.uk" },
					{ new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), "50 Boulevard Voltaire", "Paris", "FR", "Bataclan", "75011", "+33 1 43 14 00 30", "https://www.bataclan.fr/" },
					{ new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"), "Columbiadamm 9 - 11", "Berlin", "DE", "Columbia Theatre", "10965", "+49 30 69817584", "https://columbia-theater.de/" },
					{ new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"), "Liosion 205", "Athens", "GR", "Gagarin 205", "104 45", "+45 35 35 50 69", "" },
					{ new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"), "Torggata 16", "Oslo", "NO", "John Dee Live Club & Pub", "0181", "+47 22 20 32 32", "https://www.rockefeller.no/" },
					{ new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb6"), "Stengade 18", "Copenhagen", "DK", "Stengade", "2200", "+45 35355069", "https://www.stengade.dk" },
					{ new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"), "R da Madeira 186", "Porto", "PT", "Barracuda", "400 - 433", null, null },
					{ new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"), "Sveavägen 90", "Stockholm", "SE", "Pub Anchor", "113 59", "+46 8 15 20 00", "https://www.instagram.com/pubanchor/?hl=en" },
					{ new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"), "323 New Cross Road", "London", "GB", "New Cross Inn", "SE14 6AS", "+44 20 8469 4382", "https://www.newcrossinn.com/" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Venues");
		}
	}
}