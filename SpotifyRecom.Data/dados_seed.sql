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
    
START TRANSACTION;

-- 1. Limpeza dos registros genéricos default criados pela migration inicial
DELETE FROM `MusicaAtividade` WHERE AtividadeId = 1;
DELETE FROM `MusicaEmocao` WHERE EmocaoId = 1;

-- 2. Inserção das Relações em MusicaEmocao
INSERT INTO `MusicaEmocao` (`MidiaId`, `EmocaoId`) VALUES
-- Queen (Midias 1 a 9)
(1, 7), (1, 3),        -- Death on Two Legs: Enfurecido, Energético
(2, 2), (2, 5),        -- Love of My Life: Triste, Reflexivo
(3, 5), (3, 3), (3, 1),-- Bohemian Rhapsody: Reflexivo, Energético, Alegre
(4, 3), (4, 4),        -- We Will Rock You: Energético, Motivado
(5, 4), (5, 1),        -- We Are the Champions: Motivado, Alegre
(6, 4), (6, 5),        -- Spread Your Wings: Motivado, Reflexivo
(7, 3), (7, 1),        -- Another One Bites the Dust: Energético, Alegre
(8, 1), (8, 3),        -- Crazy Little Thing Called Love: Alegre, Energético
(9, 2), (9, 5),        -- Save Me: Triste, Reflexivo

-- Michael Jackson (Midias 10 a 18)
(10, 1), (10, 3),      -- Don't Stop 'Til You Get Enough: Alegre, Energético
(11, 1), (11, 6),      -- Rock with You: Alegre, Preguiçoso
(12, 1), (12, 3),      -- Off the Wall: Alegre, Energético
(13, 3), (13, 7),      -- Thriller: Energético, Enfurecido
(14, 3), (14, 4), (14, 7), -- Beat It: Energético, Motivado, Enfurecido
(15, 3), (15, 4),      -- Billie Jean: Energético, Motivado
(16, 3), (16, 7),      -- Bad: Energético, Enfurecido
(17, 1), (17, 3),      -- The Way You Make Me Feel: Alegre, Energético
(18, 3), (18, 4),      -- Smooth Criminal: Energético, Motivado

-- Caetano Veloso (Midias 19 a 27)
(19, 5), (19, 2),      -- You Don't Know Me: Reflexivo, Triste
(20, 1), (20, 5),      -- Nine Out of Ten: Alegre, Reflexivo
(21, 5), (21, 2),      -- Triste Bahia: Reflexivo, Triste
(22, 1), (22, 6), (22, 5), -- O Leãozinho: Alegre, Preguiçoso, Reflexivo
(23, 5), (23, 1),      -- Tigresa: Reflexivo, Alegre
(24, 5), (24, 4),      -- Gente: Reflexivo, Motivado
(25, 1), (25, 4),      -- Lua de São Jorge: Alegre, Motivado
(26, 5), (26, 2),      -- Oração ao Tempo: Reflexivo, Triste
(27, 1), (27, 3),      -- Beleza Pura: Alegre, Energético

-- Iron Maiden (Midias 28 a 36)
(28, 3), (28, 4), (28, 7), -- Run to the Hills: Energético, Motivado, Enfurecido
(29, 3), (29, 7),      -- The Number of the Beast: Energético, Enfurecido
(30, 5), (30, 3), (30, 7), -- Hallowed Be Thy Name: Reflexivo, Energético, Enfurecido
(31, 3), (31, 4),      -- Aces High: Energético, Motivado
(32, 3), (32, 7),      -- 2 Minutes to Midnight: Energético, Enfurecido
(33, 3), (33, 5),      -- Powerslave: Energético, Reflexivo
(34, 3), (34, 7),      -- Be Quick or Be Dead: Energético, Enfurecido
(35, 5), (35, 2),      -- Afraid to Shoot Strangers: Reflexivo, Triste
(36, 3), (36, 4), (36, 7), -- Fear of the Dark: Energético, Motivado, Enfurecido

-- Miles Davis (Midias 37 a 45)
(37, 5), (37, 6),      -- So What: Reflexivo, Preguiçoso
(38, 1), (38, 5),      -- Freddie Freeloader: Alegre, Reflexivo
(39, 2), (39, 5), (39, 6), -- Blue in Green: Triste, Reflexivo, Preguiçoso
(40, 5), (40, 3),      -- Pharaoh's Dance: Reflexivo, Energético
(41, 5), (41, 7),      -- Bitches Brew: Reflexivo, Enfurecido
(42, 5), (42, 3),      -- Spanish Key: Reflexivo, Energético
(43, 5), (43, 2),      -- Concierto de Aranjuez: Reflexivo, Triste
(44, 5), (44, 6),      -- Will O' the Wisp: Reflexivo, Preguiçoso
(45, 5), (45, 1);      -- The Pan Piper: Reflexivo, Alegre

-- 3. Inserção das Relações em MusicaAtividade
INSERT INTO `MusicaAtividade` (`MidiaId`, `AtividadeId`) VALUES
-- Queen (Midias 1 a 9)
(1, 3), (1, 1),        -- Death on Two Legs: Jogando, Caminhando
(2, 5), (2, 2),        -- Love of My Life: Relaxando, Cozinhando
(3, 2), (3, 1), (3, 3),-- Bohemian Rhapsody: Cozinhando, Caminhando, Jogando
(4, 1), (4, 3),        -- We Will Rock You: Caminhando, Jogando
(5, 1), (5, 6),        -- We Are the Champions: Caminhando, Trabalhando
(6, 4), (6, 1),        -- Spread Your Wings: Estudando, Caminhando
(7, 1), (7, 3), (7, 2),-- Another One Bites the Dust: Caminhando, Jogando, Cozinhando
(8, 2), (8, 1),        -- Crazy Little Thing Called Love: Cozinhando, Caminhando
(9, 5), (9, 4),        -- Save Me: Relaxando, Estudando

-- Michael Jackson (Midias 10 a 18)
(10, 1), (10, 2),      -- Don't Stop 'Til You Get Enough: Caminhando, Cozinhando
(11, 2), (11, 5),      -- Rock with You: Cozinhando, Relaxando
(12, 1), (12, 3),      -- Off the Wall: Caminhando, Jogando
(13, 3), (13, 1),      -- Thriller: Jogando, Caminhando
(14, 1), (14, 3),      -- Beat It: Caminhando, Jogando
(15, 1), (15, 3), (15, 6), -- Billie Jean: Caminhando, Jogando, Trabalhando
(16, 1), (16, 3),      -- Bad: Caminhando, Jogando
(17, 2), (17, 1),      -- The Way You Make Me Feel: Cozinhando, Caminhando
(18, 3), (18, 1),      -- Smooth Criminal: Jogando, Caminhando

-- Caetano Veloso (Midias 19 a 27)
(19, 5), (19, 4),      -- You Don't Know Me: Relaxando, Estudando
(20, 2), (20, 5),      -- Nine Out of Ten: Cozinhando, Relaxando
(21, 4), (21, 5),      -- Triste Bahia: Estudando, Relaxando
(22, 5), (22, 2), (22, 4), -- O Leãozinho: Relaxando, Cozinhando, Estudando
(23, 2), (23, 6),      -- Tigresa: Cozinhando, Trabalhando
(24, 4), (24, 6),      -- Gente: Estudando, Trabalhando
(25, 2), (25, 1),      -- Lua de São Jorge: Cozinhando, Caminhando
(26, 4), (26, 5),      -- Oração ao Tempo: Estudando, Relaxando
(27, 2), (27, 1),      -- Beleza Pura: Cozinhando, Caminhando

-- Iron Maiden (Midias 28 a 36)
(28, 3), (28, 1),      -- Run to the Hills: Jogando, Caminhando
(29, 3), (29, 1),      -- The Number of the Beast: Jogando, Caminhando
(30, 3), (30, 4),      -- Hallowed Be Thy Name: Jogando, Estudando
(31, 3), (31, 1),      -- Aces High: Jogando, Caminhando
(32, 3), (32, 1),      -- 2 Minutes to Midnight: Jogando, Caminhando
(33, 3), (33, 4),      -- Powerslave: Jogando, Estudando
(34, 3), (34, 1),      -- Be Quick or Be Dead: Jogando, Caminhando
(35, 4), (35, 5),      -- Afraid to Shoot Strangers: Estudando, Relaxando
(36, 1), (36, 3), (36, 2), -- Fear of the Dark: Caminhando, Jogando, Cozinhando

-- Miles Davis (Midias 37 a 45)
(37, 4), (37, 6), (37, 5), -- So What: Estudando, Trabalhando, Relaxando
(38, 2), (38, 6),      -- Freddie Freeloader: Cozinhando, Trabalhando
(39, 4), (39, 5),      -- Blue in Green: Estudando, Relaxando
(40, 6), (40, 4),      -- Pharaoh's Dance: Trabalhando, Estudando
(41, 6), (41, 3),      -- Bitches Brew: Trabalhando, Jogando
(42, 6), (42, 4),      -- Spanish Key: Trabalhando, Estudando
(43, 5), (43, 4),      -- Concierto de Aranjuez: Relaxando, Estudando
(44, 4), (44, 5),      -- Will O' the Wisp: Estudando, Relaxando
(45, 5), (45, 2);      -- The Pan Piper: Relaxando, Cozinhando

COMMIT;