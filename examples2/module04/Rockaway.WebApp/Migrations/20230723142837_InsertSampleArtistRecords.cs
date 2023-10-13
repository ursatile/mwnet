using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class InsertSampleArtistRecords : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.Sql(@"
				INSERT INTO Artists(Id, Name, Description) VALUES('00000001-0000-0000-0000-AAAAAAAAAAAA', 'Alter Column', 'Alter Column are South Africa''s hottest math rock export. Founded in Cape Town in 2021, their debut album """"Drop Table Mountain"""" was nominated for four Grammy awards.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000002-0000-0000-0000-AAAAAAAAAAAA', 'Binary Search', 'Speed metal pioneers from San Francisco, Binary Search helped define the data rock sound in the late 1990s.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000003-0000-0000-0000-AAAAAAAAAAAA', 'C0DA', 'Hailing from a distant future, C0DA is a time-traveling rock band known for their mind-bending melodies that transport audiences through different eras, merging past and future into a harmonious blend of sound.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000004-0000-0000-0000-AAAAAAAAAAAA', 'Dev Leppard', 'Rising from the ashes of adversity, Dev Leppard is a fiercely talented rock band that overcame obstacles with sheer determination, captivating fans worldwide with their electrifying performances and showcasing a bond that empowers their music.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000005-0000-0000-0000-AAAAAAAAAAAA', 'Erlangst', 'Merging the realms of art and emotion, Erlangst is an introspective rock group, infusing their hauntingly beautiful lyrics with mesmerizing melodies that delve into the depths of human existence, leaving listeners immersed in profound reflections.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000006-0000-0000-0000-AAAAAAAAAAAA', 'Floating Point Foxes', 'With an otherworldly allure, Floating Point Foxes is an ethereal rock ensemble, their music transcends reality, leading listeners on a dreamlike journey where celestial harmonies and ethereal instrumentation create a captivating and transformative experience.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000007-0000-0000-0000-AAAAAAAAAAAA', 'Garbage Collectors', 'Rebel rockers with a cause, Garbage Collectors are raw, raucous and unapologetic, fearlessly tackling social issues and societal norms in their music, energizing crowds with their powerful anthems and unyielding spirit.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000008-0000-0000-0000-AAAAAAAAAAAA', 'Haskell’s Angels', 'Virtuosos of rhythm and harmony, Haskell’s Angels radiate a divine aura, blending complex melodies and celestial harmonies that resonate deep within the soul.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000009-0000-0000-0000-AAAAAAAAAAAA', 'Iron Median', 'A force to be reckoned with, Iron Median are known for their thunderous beats and adrenaline-pumping anthems, electrifying audiences with their commanding stage presence and unstoppable energy.');
				INSERT INTO Artists(Id, Name, Description) VALUES('0000000A-0000-0000-0000-AAAAAAAAAAAA', 'Java’s Crypt', 'Enigmatic and mysterious, Java’s Crypt are shrouded in secrecy, their enigmatic melodies and cryptic lyrics take listeners on a thrilling journey through the unknown realms of music.');
				INSERT INTO Artists(Id, Name, Description) VALUES('0000000B-0000-0000-0000-AAAAAAAAAAAA', 'Killerbyte', 'An electrifying whirlwind, Killerbyte unleash a torrent of energy through their performances, captivating audiences with their explosive riffs and heart-pounding rhythms.');
				INSERT INTO Artists(Id, Name, Description) VALUES('0000000C-0000-0000-0000-AAAAAAAAAAAA', 'Lambda of God', 'Pioneers of progressive rock, Lambda of God is an innovative band that pushes the boundaries of musical expression, combining intricate compositions and thought-provoking lyrics that resonate deeply with their dedicated fanbase.');
				INSERT INTO Artists(Id, Name, Description) VALUES('0000000D-0000-0000-0000-AAAAAAAAAAAA', 'Null Terminated String Band', 'Quirky and witty, the Null Terminated String Band is a rock group that weaves clever humor and geeky references into their catchy tunes, bringing a smile to the faces of both tech enthusiasts and music lovers alike.');
				INSERT INTO Artists(Id, Name, Description) VALUES('0000000E-0000-0000-0000-AAAAAAAAAAAA', 'Mott the Tuple', 'A charismatic ensemble, Mott the Tuple blends vintage charm with a modern edge, creating a unique sound that captivates audiences and takes them on a nostalgic journey through time.');
				INSERT INTO Artists(Id, Name, Description) VALUES('0000000F-0000-0000-0000-AAAAAAAAAAAA', 'Överflow', 'Overflowing with passion and intensity, Överflow is a rock band that immerses listeners in a tsunami of sound, exploring emotions through powerful melodies and soul-stirring performances.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000010-0000-0000-0000-AAAAAAAAAAAA', 'Pascal’s Wager', 'Philosophers of rock, Pascal’s Wager delves into existential themes with their intellectually charged songs, prompting listeners to ponder the profound questions of life and purpose.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000011-0000-0000-0000-AAAAAAAAAAAA', 'Qüantum Gäte', 'Futuristic and avant-garde, Qüantum Gäte defy conventions, using experimental sounds and innovative compositions to transport listeners to other dimensions of music.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000012-0000-0000-0000-AAAAAAAAAAAA', 'Run CMD', 'High-energy and rebellious, Run CMD is a rock band that merges technology themes with headbanging-worthy tunes, igniting the stage with their electrifying presence and infectious enthusiasm.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000013-0000-0000-0000-AAAAAAAAAAAA', 'Script Kiddies', 'Mischievous and bold, Script Kiddies subvert expectations with clever musical pranks, weaving clever wordplay and tongue-in-cheek humor into their audacious performances.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000014-0000-0000-0000-AAAAAAAAAAAA', 'Terrorform', 'Masters of atmosphere, Terrorform’s dark and atmospheric rock ensembles conjure hauntingly beautiful soundscapes that captivate the senses and evoke a deep emotional response.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000015-0000-0000-0000-AAAAAAAAAAAA', 'Unsigned Integer', 'Unsigned Integer harmonize complex equations and melodies, weaving a symphony of logic and emotion in their unique and captivating music.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000016-0000-0000-0000-AAAAAAAAAAAA', 'Virtual Machine', 'Bridging reality and virtuality, Virtual Machine is a surreal rock group that blurs the lines between the tangible and the digital, creating mind-bending performances that leave audiences questioning the nature of existence.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000017-0000-0000-0000-AAAAAAAAAAAA', 'Webmaster of Puppets', 'Technologically savvy and creatively ambitious, Webmaster of Puppets is a web-inspired rock band, crafting narratives of digital dominance and manipulation with their inventive music.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000018-0000-0000-0000-AAAAAAAAAAAA', 'XSLTE', 'Mesmerizing and genre-defying, XSLTE is an enchanting rock ensemble that fuses electronic and rock elements, creating a spellbinding sound that enthralls listeners and transports them to new sonic landscapes.');
				INSERT INTO Artists(Id, Name, Description) VALUES('00000019-0000-0000-0000-AAAAAAAAAAAA', 'YAMB', 'Youthful and exuberant, YAMB spreads positivity and infectious energy through their music, connecting with fans through their youthful spirit and heartwarming performances.');
				INSERT INTO Artists(Id, Name, Description) VALUES('0000001A-0000-0000-0000-AAAAAAAAAAAA', 'Zero Based Index', 'Innovative and exploratory, Zero Based Index starts from scratch, building powerful narratives through their dynamic sound, leaving audiences inspired and moved by their expressive and daring music.');
			");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {

		}
	}
}