namespace SpotifyRecom.Data;
using Microsoft.EntityFrameworkCore;
using SpotifyRecom.Model;
public class SpotifyRecomContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set;}
    public DbSet<Album> Albums { get; set;}
    public DbSet<Artista> Artistas { get; set;}
    public DbSet<Biblioteca> Bibliotecas { get; set;}
    public DbSet<Genero> Generos { get; set;}
    public DbSet<Midia> Midias { get; set;}
    public DbSet<Plano> Planos { get; set;}
    public DbSet<Playlist> Playlists { get; set;}
    public DbSet<MusicaCurtida> MusicasCurtidas { get; set; }
    public DbSet<ArtistaSeguido> ArtistasSeguidos { get; set; }
    public DbSet<MusicaPlaylist> MusicasPlaylists { get; set; }
    public DbSet<Emocao> Emocoes { get; set; }
    public DbSet<Atividade> Atividades { get; set; }
    public DbSet<MusicaEmocao> MusicasEmocoes { get; set; }
    public DbSet<MusicaAtividade> MusicasAtividades { get; set; }

    private readonly string StringConexao = "Server=localhost;Port=3306;Database=db_spotify_recomendacoes;Uid=root;Pwd=Sh1nobu-chan!;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(StringConexao, new MySqlServerVersion(new Version(8, 0, 0)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
        modelBuilder.Entity<Album>().HasKey(a => a.IdAlbum);
        modelBuilder.Entity<Artista>().HasKey(a => a.IdArtista);
        modelBuilder.Entity<Biblioteca>().HasKey(b => b.IdBiblioteca);
        modelBuilder.Entity<Genero>().HasKey(g => g.IdGenero);
        modelBuilder.Entity<Midia>().HasKey(m => m.IdMidia);
        modelBuilder.Entity<Plano>().HasKey(p => p.IdPlano);
        modelBuilder.Entity<Playlist>().HasKey(p => p.IdPlaylist);
        modelBuilder.Entity<Emocao>().HasKey(e => e.IdEmocao);
        modelBuilder.Entity<Atividade>().HasKey(a => a.IdAtividade);

        modelBuilder.Entity<MusicaEmocao>(entity =>
        {
            entity.ToTable("MusicaEmocao");
            entity.HasKey(item => new { item.MidiaId, item.EmocaoId });
            entity.HasOne(item => item.Midia).WithMany(midia => midia.MusicasEmocoes)
                .HasForeignKey(item => item.MidiaId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(item => item.Emocao).WithMany(emocao => emocao.MusicasEmocoes)
                .HasForeignKey(item => item.EmocaoId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MusicaAtividade>(entity =>
        {
            entity.ToTable("MusicaAtividade");
            entity.HasKey(item => new { item.MidiaId, item.AtividadeId });
            entity.HasOne(item => item.Midia).WithMany(midia => midia.MusicasAtividades)
                .HasForeignKey(item => item.MidiaId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(item => item.Atividade).WithMany(atividade => atividade.MusicasAtividades)
                .HasForeignKey(item => item.AtividadeId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MusicaCurtida>(entity =>
        {
            entity.ToTable("MusicasCurtidas");
            entity.HasKey(musica => new { musica.UsuarioId, musica.MidiaId });

            entity.HasOne(musica => musica.Usuario)
                .WithMany()
                .HasForeignKey(musica => musica.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(musica => musica.Midia)
                .WithMany()
                .HasForeignKey(musica => musica.MidiaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ArtistaSeguido>(entity =>
        {
            entity.ToTable("ArtistasSeguidos");
            entity.HasKey(artista => new { artista.UsuarioId, artista.ArtistaId });

            entity.HasOne(artista => artista.Usuario)
                .WithMany()
                .HasForeignKey(artista => artista.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(artista => artista.Artista)
                .WithMany()
                .HasForeignKey(artista => artista.ArtistaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MusicaPlaylist>(entity =>
        {
            entity.ToTable("MusicasPlaylists");
            entity.HasKey(musica => new { musica.UsuarioId, musica.MidiaId, musica.PlaylistId });
            entity.HasIndex(musica => new { musica.PlaylistId, musica.MidiaId })
                .IsUnique();

            entity.HasOne(musica => musica.Playlist)
                .WithMany()
                .HasForeignKey(musica => musica.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(musica => musica.Usuario)
                .WithMany()
                .HasForeignKey(musica => musica.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(musica => musica.Midia)
                .WithMany()
                .HasForeignKey(musica => musica.MidiaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Playlists)
            .WithOne(p => p.Usuario)
            .HasForeignKey(p => p.UsuarioId);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.biblioteca)
            .WithOne(b => b.Usuario)
            .HasForeignKey<Biblioteca>(b => b.UsuarioId);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Plano)
            .WithMany(p => p.Usuarios)
            .HasForeignKey(u => u.PlanoId);

    
        modelBuilder.Entity<Artista>()
            .HasMany(a => a.Albuns)
            .WithMany(ab => ab.Artistas);

        modelBuilder.Entity<Artista>()
            .HasMany(a => a.GenerosArtista)
            .WithMany(g => g.Artistas);


        modelBuilder.Entity<Album>()
            .HasMany(a => a.Midias)
            .WithOne(m => m.Album)
            .HasForeignKey(m => m.AlbumId);


        modelBuilder.Entity<Midia>()
            .HasMany(m => m.Artistas)
            .WithMany(a => a.Midias);

        modelBuilder.Entity<Midia>()
            .HasMany(m => m.GenerosMidia)
            .WithMany(g => g.Midias);


        modelBuilder.Entity<Playlist>()
            .HasMany(p => p.Midias)
            .WithMany(m => m.Playlists);
        

        modelBuilder.Entity<Biblioteca>()
            .HasMany(b => b.Midias)
            .WithMany(m => m.Bibliotecas);

        modelBuilder.Entity<Biblioteca>()
            .HasMany(b => b.Playlists)
            .WithMany(p => p.Bibliotecas);
        

            
    }

}
