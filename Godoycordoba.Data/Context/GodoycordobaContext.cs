using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Godoycordoba.Data.Context;

public partial class GodoycordobaContext : DbContext
{
    public GodoycordobaContext()
    {
    }

    public GodoycordobaContext(DbContextOptions<GodoycordobaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TiposDocumento> TiposDocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TiposDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento);

            entity.ToTable("TiposDocumento");

            entity.Property(e => e.IdTipoDocumento).HasColumnName("Id_TipoDocumento");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElentronico)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("correoElentronico");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdTipoDocumento).HasColumnName("Id_TipoDocumento");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
