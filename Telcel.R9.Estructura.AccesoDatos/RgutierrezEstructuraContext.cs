﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Telcel.R9.Estructura.AccesoDatos;

public partial class RgutierrezEstructuraContext : DbContext
{
    public RgutierrezEstructuraContext()
    {
    }

    public RgutierrezEstructuraContext(DbContextOptions<RgutierrezEstructuraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EmpleadoView> EmpleadoViews { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=RGutierrezEstructura; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.DepartamentoId).HasName("PK__Departam__66BB0E1E3A34DA57");

            entity.ToTable("Departamento");

            entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__958BE6F066C6609B");

            entity.ToTable("Empleado");

            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PuestoId).HasColumnName("PuestoID");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.DepartamentoId)
                .HasConstraintName("FK_EmpleadoDepartamento");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.PuestoId)
                .HasConstraintName("FK_EmpleadoPuesto");
        });

        modelBuilder.Entity<EmpleadoView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EmpleadoView");

            entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");
            entity.Property(e => e.DescripcionDepartamento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionPuesto)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PuestoId).HasColumnName("PuestoID");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.PuestoId).HasName("PK__Puesto__F7F6C624A48102F4");

            entity.ToTable("Puesto");

            entity.Property(e => e.PuestoId).HasColumnName("PuestoID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
