using MySql.Data.MySqlClient;

Plano planoBase = new(1, "Plano Base", 0.00m);
Plano planoPago = new(2, "Plano Pago", 29.99m);
Plano planoPremium = new(3, "Plano Premium", 49.99m);

// Artista Gorillaz = new("Gorillaz", "Eletrônica");
// Artista BMTH = new("Bring me the Horizon", "Heavy metal");
// Artista VicELeo = new("Victor e Leo", "Sertanejo");

// // --- Músicas do Gorillaz ---
// Midia musicaGorillaz1 = new Midia("Feel Good Inc.", Gorillaz, TimeSpan.FromMinutes(3.43), Gorillaz.Genero);
// Midia musicaGorillaz2 = new Midia("Clint Eastwood", Gorillaz, TimeSpan.FromMinutes(5.40), Gorillaz.Genero);
// Midia musicaGorillaz3 = new Midia("On Melancholy Hill", Gorillaz, TimeSpan.FromMinutes(3.53), Gorillaz.Genero);

// // --- Músicas do Bring me the Horizon ---
// Midia musicaBmth1 = new Midia("Throne", BMTH, TimeSpan.FromMinutes(3.11), BMTH.Genero);
// Midia musicaBmth2 = new Midia("Can You Feel My Heart", BMTH, TimeSpan.FromMinutes(3.47), BMTH.Genero);
// Midia musicaBmth3 = new Midia("Drown", BMTH, TimeSpan.FromMinutes(3.42), BMTH.Genero);

// // --- Músicas do Victor e Leo ---
// Midia musicaVicELeo1 = new Midia("Borboletas", VicELeo, TimeSpan.FromMinutes(3.18), VicELeo.Genero);
// Midia musicaVicELeo2 = new Midia("Fada", VicELeo, TimeSpan.FromMinutes(4.56), VicELeo.Genero);
// Midia musicaVicELeo3 = new Midia("Deus e Eu no Sertão", VicELeo, TimeSpan.FromMinutes(3.41), VicELeo.Genero);

/*
var queen = new Artista("Queen", "Rock");
var michaelJackson = new Artista("Michael Jackson", "Pop");
var ironMaiden = new Artista("Iron Maiden", "Heavy metal");
var caetanoVeloso = new Artista("Caetano Veloso", "MPB");
var theWeeknd = new Artista("The Weeknd", "Synth-Pop");

var albumANightAtTheOpera = new Album("A Night at the Opera", queen, 1975);
var albumNewsOfTheWorld = new Album("News of the World", queen, 1977);
var albumTheGame = new Album("The Game", queen, 1980);

var albumOffTheWall = new Album("Off the Wall", michaelJackson, 1979);
var albumThriller = new Album("Thriller", michaelJackson, 1982);
var albumBad = new Album("Bad", michaelJackson, 1987);

var albumTheNumberOfTheBeast = new Album("The Number of the Beast", ironMaiden, 1982);
var albumPieceOfMind = new Album("Piece of Mind", ironMaiden, 1983);
var albumPowerslave = new Album("Powerslave", ironMaiden, 1984);

var albumTransa = new Album("Transa", caetanoVeloso, 1972);
var albumBicho = new Album("Bicho", caetanoVeloso, 1977);
var albumVelo = new Album("Velô", caetanoVeloso, 1984);

var albumStarboy = new Album("Starboy", theWeeknd, 2016);
var albumAfterHours = new Album("After Hours", theWeeknd, 2020);
var albumDawnFm = new Album("Dawn FM", theWeeknd, 2022);

#region Queen - Rock
var musica1 = new Midia("Bohemian Rhapsody", queen, albumANightAtTheOpera, new TimeSpan(0, 5, 55), "Rock");
var musica16 = new Midia("Love of My Life", queen, albumANightAtTheOpera, new TimeSpan(0, 3, 39), "Rock");
var musica17 = new Midia("You're My Best Friend", queen, albumANightAtTheOpera, new TimeSpan(0, 2, 52), "Rock");

var musica2 = new Midia("We Will Rock You", queen, albumNewsOfTheWorld, new TimeSpan(0, 2, 1), "Rock");
var musica18 = new Midia("We Are the Champions", queen, albumNewsOfTheWorld, new TimeSpan(0, 2, 59), "Rock");
var musica19 = new Midia("Spread Your Wings", queen, albumNewsOfTheWorld, new TimeSpan(0, 4, 34), "Rock");

var musica3 = new Midia("Another One Bites the Dust", queen, albumTheGame, new TimeSpan(0, 3, 35), "Rock");
var musica20 = new Midia("Crazy Little Thing Called Love", queen, albumTheGame, new TimeSpan(0, 2, 42), "Rock");
var musica21 = new Midia("Save Me", queen, albumTheGame, new TimeSpan(0, 3, 48), "Rock");
#endregion

#region Michael Jackson - Pop
var musica4 = new Midia("Don't Stop 'Til You Get Enough", michaelJackson, albumOffTheWall, new TimeSpan(0, 6, 5), "Pop");
var musica22 = new Midia("Rock with You", michaelJackson, albumOffTheWall, new TimeSpan(0, 3, 40), "Pop");
var musica23 = new Midia("She's Out of My Life", michaelJackson, albumOffTheWall, new TimeSpan(0, 3, 37), "Pop");

var musica5 = new Midia("Billie Jean", michaelJackson, albumThriller, new TimeSpan(0, 4, 54), "Pop");
var musica24 = new Midia("Beat It", michaelJackson, albumThriller, new TimeSpan(0, 4, 18), "Pop");
var musica25 = new Midia("Thriller", michaelJackson, albumThriller, new TimeSpan(0, 5, 57), "Pop");

var musica6 = new Midia("Smooth Criminal", michaelJackson, albumBad, new TimeSpan(0, 4, 17), "Pop");
var musica26 = new Midia("Bad", michaelJackson, albumBad, new TimeSpan(0, 4, 7), "Pop");
var musica27 = new Midia("The Way You Make Me Feel", michaelJackson, albumBad, new TimeSpan(0, 4, 58), "Pop");
#endregion

#region Iron Maiden - Heavy Metal
var musica7 = new Midia("The Number of the Beast", ironMaiden, albumTheNumberOfTheBeast, new TimeSpan(0, 4, 49), "Heavy Metal");
var musica28 = new Midia("Run to the Hills", ironMaiden, albumTheNumberOfTheBeast, new TimeSpan(0, 3, 53), "Heavy Metal");
var musica29 = new Midia("Hallowed Be Thy Name", ironMaiden, albumTheNumberOfTheBeast, new TimeSpan(0, 7, 11), "Heavy Metal");

var musica8 = new Midia("The Trooper", ironMaiden, albumPieceOfMind, new TimeSpan(0, 4, 12), "Heavy Metal");
var musica30 = new Midia("Flight of Icarus", ironMaiden, albumPieceOfMind, new TimeSpan(0, 3, 51), "Heavy Metal");
var musica31 = new Midia("Where Eagles Dare", ironMaiden, albumPieceOfMind, new TimeSpan(0, 6, 10), "Heavy Metal");

var musica9 = new Midia("Aces High", ironMaiden, albumPowerslave, new TimeSpan(0, 4, 29), "Heavy Metal");
var musica32 = new Midia("2 Minutes to Midnight", ironMaiden, albumPowerslave, new TimeSpan(0, 6, 0), "Heavy Metal");
var musica33 = new Midia("Powerslave", ironMaiden, albumPowerslave, new TimeSpan(0, 7, 11), "Heavy Metal");
#endregion

#region Caetano Veloso - MPB
var musica10 = new Midia("You Don't Know Me", caetanoVeloso, albumTransa, new TimeSpan(0, 3, 50), "MPB");
var musica34 = new Midia("Nine Out of Ten", caetanoVeloso, albumTransa, new TimeSpan(0, 4, 55), "MPB");
var musica35 = new Midia("Triste Bahia", caetanoVeloso, albumTransa, new TimeSpan(0, 9, 47), "MPB");

var musica11 = new Midia("Tigresa", caetanoVeloso, albumBicho, new TimeSpan(0, 6, 15), "MPB");
var musica36 = new Midia("Odara", caetanoVeloso, albumBicho, new TimeSpan(0, 7, 14), "MPB");
var musica37 = new Midia("Leãozinho", caetanoVeloso, albumBicho, new TimeSpan(0, 3, 5), "MPB");

var musica12 = new Midia("Podres Poderes", caetanoVeloso, albumVelo, new TimeSpan(0, 4, 36), "MPB");
var musica38 = new Midia("O Quereres", caetanoVeloso, albumVelo, new TimeSpan(0, 4, 40), "MPB");
var musica39 = new Midia("Língua", caetanoVeloso, albumVelo, new TimeSpan(0, 4, 43), "MPB");
#endregion

#region The Weeknd - Synth-Pop
var musica13 = new Midia("Starboy", theWeeknd, albumStarboy, new TimeSpan(0, 3, 50), "Synth-Pop");
var musica40 = new Midia("I Feel It Coming", theWeeknd, albumStarboy, new TimeSpan(0, 4, 29), "Synth-Pop");
var musica41 = new Midia("Die For You", theWeeknd, albumStarboy, new TimeSpan(0, 4, 20), "Synth-Pop");

var musica14 = new Midia("Blinding Lights", theWeeknd, albumAfterHours, new TimeSpan(0, 3, 20), "Synth-Pop");
var musica42 = new Midia("Save Your Tears", theWeeknd, albumAfterHours, new TimeSpan(0, 3, 35), "Synth-Pop");
var musica43 = new Midia("In Your Eyes", theWeeknd, albumAfterHours, new TimeSpan(0, 3, 57), "Synth-Pop");

var musica15 = new Midia("Take My Breath", theWeeknd, albumDawnFm, new TimeSpan(0, 5, 39), "Synth-Pop");
var musica44 = new Midia("Sacrifice", theWeeknd, albumDawnFm, new TimeSpan(0, 3, 30), "Synth-Pop");
var musica45 = new Midia("Out of Time", theWeeknd, albumDawnFm, new TimeSpan(0, 3, 34), "Synth-Pop");
#endregion
*/


//Usuario usuarioTeste = new("Ka", "k@", "123", planoPago);

//usuarioTeste.CriaPlaylist("Minha Playlist");

Menu menu = new Menu();
menu.Iniciar();