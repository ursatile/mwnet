using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class AddSlugToArtist : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<string>(
				name: "Slug",
				table: "Artists",
				type: "varchar(100)",
				unicode: false,
				maxLength: 100,
				nullable: false,
				defaultValue: "");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10"),
				column: "Slug",
				value: "javas-crypt");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa11"),
				column: "Slug",
				value: "killer-bite");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa12"),
				column: "Slug",
				value: "lambda-of-god");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa13"),
				column: "Slug",
				value: "null-terminated-string-band");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa14"),
				column: "Slug",
				value: "mott-the-tuple");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa15"),
				column: "Slug",
				value: "overflow");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa16"),
				column: "Slug",
				value: "pascals-wager");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa17"),
				column: "Slug",
				value: "quantum-gate");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa18"),
				column: "Slug",
				value: "run-cmd");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa19"),
				column: "Slug",
				value: "script-kiddies");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa20"),
				column: "Slug",
				value: "terrorform");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa21"),
				column: "Slug",
				value: "unicoder");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa22"),
				column: "Slug",
				value: "virtual-machine");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa23"),
				column: "Slug",
				value: "webmaster-of-puppets");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa24"),
				column: "Slug",
				value: "xslte");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa25"),
				column: "Slug",
				value: "yamb");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa26"),
				column: "Slug",
				value: "zero-based-index");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa27"),
				column: "Slug",
				value: "aerbaarn");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
				column: "Slug",
				value: "alter-column");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
				column: "Slug",
				value: "body-bag");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
				column: "Slug",
				value: "coda");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
				column: "Slug",
				value: "dev-leppard");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"),
				column: "Slug",
				value: "elektronika");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa6"),
				column: "Slug",
				value: "for-ear-transform");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa7"),
				column: "Slug",
				value: "garbage-collectors");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa8"),
				column: "Slug",
				value: "haskells-angels");

			migrationBuilder.UpdateData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa9"),
				column: "Slug",
				value: "iron-median");

			migrationBuilder.CreateIndex(
				name: "IX_Artists_Slug",
				table: "Artists",
				column: "Slug",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropIndex(
				name: "IX_Artists_Slug",
				table: "Artists");

			migrationBuilder.DropColumn(
				name: "Slug",
				table: "Artists");
		}
	}
}