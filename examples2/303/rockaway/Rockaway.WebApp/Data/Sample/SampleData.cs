// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Rockaway.WebApp.Data.Sample;

public class SampleData {

	private static Guid TestGuid(int seed, char pad) => new(seed.ToString().PadLeft(32, pad));

	public static class Venues {
		public static Venue Astoria = new(TestGuid(1, 'b'), "The Astoria", "157 Charing Cross Road", "London", "GB", "WC2H 0EL", "020 7412 3400", "https://www.astoria.co.uk");
		public static Venue Bataclan = new(TestGuid(2, 'b'), "Bataclan", "50 Boulevard Voltaire", "Paris", "FR", "75011", "+33 1 43 14 00 30", "https://www.bataclan.fr/");
		public static Venue Columbia = new(TestGuid(3, 'b'), "Columbia Theatre", "Columbiadamm 9 - 11", "Berlin", "DE", "10965", "+49 30 69817584", "https://columbia-theater.de/");
		public static Venue Gagarin = new(TestGuid(4, 'b'), "Gagarin 205", "Liosion 205", "Athens", "GR", "104 45", "+45 35 35 50 69", "");
		public static Venue JohnDee = new(TestGuid(5, 'b'), "John Dee Live Club & Pub", "Torggata 16", "Oslo", "NO", "0181", "+47 22 20 32 32", "https://www.rockefeller.no/");
		public static Venue Stengade = new(TestGuid(6, 'b'), "Stengade", "Stengade 18", "Copenhagen", "DK", "2200", "+45 35355069", "https://www.stengade.dk");
		public static Venue Barracuda = new(TestGuid(7, 'b'), "Barracuda", "R da Madeira 186", "Porto", "PT", "400 - 433");
		public static Venue PubAnchor = new(TestGuid(8, 'b'), "Pub Anchor", "Sveavägen 90", "Stockholm", "SE", "113 59", "+46 8 15 20 00", "https://www.instagram.com/pubanchor/?hl=en");
		public static Venue NewCrossInn = new(TestGuid(9, 'b'), "New Cross Inn", "323 New Cross Road", "London", "GB", "SE14 6AS", "+44 20 8469 4382", "https://www.newcrossinn.com/");

		public static Venue[] AllVenues = {
			Astoria, Bataclan, Columbia, Gagarin, JohnDee, Stengade, Barracuda, PubAnchor, NewCrossInn
		};
	}
	public static class Artists {
		public static Artist AlterColumn = new() {
			Id = TestGuid(1, 'a'),
			Name = "Alter Column",
			Description = "Alter Column are South Africa's hottest math rock export. Founded in Cape Town in 2021, their debut album \"Drop Table Mountain\" was nominated for four Grammy awards.",
			Slug = "alter-column"
		};

		public static Artist BodyBag = new() {
			Id = TestGuid(2, 'a'),
			Name = "<Body>Bag",
			Description = "Speed metal pioneers from San Francisco, <Body>Bag helped define the “web rock” sound in the early 2020s.",
			Slug = "body-bag"
		};

		public static Artist Coda = new() {
			Id = TestGuid(3, 'a'),
			Name = "Coda",
			Description = "Hailing from a distant future, Coda is a time-traveling rock band known for their mind-bending melodies that transport audiences through different eras, merging past and future into a harmonious blend of sound.",
			Slug = "coda"
		};

		public static Artist DevLeppard = new() {
			Id = TestGuid(4, 'a'),
			Name = "Dev Leppard",
			Description = "Rising from the ashes of adversity, Dev Leppard is a fiercely talented rock band that overcame obstacles with sheer determination, captivating fans worldwide with their electrifying performances and showcasing a bond that empowers their music.",
			Slug = "dev-leppard"
		};

		public static Artist Elektronika = new() {
			Id = TestGuid(5, 'a'),
			Name = "Электроника",
			Description = "Merging the realms of art and emotion, Электроника is an introspective rock group, infusing their hauntingly beautiful lyrics with mesmerizing melodies that delve into the depths of human existence, leaving listeners immersed in profound reflections.",
			Slug = "elektronika"
		};

		public static Artist ForEarTransform = new() {
			Id = TestGuid(6, 'a'),
			Name = "For Ear Transform",
			Description = "With an otherworldly allure, For Ear Transform is an ethereal rock ensemble, their music transcends reality, leading listeners on a dreamlike journey where celestial harmonies and ethereal instrumentation create a captivating and transformative experience.",
			Slug = "for-ear-transform"
		};

		public static Artist GarbageCollectors = new() {
			Id = TestGuid(7, 'a'),
			Name = "Garbage Collectors",
			Description = "Rebel rockers with a cause, Garbage Collectors are raw, raucous and unapologetic, fearlessly tackling social issues and societal norms in their music, energizing crowds with their powerful anthems and unyielding spirit.",
			Slug = "garbage-collectors"
		};

		public static Artist HaskellsAngels = new() {
			Id = TestGuid(8, 'a'),
			Name = "Haskell’s Angels",
			Description = "Virtuosos of rhythm and harmony, Haskell’s Angels radiate a divine aura, blending complex melodies and celestial harmonies that resonate deep within the soul.",
			Slug = "haskells-angels"
		};

		public static Artist IronMedian = new() {
			Id = TestGuid(9, 'a'),
			Name = "Iron Median",
			Description = "A force to be reckoned with, Iron Median are known for their thunderous beats and adrenaline-pumping anthems, electrifying audiences with their commanding stage presence and unstoppable energy.",
			Slug = "iron-median",
		};

		public static Artist JavasCrypt = new() {
			Id = TestGuid(10, 'a'),
			Name = "Java’s Crypt",
			Description = "Enigmatic and mysterious, Java’s Crypt are shrouded in secrecy, their enigmatic melodies and cryptic lyrics take listeners on a thrilling journey through the unknown realms of music.",
			Slug = "javas-crypt"
		};

		public static Artist KillerBite = new() {
			Id = TestGuid(11, 'a'),
			Name = "Killer Bite",
			Description = "An electrifying whirlwind, Killer Bite unleash a torrent of energy through their performances, captivating audiences with their explosive riffs and heart-pounding rhythms.",
			Slug = "killer-bite"
		};

		public static Artist LambdaOfGod = new() {
			Id = TestGuid(12, 'a'),
			Name = "Lambda of God",
			Description = "Pioneers of progressive rock, Lambda of God is an innovative band that pushes the boundaries of musical expression, combining intricate compositions and thought-provoking lyrics that resonate deeply with their dedicated fanbase.",
			Slug = "lambda-of-god"
		};

		public static Artist NullTerminatedStringBand = new() {
			Id = TestGuid(13, 'a'),
			Name = "Null Terminated String Band",
			Description = "Quirky and witty, the Null Terminated String Band is a rock group that weaves clever humor and geeky references into their catchy tunes, bringing a smile to the faces of both tech enthusiasts and music lovers alike.",
			Slug = "null-terminated-string-band"
		};

		public static Artist MottTheTuple = new() {
			Id = TestGuid(14, 'a'),
			Name = "Mott the Tuple",
			Description = "A charismatic ensemble, Mott the Tuple blends vintage charm with a modern edge, creating a unique sound that captivates audiences and takes them on a nostalgic journey through time.",
			Slug = "mott-the-tuple"
		};

		public static Artist Overflow = new() {
			Id = TestGuid(15, 'a'),
			Name = "Överflow",
			Description = "Overflowing with passion and intensity, Överflow is a rock band that immerses listeners in a tsunami of sound, exploring emotions through powerful melodies and soul-stirring performances.",
			Slug = "overflow"
		};

		public static Artist PascalsWager = new() {
			Id = TestGuid(16, 'a'),
			Name = "Pascal’s Wager",
			Description = "Philosophers of rock, Pascal’s Wager delves into existential themes with their intellectually charged songs, prompting listeners to ponder the profound questions of life and purpose.",
			Slug = "pascals-wager"
		};

		public static Artist QuantumGate = new() {
			Id = TestGuid(17, 'a'),
			Name = "Qüantum Gäte",
			Description = "Futuristic and avant-garde, Qüantum Gäte defy conventions, using experimental sounds and innovative compositions to transport listeners to other dimensions of music.",
			Slug = "quantum-gate"
		};

		public static Artist RunCmd = new() {
			Id = TestGuid(18, 'a'),
			Name = "Run CMD",
			Description = "High-energy and rebellious, Run CMD is a rock band that merges technology themes with headbanging-worthy tunes, igniting the stage with their electrifying presence and infectious enthusiasm.",
			Slug = "run-cmd"
		};

		public static Artist ScriptKiddies = new() {
			Id = TestGuid(19, 'a'),
			Name = "<Script>Kiddies",
			Description = "Mischievous and bold, <Script>Kiddies subvert expectations with clever musical pranks, weaving clever wordplay and tongue-in-cheek humor into their audacious performances.",
			Slug = "script-kiddies"
		};

		public static Artist Terrorform = new() {
			Id = TestGuid(20, 'a'),
			Name = "Terrorform",
			Description = "Masters of atmosphere, Terrorform’s dark and atmospheric rock ensembles conjure hauntingly beautiful soundscapes that captivate the senses and evoke a deep emotional response.",
			Slug = "terrorform"
		};

		public static Artist Unicoder = new() {
			Id = TestGuid(21, 'a'),
			Name = "ᵾnɨȼøđɇɍ",
			Description = "ᵾnɨȼøđɇɍ harmonize complex equations and melodies, weaving a symphony of logic and emotion in their unique and captivating music.",
			Slug = "unicoder"
		};

		public static Artist VirtualMachine = new() {
			Id = TestGuid(22, 'a'),
			Name = "Virtual Machine",
			Description = "Bridging reality and virtuality, Virtual Machine is a surreal rock group that blurs the lines between the tangible and the digital, creating mind-bending performances that leave audiences questioning the nature of existence.",
			Slug = "virtual-machine"
		};

		public static Artist WebmasterOfPuppets = new() {
			Id = TestGuid(23, 'a'),
			Name = "Webmaster of Puppets",
			Description = "Technologically savvy and creatively ambitious, Webmaster of Puppets is a web-inspired rock band, crafting narratives of digital dominance and manipulation with their inventive music.",
			Slug = "webmaster-of-puppets"
		};

		public static Artist Xslte = new() {
			Id = TestGuid(24, 'a'),
			Name = "XSLTE",
			Description = "Mesmerizing and genre-defying, XSLTE is an enchanting rock ensemble that fuses electronic and rock elements, creating a spellbinding sound that enthralls listeners and transports them to MakeArtist sonic landscapes.",
			Slug = "xslte"
		};

		public static Artist Yamb = new() {
			Id = TestGuid(25, 'a'),
			Name = "YAMB",
			Description = "Youthful and exuberant, YAMB spreads positivity and infectious energy through their music, connecting with fans through their youthful spirit and heartwarming performances.",
			Slug = "yamb"
		};

		public static Artist ZeroBasedIndex = new() {
			Id = TestGuid(26, 'a'),
			Name = "Zero Based Index",
			Description = "Innovative and exploratory, Zero Based Index starts from scratch, building powerful narratives through their dynamic sound, leaving audiences inspired and moved by their expressive and daring music.",
			Slug = "zero-based-index"
		};

		public static Artist Ærbårn = new() {
			Id = TestGuid(27, 'a'),
			Name = "Ærbårn",
			Description = "Inspired by their Australian namesakes, Ærbårn are Scandinavia's #1 party rock band. Thundering drums, huge guitar riffs and enough energy to light up the Arctic Circle, their shows have had amazing reviews all over the world",
			Slug = "aerbaarn"
		};

		public static Artist[] AllArtists = {
			AlterColumn, BodyBag, Coda, DevLeppard, Elektronika, ForEarTransform,
			GarbageCollectors, HaskellsAngels, IronMedian, JavasCrypt, KillerBite,
			LambdaOfGod, MottTheTuple, NullTerminatedStringBand, Overflow, PascalsWager,
			QuantumGate, RunCmd, ScriptKiddies, Terrorform, Unicoder,
			VirtualMachine, WebmasterOfPuppets, Xslte, Yamb, ZeroBasedIndex,
			Ærbårn,
		};
	}
}