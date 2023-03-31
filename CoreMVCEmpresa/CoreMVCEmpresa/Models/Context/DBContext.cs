using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CoreMVCEmpresa.Models.Entities;

namespace CoreMVCEmpresa.Models.Context
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCatPuesto> TCatPuesto { get; set; } = null!;
        public virtual DbSet<TEmpleados> TEmpleados { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Database=Empresa;integrated security=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TCatPuesto>(entity =>
            {
                entity.HasKey(e => e.IdPuesto)
                    .HasName("PK__T_CAT_PU__71BE91D32761DCFC");

                entity.ToTable("T_CAT_PUESTO");

                entity.Property(e => e.IdPuesto).HasColumnName("Id_Puesto");

                entity.Property(e => e.NombrePuesto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Puesto");
            });

            modelBuilder.Entity<TEmpleados>(entity =>
            {
                entity.HasKey(e => e.IdNumEmp)
                    .HasName("PK__T_EMPLEA__1EEF05D48A3ABFED");

                entity.ToTable("T_EMPLEADOS");

                entity.Property(e => e.IdNumEmp).HasColumnName("Id_NumEmp");

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPuesto).HasColumnName("Id_Puesto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPuestoNavigation)
                    .WithMany(p => p.TEmpleados)
                    .HasForeignKey(d => d.IdPuesto)
                    .HasConstraintName("FK__T_EMPLEAD__Id_Pu__3F466844");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
