using Microsoft.EntityFrameworkCore;

namespace FabricaAPI.Models
{
    public partial class FabricaDBContext : DbContext
    {
        public FabricaDBContext()
        {
           
        }

        public FabricaDBContext(DbContextOptions<FabricaDBContext> options)
            : base(options)
        {
           
        }

        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; } 
        public virtual DbSet<Usuario> Usuarios { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.PedId)
                    .HasName("PK__PEDIDOS__50CF4A259E34F5A8");

                entity.ToTable("PEDIDOS");

                entity.Property(e => e.PedId).HasColumnName("PedID");

                entity.Property(e => e.PedIva).HasColumnName("PedIVA");

                entity.Property(e => e.PedSubtot).HasColumnType("money");

                entity.Property(e => e.PedTotal).HasColumnType("money");

                entity.Property(e => e.PedVrUnit).HasColumnType("money");

                entity.HasOne(d => d.oProducto)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PedPro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Producto");

                entity.HasOne(d => d.oUsuario)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PedUsu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProId)
                    .HasName("PK__PRODUCTO__620295F0EC9010A6");

                entity.ToTable("PRODUCTO");

                entity.Property(e => e.ProId).HasColumnName("ProID");

                entity.Property(e => e.ProDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProValor).HasColumnType("money");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuId)
                    .HasName("PK__USUARIO__685263A3D3A7345C");

                entity.ToTable("USUARIO");

                entity.Property(e => e.UsuId).HasColumnName("UsuID");

                entity.Property(e => e.UsuNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuPass)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

        }

       
    }
}
