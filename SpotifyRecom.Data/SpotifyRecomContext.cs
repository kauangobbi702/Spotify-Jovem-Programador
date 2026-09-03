using Microsoft.EntityFrameworkCore;
namespace SpotifyRecom.Data;

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

    private readonly string StringConexao = "Server=localhost;Port=3306;Database=db_diario_senac;Uid=root;Pwd=1234;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(StringConexao, ServerVersion.AutoDetect(StringConexao));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Playlists)
            .WithOne(p => p.Usuario)
            .HasForeignKey(p => p.UsuarioId);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Biblioteca)
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
