using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BuildProjectSummer2024.Models;

public partial class BuildProject2024Context : DbContext
{
    public BuildProject2024Context()
    {
    }

    public BuildProject2024Context(DbContextOptions<BuildProject2024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<HealthCareProvider> HealthCareProviders { get; set; }

    public virtual DbSet<HealthCareProviderRole> HealthCareProviderRoles { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<MedicalClaim> MedicalClaims { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-ML8R4NJG;Initial Catalog=BUILD_PROJECT_2024;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HealthCareProvider>(entity =>
        {
            entity.ToTable("HealthCareProvider");

            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Npi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NPI");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ZIP");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HealthCareProviders)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HealthCareProvider_Hospital");

            entity.HasOne(d => d.Role).WithMany(p => p.HealthCareProviders)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HealthCareProvider_Role");
        });

        modelBuilder.Entity<HealthCareProviderRole>(entity =>
        {
            entity.ToTable("HealthCareProviderRole");

            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.ToTable("Hospital");

            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HospitalName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ZIP");
        });

        modelBuilder.Entity<MedicalClaim>(entity =>
        {
            entity.ToTable("MedicalClaim");

            entity.Property(e => e.Ndc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NDC");
            entity.Property(e => e.Quantity).HasColumnType("numeric(18, 5)");
            entity.Property(e => e.RxNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("numeric(18, 5)");

            entity.HasOne(d => d.HealthCareProvider).WithMany(p => p.MedicalClaims)
                .HasForeignKey(d => d.HealthCareProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalClaim_HealthcareProvider");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalClaims)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalClaim_patient");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.InsuranceMemberId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Mrn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRN");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ssn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SSN");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ZIP");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Patients)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Hospital");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
