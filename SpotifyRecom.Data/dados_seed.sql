use db_spotify_recomendacoes;
START TRANSACTION;

-- 1. Inserir Gêneros
INSERT INTO `Generos` (`IdGenero`, `Nome`) VALUES
(1, 'Rock'),
(2, 'Pop'),
(3, 'MPB'),
(4, 'Heavy Metal'),
(5, 'Jazz');

-- 2. Inserir Artistas
INSERT INTO `Artistas` (`IdArtista`, `Nome`) VALUES
(1, 'Queen'),
(2, 'Michael Jackson'),
(3, 'Caetano Veloso'),
(4, 'Iron Maiden'),
(5, 'Miles Davis');

-- 3. Vincular Artistas aos Gêneros (Tabela ArtistaGenero)
INSERT INTO `ArtistaGenero` (`ArtistasIdArtista`, `GenerosArtistaIdGenero`) VALUES
(1, 1), -- Queen -> Rock
(2, 2), -- Michael Jackson -> Pop
(3, 3), -- Caetano Veloso -> MPB
(4, 4), -- Iron Maiden -> Heavy Metal
(5, 5); -- Miles Davis -> Jazz

-- 4. Inserir Álbuns (Campos: Nome, ArtistaId, MidiaId, AnoLancamento)
-- Obs: O campo MidiaId na tabela Albums reflete a estrutura do banco original.
INSERT INTO `Albums` (`IdAlbum`, `Nome`, `ArtistaId`, `MidiaId`, `AnoLancamento`) VALUES
-- Queen (Artista 1)
(1, 'A Night at the Opera', 1, 0, 1975),
(2, 'News of the World', 1, 0, 1977),
(3, 'The Game', 1, 0, 1980),

-- Michael Jackson (Artista 2)
(4, 'Off the Wall', 2, 0, 1979),
(5, 'Thriller', 2, 0, 1982),
(6, 'Bad', 2, 0, 1987),

-- Caetano Veloso (Artista 3)
(7, 'Transa', 3, 0, 1972),
(8, 'Bicho', 3, 0, 1977),
(9, 'Cinema Transcendental', 3, 0, 1979),

-- Iron Maiden (Artista 4)
(10, 'The Number of the Beast', 4, 0, 1982),
(11, 'Powerslave', 4, 0, 1984),
(12, 'Fear of the Dark', 4, 0, 1992),

-- Miles Davis (Artista 5)
(13, 'Kind of Blue', 5, 0, 1959),
(14, 'Bitches Brew', 5, 0, 1970),
(15, 'Sketches of Spain', 5, 0, 1960);

-- 5. Vincular Álbuns aos Artistas (Tabela AlbumArtista)
INSERT INTO `AlbumArtista` (`AlbunsIdAlbum`, `ArtistasIdArtista`) VALUES
(1, 1), (2, 1), (3, 1),
(4, 2), (5, 2), (6, 2),
(7, 3), (8, 3), (9, 3),
(10, 4), (11, 4), (12, 4),
(13, 5), (14, 5), (15, 5);

-- 6. Inserir Mídias / Músicas (Campos: Titulo, ArtistaId, AlbumId, Duracao)
INSERT INTO `Midias` (`IdMidia`, `Titulo`, `ArtistaId`, `AlbumId`, `Duracao`) VALUES
-- Queen - A Night at the Opera (Album 1)
(1, 'Death on Two Legs', 1, 1, '00:03:43'),
(2, 'Love of My Life', 1, 1, '00:03:39'),
(3, 'Bohemian Rhapsody', 1, 1, '00:05:55'),

-- Queen - News of the World (Album 2)
(4, 'We Will Rock You', 1, 2, '00:02:01'),
(5, 'We Are the Champions', 1, 2, '00:02:59'),
(6, 'Spread Your Wings', 1, 2, '00:04:34'),

-- Queen - The Game (Album 3)
(7, 'Another One Bites the Dust', 1, 3, '00:03:35'),
(8, 'Crazy Little Thing Called Love', 1, 3, '00:02:42'),
(9, 'Save Me', 1, 3, '00:03:48'),

-- Michael Jackson - Off the Wall (Album 4)
(10, 'Don\'t Stop \'Til You Get Enough', 2, 4, '00:06:05'),
(11, 'Rock with You', 2, 4, '00:03:40'),
(12, 'Off the Wall', 2, 4, '00:04:06'),

-- Michael Jackson - Thriller (Album 5)
(13, 'Thriller', 2, 5, '00:05:57'),
(14, 'Beat It', 2, 5, '00:04:18'),
(15, 'Billie Jean', 2, 5, '00:04:54'),

-- Michael Jackson - Bad (Album 6)
(16, 'Bad', 2, 6, '00:04:07'),
(17, 'The Way You Make Me Feel', 2, 6, '00:04:57'),
(18, 'Smooth Criminal', 2, 6, '00:04:17'),

-- Caetano Veloso - Transa (Album 7)
(19, 'You Don\'t Know Me', 3, 7, '00:03:50'),
(20, 'Nine Out of Ten', 3, 7, '00:04:57'),
(21, 'Triste Bahia', 3, 7, '00:09:50'),

-- Caetano Veloso - Bicho (Album 8)
(22, 'O Leãozinho', 3, 8, '00:03:00'),
(23, 'Tigresa', 3, 8, '00:06:00'),
(24, 'Gente', 3, 8, '00:03:35'),

-- Caetano Veloso - Cinema Transcendental (Album 9)
(25, 'Lua de São Jorge', 3, 9, '00:03:57'),
(26, 'Oração ao Tempo', 3, 9, '00:03:25'),
(27, 'Beleza Pura', 3, 9, '00:03:30'),

-- Iron Maiden - The Number of the Beast (Album 10)
(28, 'Run to the Hills', 4, 10, '00:03:53'),
(29, 'The Number of the Beast', 4, 10, '00:04:51'),
(30, 'Hallowed Be Thy Name', 4, 10, '00:07:11'),

-- Iron Maiden - Powerslave (Album 11)
(31, 'Aces High', 4, 11, '00:04:29'),
(32, '2 Minutes to Midnight', 4, 11, '00:06:00'),
(33, 'Powerslave', 4, 11, '00:06:48'),

-- Iron Maiden - Fear of the Dark (Album 12)
(34, 'Be Quick or Be Dead', 4, 12, '00:03:24'),
(35, 'Afraid to Shoot Strangers', 4, 12, '00:06:56'),
(36, 'Fear of the Dark', 4, 12, '00:07:16'),

-- Miles Davis - Kind of Blue (Album 13)
(37, 'So What', 5, 13, '00:09:22'),
(38, 'Freddie Freeloader', 5, 13, '00:09:49'),
(39, 'Blue in Green', 5, 13, '00:05:37'),

-- Miles Davis - Bitches Brew (Album 14)
(40, 'Pharaoh\'s Dance', 5, 14, '00:20:06'),
(41, 'Bitches Brew', 5, 14, '00:26:58'),
(42, 'Spanish Key', 5, 14, '00:17:32'),

-- Miles Davis - Sketches of Spain (Album 15)
(43, 'Concierto de Aranjuez', 5, 15, '00:16:19'),
(44, 'Will O\' the Wisp', 5, 15, '00:03:47'),
(45, 'The Pan Piper', 5, 15, '00:03:52');

-- 7. Vincular Mídias aos Artistas (Tabela ArtistaMidia)
INSERT INTO `ArtistaMidia` (`ArtistasIdArtista`, `MidiasIdMidia`) VALUES
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 8), (1, 9),
(2, 10), (2, 11), (2, 12), (2, 13), (2, 14), (2, 15), (2, 16), (2, 17), (2, 18),
(3, 19), (3, 20), (3, 21), (3, 22), (3, 23), (3, 24), (3, 25), (3, 26), (3, 27),
(4, 28), (4, 29), (4, 30), (4, 31), (4, 32), (4, 33), (4, 34), (4, 35), (4, 36),
(5, 37), (5, 38), (5, 39), (5, 40), (5, 41), (5, 42), (5, 43), (5, 44), (5, 45);

-- 8. Vincular Mídias aos Gêneros (Tabela GeneroMidia)
INSERT INTO `GeneroMidia` (`GenerosMidiaIdGenero`, `MidiasIdMidia`) VALUES
-- Gênero 1 (Rock) -> Músicas do Queen (1 a 9)
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 8), (1, 9),

-- Gênero 2 (Pop) -> Músicas do Michael Jackson (10 a 18)
(2, 10), (2, 11), (2, 12), (2, 13), (2, 14), (2, 15), (2, 16), (2, 17), (2, 18),

-- Gênero 3 (MPB) -> Músicas do Caetano Veloso (19 a 27)
(3, 19), (3, 20), (3, 21), (3, 22), (3, 23), (3, 24), (3, 25), (3, 26), (3, 27),

-- Gênero 4 (Heavy Metal) -> Músicas do Iron Maiden (28 a 36)
(4, 28), (4, 29), (4, 30), (4, 31), (4, 32), (4, 33), (4, 34), (4, 35), (4, 36),

-- Gênero 5 (Jazz) -> Músicas do Miles Davis (37 a 45)
(5, 37), (5, 38), (5, 39), (5, 40), (5, 41), (5, 42), (5, 43), (5, 44), (5, 45);

COMMIT;

INSERT INTO planos (IdPlano, Descricao, Valor) VALUES
	(1, "Gratuito", 00),
    (2, "Premium", 19.99),
    (3, "Premium família", 39.99);