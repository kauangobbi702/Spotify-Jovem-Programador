CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;
DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `Albums` (
        `IdAlbum` int NOT NULL AUTO_INCREMENT,
        `Nome` longtext CHARACTER SET utf8mb4 NOT NULL,
        `ArtistaId` int NOT NULL,
        `MidiaId` int NOT NULL,
        `AnoLancamento` int NOT NULL,
        CONSTRAINT `PK_Albums` PRIMARY KEY (`IdAlbum`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `Artistas` (
        `IdArtista` int NOT NULL AUTO_INCREMENT,
        `Nome` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_Artistas` PRIMARY KEY (`IdArtista`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `Generos` (
        `IdGenero` int NOT NULL AUTO_INCREMENT,
        `Nome` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_Generos` PRIMARY KEY (`IdGenero`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `Planos` (
        `IdPlano` int NOT NULL AUTO_INCREMENT,
        `Descricao` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Valor` decimal(65,30) NOT NULL,
        CONSTRAINT `PK_Planos` PRIMARY KEY (`IdPlano`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `Midias` (
        `IdMidia` int NOT NULL AUTO_INCREMENT,
        `Titulo` longtext CHARACTER SET utf8mb4 NOT NULL,
        `ArtistaId` int NOT NULL,
        `AlbumId` int NOT NULL,
        `Duracao` time(6) NOT NULL,
        CONSTRAINT `PK_Midias` PRIMARY KEY (`IdMidia`),
        CONSTRAINT `FK_Midias_Albums_AlbumId` FOREIGN KEY (`AlbumId`) REFERENCES `Albums` (`IdAlbum`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `AlbumArtista` (
        `AlbunsIdAlbum` int NOT NULL,
        `ArtistasIdArtista` int NOT NULL,
        CONSTRAINT `PK_AlbumArtista` PRIMARY KEY (`AlbunsIdAlbum`, `ArtistasIdArtista`),
        CONSTRAINT `FK_AlbumArtista_Albums_AlbunsIdAlbum` FOREIGN KEY (`AlbunsIdAlbum`) REFERENCES `Albums` (`IdAlbum`) ON DELETE CASCADE,
        CONSTRAINT `FK_AlbumArtista_Artistas_ArtistasIdArtista` FOREIGN KEY (`ArtistasIdArtista`) REFERENCES `Artistas` (`IdArtista`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `ArtistaGenero` (
        `ArtistasIdArtista` int NOT NULL,
        `GenerosArtistaIdGenero` int NOT NULL,
        CONSTRAINT `PK_ArtistaGenero` PRIMARY KEY (`ArtistasIdArtista`, `GenerosArtistaIdGenero`),
        CONSTRAINT `FK_ArtistaGenero_Artistas_ArtistasIdArtista` FOREIGN KEY (`ArtistasIdArtista`) REFERENCES `Artistas` (`IdArtista`) ON DELETE CASCADE,
        CONSTRAINT `FK_ArtistaGenero_Generos_GenerosArtistaIdGenero` FOREIGN KEY (`GenerosArtistaIdGenero`) REFERENCES `Generos` (`IdGenero`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `Usuarios` (
        `IdUsuario` int NOT NULL AUTO_INCREMENT,
        `Nome` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Senha` longtext CHARACTER SET utf8mb4 NOT NULL,
        `PlanoId` int NOT NULL,
        CONSTRAINT `PK_Usuarios` PRIMARY KEY (`IdUsuario`),
        CONSTRAINT `FK_Usuarios_Planos_PlanoId` FOREIGN KEY (`PlanoId`) REFERENCES `Planos` (`IdPlano`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `ArtistaMidia` (
        `ArtistasIdArtista` int NOT NULL,
        `MidiasIdMidia` int NOT NULL,
        CONSTRAINT `PK_ArtistaMidia` PRIMARY KEY (`ArtistasIdArtista`, `MidiasIdMidia`),
        CONSTRAINT `FK_ArtistaMidia_Artistas_ArtistasIdArtista` FOREIGN KEY (`ArtistasIdArtista`) REFERENCES `Artistas` (`IdArtista`) ON DELETE CASCADE,
        CONSTRAINT `FK_ArtistaMidia_Midias_MidiasIdMidia` FOREIGN KEY (`MidiasIdMidia`) REFERENCES `Midias` (`IdMidia`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `GeneroMidia` (
        `GenerosMidiaIdGenero` int NOT NULL,
        `MidiasIdMidia` int NOT NULL,
        CONSTRAINT `PK_GeneroMidia` PRIMARY KEY (`GenerosMidiaIdGenero`, `MidiasIdMidia`),
        CONSTRAINT `FK_GeneroMidia_Generos_GenerosMidiaIdGenero` FOREIGN KEY (`GenerosMidiaIdGenero`) REFERENCES `Generos` (`IdGenero`) ON DELETE CASCADE,
        CONSTRAINT `FK_GeneroMidia_Midias_MidiasIdMidia` FOREIGN KEY (`MidiasIdMidia`) REFERENCES `Midias` (`IdMidia`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `Bibliotecas` (
        `IdBiblioteca` int NOT NULL AUTO_INCREMENT,
        `UsuarioId` int NOT NULL,
        CONSTRAINT `PK_Bibliotecas` PRIMARY KEY (`IdBiblioteca`),
        CONSTRAINT `FK_Bibliotecas_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`IdUsuario`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `Playlists` (
        `IdPlaylist` int NOT NULL AUTO_INCREMENT,
        `NomePlaylist` longtext CHARACTER SET utf8mb4 NOT NULL,
        `UsuarioId` int NOT NULL,
        CONSTRAINT `PK_Playlists` PRIMARY KEY (`IdPlaylist`),
        CONSTRAINT `FK_Playlists_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`IdUsuario`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `BibliotecaMidia` (
        `BibliotecasIdBiblioteca` int NOT NULL,
        `MidiasIdMidia` int NOT NULL,
        CONSTRAINT `PK_BibliotecaMidia` PRIMARY KEY (`BibliotecasIdBiblioteca`, `MidiasIdMidia`),
        CONSTRAINT `FK_BibliotecaMidia_Bibliotecas_BibliotecasIdBiblioteca` FOREIGN KEY (`BibliotecasIdBiblioteca`) REFERENCES `Bibliotecas` (`IdBiblioteca`) ON DELETE CASCADE,
        CONSTRAINT `FK_BibliotecaMidia_Midias_MidiasIdMidia` FOREIGN KEY (`MidiasIdMidia`) REFERENCES `Midias` (`IdMidia`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `BibliotecaPlaylist` (
        `BibliotecasIdBiblioteca` int NOT NULL,
        `PlaylistsIdPlaylist` int NOT NULL,
        CONSTRAINT `PK_BibliotecaPlaylist` PRIMARY KEY (`BibliotecasIdBiblioteca`, `PlaylistsIdPlaylist`),
        CONSTRAINT `FK_BibliotecaPlaylist_Bibliotecas_BibliotecasIdBiblioteca` FOREIGN KEY (`BibliotecasIdBiblioteca`) REFERENCES `Bibliotecas` (`IdBiblioteca`) ON DELETE CASCADE,
        CONSTRAINT `FK_BibliotecaPlaylist_Playlists_PlaylistsIdPlaylist` FOREIGN KEY (`PlaylistsIdPlaylist`) REFERENCES `Playlists` (`IdPlaylist`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE TABLE `MidiaPlaylist` (
        `MidiasIdMidia` int NOT NULL,
        `PlaylistsIdPlaylist` int NOT NULL,
        CONSTRAINT `PK_MidiaPlaylist` PRIMARY KEY (`MidiasIdMidia`, `PlaylistsIdPlaylist`),
        CONSTRAINT `FK_MidiaPlaylist_Midias_MidiasIdMidia` FOREIGN KEY (`MidiasIdMidia`) REFERENCES `Midias` (`IdMidia`) ON DELETE CASCADE,
        CONSTRAINT `FK_MidiaPlaylist_Playlists_PlaylistsIdPlaylist` FOREIGN KEY (`PlaylistsIdPlaylist`) REFERENCES `Playlists` (`IdPlaylist`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_AlbumArtista_ArtistasIdArtista` ON `AlbumArtista` (`ArtistasIdArtista`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_ArtistaGenero_GenerosArtistaIdGenero` ON `ArtistaGenero` (`GenerosArtistaIdGenero`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_ArtistaMidia_MidiasIdMidia` ON `ArtistaMidia` (`MidiasIdMidia`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_BibliotecaMidia_MidiasIdMidia` ON `BibliotecaMidia` (`MidiasIdMidia`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_BibliotecaPlaylist_PlaylistsIdPlaylist` ON `BibliotecaPlaylist` (`PlaylistsIdPlaylist`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE UNIQUE INDEX `IX_Bibliotecas_UsuarioId` ON `Bibliotecas` (`UsuarioId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_GeneroMidia_MidiasIdMidia` ON `GeneroMidia` (`MidiasIdMidia`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_MidiaPlaylist_PlaylistsIdPlaylist` ON `MidiaPlaylist` (`PlaylistsIdPlaylist`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_Midias_AlbumId` ON `Midias` (`AlbumId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_Playlists_UsuarioId` ON `Playlists` (`UsuarioId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    CREATE INDEX `IX_Usuarios_PlanoId` ON `Usuarios` (`PlanoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907145422_CriandoBD') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260907145422_CriandoBD', '9.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907151225_AdicionandoTabelasDeRelacionamento') THEN

    CREATE TABLE `ArtistasSeguidos` (
        `UsuarioId` int NOT NULL,
        `ArtistaId` int NOT NULL,
        CONSTRAINT `PK_ArtistasSeguidos` PRIMARY KEY (`UsuarioId`, `ArtistaId`),
        CONSTRAINT `FK_ArtistasSeguidos_Artistas_ArtistaId` FOREIGN KEY (`ArtistaId`) REFERENCES `Artistas` (`IdArtista`) ON DELETE CASCADE,
        CONSTRAINT `FK_ArtistasSeguidos_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`IdUsuario`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907151225_AdicionandoTabelasDeRelacionamento') THEN

    CREATE TABLE `MusicasCurtidas` (
        `UsuarioId` int NOT NULL,
        `MidiaId` int NOT NULL,
        CONSTRAINT `PK_MusicasCurtidas` PRIMARY KEY (`UsuarioId`, `MidiaId`),
        CONSTRAINT `FK_MusicasCurtidas_Midias_MidiaId` FOREIGN KEY (`MidiaId`) REFERENCES `Midias` (`IdMidia`) ON DELETE CASCADE,
        CONSTRAINT `FK_MusicasCurtidas_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`IdUsuario`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907151225_AdicionandoTabelasDeRelacionamento') THEN

    CREATE INDEX `IX_ArtistasSeguidos_ArtistaId` ON `ArtistasSeguidos` (`ArtistaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907151225_AdicionandoTabelasDeRelacionamento') THEN

    CREATE INDEX `IX_MusicasCurtidas_MidiaId` ON `MusicasCurtidas` (`MidiaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907151225_AdicionandoTabelasDeRelacionamento') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260907151225_AdicionandoTabelasDeRelacionamento', '9.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907160016_AdicionandoOperacoesDePlaylist') THEN

    CREATE TABLE `MusicasPlaylists` (
        `UsuarioId` int NOT NULL,
        `MidiaId` int NOT NULL,
        `PlaylistId` int NOT NULL,
        CONSTRAINT `PK_MusicasPlaylists` PRIMARY KEY (`UsuarioId`, `MidiaId`, `PlaylistId`),
        CONSTRAINT `FK_MusicasPlaylists_Midias_MidiaId` FOREIGN KEY (`MidiaId`) REFERENCES `Midias` (`IdMidia`) ON DELETE CASCADE,
        CONSTRAINT `FK_MusicasPlaylists_Playlists_PlaylistId` FOREIGN KEY (`PlaylistId`) REFERENCES `Playlists` (`IdPlaylist`) ON DELETE CASCADE,
        CONSTRAINT `FK_MusicasPlaylists_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`IdUsuario`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907160016_AdicionandoOperacoesDePlaylist') THEN

    CREATE INDEX `IX_MusicasPlaylists_MidiaId` ON `MusicasPlaylists` (`MidiaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907160016_AdicionandoOperacoesDePlaylist') THEN

    CREATE INDEX `IX_MusicasPlaylists_PlaylistId` ON `MusicasPlaylists` (`PlaylistId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907160016_AdicionandoOperacoesDePlaylist') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260907160016_AdicionandoOperacoesDePlaylist', '9.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907165035_ImpedirMusicasDuplicadasNaPlaylist') THEN

    CREATE UNIQUE INDEX `IX_MusicasPlaylists_PlaylistId_MidiaId` ON `MusicasPlaylists` (`PlaylistId`, `MidiaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907165035_ImpedirMusicasDuplicadasNaPlaylist') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260907165035_ImpedirMusicasDuplicadasNaPlaylist', '9.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    ALTER TABLE `Midias` ADD `AtividadeId` int NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    ALTER TABLE `Midias` ADD `EmocaoId` int NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    CREATE TABLE `Atividades` (
        `IdAtividade` int NOT NULL AUTO_INCREMENT,
        `Nome` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_Atividades` PRIMARY KEY (`IdAtividade`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    CREATE TABLE `Emocoes` (
        `IdEmocao` int NOT NULL AUTO_INCREMENT,
        `Nome` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_Emocoes` PRIMARY KEY (`IdEmocao`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    INSERT INTO `Atividades` (`IdAtividade`, `Nome`)
    VALUES (1, 'Caminhando'),
    (2, 'Cozinhando'),
    (3, 'Jogando'),
    (4, 'Estudando'),
    (5, 'Relaxando'),
    (6, 'Trabalhando');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    INSERT INTO `Emocoes` (`IdEmocao`, `Nome`)
    VALUES (1, 'Alegre'),
    (2, 'Triste'),
    (3, 'Energetico'),
    (4, 'Motivado'),
    (5, 'Reflexivo'),
    (6, 'Preguicoso'),
    (7, 'Enfurecido');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    UPDATE `Midias` SET `AtividadeId` = 1, `EmocaoId` = 1 WHERE `AtividadeId` = 0 OR `EmocaoId` = 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    CREATE INDEX `IX_Midias_AtividadeId` ON `Midias` (`AtividadeId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    CREATE INDEX `IX_Midias_EmocaoId` ON `Midias` (`EmocaoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    ALTER TABLE `Midias` ADD CONSTRAINT `FK_Midias_Atividades_AtividadeId` FOREIGN KEY (`AtividadeId`) REFERENCES `Atividades` (`IdAtividade`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    ALTER TABLE `Midias` ADD CONSTRAINT `FK_Midias_Emocoes_EmocaoId` FOREIGN KEY (`EmocaoId`) REFERENCES `Emocoes` (`IdEmocao`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907183640_AdicionandoMoodMatch') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260907183640_AdicionandoMoodMatch', '9.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    CREATE TABLE `MusicaAtividade` (
        `MidiaId` int NOT NULL,
        `AtividadeId` int NOT NULL,
        CONSTRAINT `PK_MusicaAtividade` PRIMARY KEY (`MidiaId`, `AtividadeId`),
        CONSTRAINT `FK_MusicaAtividade_Atividades_AtividadeId` FOREIGN KEY (`AtividadeId`) REFERENCES `Atividades` (`IdAtividade`) ON DELETE CASCADE,
        CONSTRAINT `FK_MusicaAtividade_Midias_MidiaId` FOREIGN KEY (`MidiaId`) REFERENCES `Midias` (`IdMidia`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    CREATE TABLE `MusicaEmocao` (
        `MidiaId` int NOT NULL,
        `EmocaoId` int NOT NULL,
        CONSTRAINT `PK_MusicaEmocao` PRIMARY KEY (`MidiaId`, `EmocaoId`),
        CONSTRAINT `FK_MusicaEmocao_Emocoes_EmocaoId` FOREIGN KEY (`EmocaoId`) REFERENCES `Emocoes` (`IdEmocao`) ON DELETE CASCADE,
        CONSTRAINT `FK_MusicaEmocao_Midias_MidiaId` FOREIGN KEY (`MidiaId`) REFERENCES `Midias` (`IdMidia`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    CREATE INDEX `IX_MusicaAtividade_AtividadeId` ON `MusicaAtividade` (`AtividadeId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    CREATE INDEX `IX_MusicaEmocao_EmocaoId` ON `MusicaEmocao` (`EmocaoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    INSERT INTO `MusicaAtividade` (`MidiaId`, `AtividadeId`) SELECT `IdMidia`, `AtividadeId` FROM `Midias` WHERE `AtividadeId` <> 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    INSERT INTO `MusicaEmocao` (`MidiaId`, `EmocaoId`) SELECT `IdMidia`, `EmocaoId` FROM `Midias` WHERE `EmocaoId` <> 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    ALTER TABLE `Midias` DROP FOREIGN KEY `FK_Midias_Atividades_AtividadeId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    ALTER TABLE `Midias` DROP FOREIGN KEY `FK_Midias_Emocoes_EmocaoId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    ALTER TABLE `Midias` DROP INDEX `IX_Midias_AtividadeId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    ALTER TABLE `Midias` DROP INDEX `IX_Midias_EmocaoId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    ALTER TABLE `Midias` DROP COLUMN `AtividadeId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    ALTER TABLE `Midias` DROP COLUMN `EmocaoId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260907185127_CriarRelacionamentosMoodMatch') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260907185127_CriarRelacionamentosMoodMatch', '9.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

