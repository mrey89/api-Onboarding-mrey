using apiOnBording.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace apiOnBording.Infrastructure.Persistence;

public partial class PedidosDbContext : DbContext
{
    public PedidosDbContext()
    {
    }

    public PedidosDbContext(DbContextOptions<PedidosDbContext> options)
        : base(options)
    {
    }
    public DbSet<TransaccionPedido> Pedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransaccionPedido>(entity =>
        {
            

            entity.ToTable("Pedidos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NumeroDePedido);
            entity.Property(e => e.CuentaCorriente)
                .IsRequired();
            entity.Property(e => e.CodigoDeContratoInterno)
                .IsRequired();
            entity.Property(e => e.CicloDelPedido)
                .IsRequired();
            entity.Property(e => e.EstadoDelPedido)
                .IsRequired();
            entity.Property(e => e.Cuando)
                .IsRequired();

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
