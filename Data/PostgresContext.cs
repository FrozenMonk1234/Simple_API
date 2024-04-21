using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Simple_API_user.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {

    }
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("applicant_pkey");

            entity.ToTable("applicant");

            entity.HasIndex(e => e.Name, "applicant_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("skills_pkey");

            entity.ToTable("skills");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Applicantid).HasColumnName("applicantid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Applicant).WithMany(p => p.Skills)
                .HasForeignKey(d => d.Applicantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("skills_applicantid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
