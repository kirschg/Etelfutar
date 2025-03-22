using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Models;

public partial class EtelfutarContext : DbContext
{
    public EtelfutarContext()
    {
    }

    public EtelfutarContext(DbContextOptions<EtelfutarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chain> Chains { get; set; }

    public virtual DbSet<Ertekelesek> Ertekeleseks { get; set; }

    public virtual DbSet<Etelek> Eteleks { get; set; }

    public virtual DbSet<Ettermek> Ettermeks { get; set; }

    public virtual DbSet<Felhasznalok> Felhasznaloks { get; set; }

    public virtual DbSet<Learaza> Learazas { get; set; }

    public virtual DbSet<Rendeles> Rendeles { get; set; }

    public virtual DbSet<Varosok> Varosoks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=etelfutar;USER=root;PASSWORD=;SSL MODE=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chain");

            entity.HasIndex(e => e.Nev, "Nev").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Nev).HasMaxLength(64);
        });

        modelBuilder.Entity<Ertekelesek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ertekelesek");

            entity.HasIndex(e => e.EtteremId, "EtteremId");

            entity.HasIndex(e => new { e.FelhasznaloId, e.EtteremId }, "FelhasznaloId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Ertekeles).HasColumnType("int(1)");
            entity.Property(e => e.EtteremId).HasColumnType("int(11)");
            entity.Property(e => e.FelhasznaloId).HasColumnType("int(11)");
            entity.Property(e => e.Szoveg).HasMaxLength(255);

            entity.HasOne(d => d.Etterem).WithMany(p => p.Ertekeleseks)
                .HasForeignKey(d => d.EtteremId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ertekelesek_ibfk_2");

            entity.HasOne(d => d.Felhasznalo).WithMany(p => p.Ertekeleseks)
                .HasForeignKey(d => d.FelhasznaloId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ertekelesek_ibfk_1");
        });

        modelBuilder.Entity<Etelek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("etelek");

            entity.HasIndex(e => e.ChainId, "etteremId");

            entity.HasIndex(e => e.Nev, "nev").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Ar)
                .HasColumnType("int(11)")
                .HasColumnName("ar");
            entity.Property(e => e.ChainId).HasColumnType("int(11)");
            entity.Property(e => e.Indexkep).HasMaxLength(224);
            entity.Property(e => e.Kaloria)
                .HasColumnType("int(11)")
                .HasColumnName("kaloria");
            entity.Property(e => e.Nev)
                .HasMaxLength(32)
                .HasColumnName("nev");

            entity.HasOne(d => d.Chain).WithMany(p => p.Eteleks)
                .HasForeignKey(d => d.ChainId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("etelek_ibfk_1");

            entity.HasMany(d => d.Etterems).WithMany(p => p.Etels)
                .UsingEntity<Dictionary<string, object>>(
                    "Excludedetel",
                    r => r.HasOne<Ettermek>().WithMany()
                        .HasForeignKey("EtteremId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("excludedetel_ibfk_1"),
                    l => l.HasOne<Etelek>().WithMany()
                        .HasForeignKey("EtelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("excludedetel_ibfk_2"),
                    j =>
                    {
                        j.HasKey("EtelId", "EtteremId").HasName("PRIMARY");
                        j.ToTable("excludedetel");
                        j.HasIndex(new[] { "EtelId", "EtteremId" }, "EtelId");
                        j.HasIndex(new[] { "EtteremId" }, "EtteremId");
                        j.IndexerProperty<int>("EtelId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("EtteremId").HasColumnType("int(11)");
                    });

            entity.HasMany(d => d.Rendeles).WithMany(p => p.Etels)
                .UsingEntity<Dictionary<string, object>>(
                    "Rendeltetel",
                    r => r.HasOne<Rendeles>().WithMany()
                        .HasForeignKey("RendelesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("rendeltetel_ibfk_1"),
                    l => l.HasOne<Etelek>().WithMany()
                        .HasForeignKey("EtelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("rendeltetel_ibfk_2"),
                    j =>
                    {
                        j.HasKey("EtelId", "RendelesId").HasName("PRIMARY");
                        j.ToTable("rendeltetel");
                        j.HasIndex(new[] { "EtelId", "RendelesId" }, "EtelId");
                        j.HasIndex(new[] { "RendelesId" }, "RendelesId");
                        j.IndexerProperty<int>("EtelId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("RendelesId").HasColumnType("int(11)");
                    });
        });

        modelBuilder.Entity<Ettermek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ettermek");

            entity.HasIndex(e => e.ChainId, "ChainId");

            entity.HasIndex(e => e.VarosId, "varosId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ChainId).HasColumnType("int(11)");
            entity.Property(e => e.Cim).HasMaxLength(32);
            entity.Property(e => e.Indexkep).HasMaxLength(224);
            entity.Property(e => e.VarosId)
                .HasColumnType("int(11)")
                .HasColumnName("varosId");

            entity.HasOne(d => d.Chain).WithMany(p => p.Ettermeks)
                .HasForeignKey(d => d.ChainId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ettermek_ibfk_2");

            entity.HasOne(d => d.Varos).WithMany(p => p.Ettermeks)
                .HasForeignKey(d => d.VarosId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ettermek_ibfk_1");
        });

        modelBuilder.Entity<Felhasznalok>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("felhasznalok");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.FelhasznaloNev, "FelhasznaloNev").IsUnique();

            entity.HasIndex(e => e.VarosId, "VarosId");

            entity.Property(e => e.Id).HasColumnType("int(255)");
            entity.Property(e => e.Aktiv).HasColumnType("int(1)");
            entity.Property(e => e.Email).HasMaxLength(64);
            entity.Property(e => e.FelhasznaloNev).HasMaxLength(32);
            entity.Property(e => e.Hash).HasMaxLength(64);
            entity.Property(e => e.Jogosultsag).HasColumnType("int(11)");
            entity.Property(e => e.Lakcim).HasMaxLength(64);
            entity.Property(e => e.Salt).HasMaxLength(64);
            entity.Property(e => e.TeljesNev).HasMaxLength(64);
            entity.Property(e => e.VarosId).HasColumnType("int(11)");

            entity.HasOne(d => d.Varos).WithMany(p => p.Felhasznaloks)
                .HasForeignKey(d => d.VarosId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("felhasznalok_ibfk_1");
        });

        modelBuilder.Entity<Learaza>(entity =>
        {
            entity.HasKey(e => new { e.EtelId, e.EtteremId }).HasName("PRIMARY");

            entity.ToTable("learazas");

            entity.HasIndex(e => new { e.EtteremId, e.EtelId }, "EtteremId").IsUnique();

            entity.Property(e => e.EtelId).HasColumnType("int(11)");
            entity.Property(e => e.EtteremId).HasColumnType("int(11)");
            entity.Property(e => e.Learazas).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Rendeles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rendeles");

            entity.HasIndex(e => e.FelhasznaloId, "FelhasznaloId");

            entity.HasIndex(e => e.FelhasznaloId, "FelhasznaloId_2").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.FelhasznaloId).HasColumnType("int(11)");

            entity.HasOne(d => d.Felhasznalo).WithOne(p => p.Rendeles)
                .HasForeignKey<Rendeles>(d => d.FelhasznaloId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rendeles_ibfk_1");
        });

        modelBuilder.Entity<Varosok>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("varosok");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IndexKep)
                .HasMaxLength(224)
                .HasColumnName("indexKep");
            entity.Property(e => e.Nev).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
