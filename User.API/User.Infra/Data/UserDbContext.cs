using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;

namespace User.Infra.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Carteira> Carteiras { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.HasOne(u => u.Carteira)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Carteira>(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Endereco

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Usuario)
                .WithOne(u => u.Endereco)
                .HasForeignKey<Endereco>(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Carteira

        modelBuilder.Entity<Carteira>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Saldo)
                .HasColumnType("decimal(18,2)");

            entity.Property(c => c.RowVersion)
                .IsRowVersion();
        });

        // Transacao
        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Valor)
                .HasColumnType("decimal(18,2)");

            entity.HasIndex(t => t.ChaveIdempotencia)
                .IsUnique();

            entity.HasOne(t => t.CarteiraRemetente)
                .WithMany()
                .HasForeignKey(t => t.CarteiraRemetenteId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.CarteiraDestinatario)
                .WithMany()
                .HasForeignKey(t => t.CarteiraDestinatarioId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}
