using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class PopulateSampleArtistData : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.InsertData(
				table: "Artists",
				columns: new[] { "Id", "Description", "Name" },
				values: new object[,] {
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10"), "Enigmatic and mysterious, Java’s Crypt are shrouded in secrecy, their enigmatic melodies and cryptic lyrics take listeners on a thrilling journey through the unknown realms of music.", "Java’s Crypt" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa11"), "An electrifying whirlwind, Killer Bite unleash a torrent of energy through their performances, captivating audiences with their explosive riffs and heart-pounding rhythms.", "Killer Bite" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa12"), "Pioneers of progressive rock, Lambda of God is an innovative band that pushes the boundaries of musical expression, combining intricate compositions and thought-provoking lyrics that resonate deeply with their dedicated fanbase.", "Lambda of God" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa13"), "Quirky and witty, the Null Terminated String Band is a rock group that weaves clever humor and geeky references into their catchy tunes, bringing a smile to the faces of both tech enthusiasts and music lovers alike.", "Null Terminated String Band" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa14"), "A charismatic ensemble, Mott the Tuple blends vintage charm with a modern edge, creating a unique sound that captivates audiences and takes them on a nostalgic journey through time.", "Mott the Tuple" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa15"), "Overflowing with passion and intensity, Överflow is a rock band that immerses listeners in a tsunami of sound, exploring emotions through powerful melodies and soul-stirring performances.", "Överflow" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa16"), "Philosophers of rock, Pascal’s Wager delves into existential themes with their intellectually charged songs, prompting listeners to ponder the profound questions of life and purpose.", "Pascal’s Wager" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa17"), "Futuristic and avant-garde, Qüantum Gäte defy conventions, using experimental sounds and innovative compositions to transport listeners to other dimensions of music.", "Qüantum Gäte" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa18"), "High-energy and rebellious, Run CMD is a rock band that merges technology themes with headbanging-worthy tunes, igniting the stage with their electrifying presence and infectious enthusiasm.", "Run CMD" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa19"), "Mischievous and bold, <Script>Kiddies subvert expectations with clever musical pranks, weaving clever wordplay and tongue-in-cheek humor into their audacious performances.", "<Script>Kiddies" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa20"), "Masters of atmosphere, Terrorform’s dark and atmospheric rock ensembles conjure hauntingly beautiful soundscapes that captivate the senses and evoke a deep emotional response.", "Terrorform" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa21"), "ᵾnɨȼøđɇɍ harmonize complex equations and melodies, weaving a symphony of logic and emotion in their unique and captivating music.", "ᵾnɨȼøđɇɍ" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa22"), "Bridging reality and virtuality, Virtual Machine is a surreal rock group that blurs the lines between the tangible and the digital, creating mind-bending performances that leave audiences questioning the nature of existence.", "Virtual Machine" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa23"), "Technologically savvy and creatively ambitious, Webmaster of Puppets is a web-inspired rock band, crafting narratives of digital dominance and manipulation with their inventive music.", "Webmaster of Puppets" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa24"), "Mesmerizing and genre-defying, XSLTE is an enchanting rock ensemble that fuses electronic and rock elements, creating a spellbinding sound that enthralls listeners and transports them to MakeArtist sonic landscapes.", "XSLTE" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa25"), "Youthful and exuberant, YAMB spreads positivity and infectious energy through their music, connecting with fans through their youthful spirit and heartwarming performances.", "YAMB" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa26"), "Innovative and exploratory, Zero Based Index starts from scratch, building powerful narratives through their dynamic sound, leaving audiences inspired and moved by their expressive and daring music.", "Zero Based Index" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa27"), "Inspired by their Australian namesakes, Ærbårn are Scandinavia's #1 party rock band. Thundering drums, huge guitar riffs and enough energy to light up the Arctic Circle, their shows have had amazing reviews all over the world", "Ærbårn" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "Alter Column are South Africa's hottest math rock export. Founded in Cape Town in 2021, their debut album \"Drop Table Mountain\" was nominated for four Grammy awards.", "Alter Column" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "Speed metal pioneers from San Francisco, <Body>Bag helped define the “web rock” sound in the early 2020s.", "<Body>Bag" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), "Hailing from a distant future, Coda is a time-traveling rock band known for their mind-bending melodies that transport audiences through different eras, merging past and future into a harmonious blend of sound.", "Coda" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), "Rising from the ashes of adversity, Dev Leppard is a fiercely talented rock band that overcame obstacles with sheer determination, captivating fans worldwide with their electrifying performances and showcasing a bond that empowers their music.", "Dev Leppard" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"), "Merging the realms of art and emotion, Электроника is an introspective rock group, infusing their hauntingly beautiful lyrics with mesmerizing melodies that delve into the depths of human existence, leaving listeners immersed in profound reflections.", "Электроника" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa6"), "With an otherworldly allure, For Ear Transform is an ethereal rock ensemble, their music transcends reality, leading listeners on a dreamlike journey where celestial harmonies and ethereal instrumentation create a captivating and transformative experience.", "For Ear Transform" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa7"), "Rebel rockers with a cause, Garbage Collectors are raw, raucous and unapologetic, fearlessly tackling social issues and societal norms in their music, energizing crowds with their powerful anthems and unyielding spirit.", "Garbage Collectors" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa8"), "Virtuosos of rhythm and harmony, Haskell’s Angels radiate a divine aura, blending complex melodies and celestial harmonies that resonate deep within the soul.", "Haskell’s Angels" },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa9"), "A force to be reckoned with, Iron Median are known for their thunderous beats and adrenaline-pumping anthems, electrifying audiences with their commanding stage presence and unstoppable energy.", "Iron Median" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa11"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa12"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa13"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa14"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa15"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa16"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa17"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa18"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa19"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa20"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa21"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa22"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa23"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa24"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa25"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa26"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa27"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa6"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa7"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa8"));

			migrationBuilder.DeleteData(
				table: "Artists",
				keyColumn: "Id",
				keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa9"));
		}
	}
}