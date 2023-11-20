using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Computer_House_Almacen.Models;

public partial class DbComputerHouseAlmacenContext : DbContext
{
    public DbComputerHouseAlmacenContext()
    {
    }

    public DbComputerHouseAlmacenContext(DbContextOptions<DbComputerHouseAlmacenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Idcate).HasName("PK__CATEGORI__382212C832A65FA1");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.Idcate).HasColumnName("IDCATE");
            entity.Property(e => e.Nomcate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMCATE");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Idmarca).HasName("PK__MARCA__C8C2A4AA06CAE626");

            entity.ToTable("MARCA");

            entity.Property(e => e.Idmarca).HasColumnName("IDMARCA");
            entity.Property(e => e.Nommarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMMARCA");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idprod).HasName("PK__PRODUCTO__8E0A90394906029C");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.Idprod).HasColumnName("IDPROD");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('AGOTADO')")
                .HasColumnName("ESTADO");
            entity.Property(e => e.Idcate).HasColumnName("IDCATE");
            entity.Property(e => e.Idmarca).HasColumnName("IDMARCA");
            entity.Property(e => e.Nomprod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMPROD");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("PRECIO");
            entity.Property(e => e.Stock)
                .HasDefaultValueSql("((0))")
                .HasColumnName("STOCK");

            entity.HasOne(d => d.oCategoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idcate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IDCATE");

            entity.HasOne(d => d.oMarca).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idmarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IDMARCA");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__USUARIO__98242AA98DE39FB6");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CONTRASENIA");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USUARIO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
