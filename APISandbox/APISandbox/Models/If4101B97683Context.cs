using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APISandbox.Models;

public partial class If4101B97683Context : DbContext
{
    private readonly IConfiguration _configuration;

    public If4101B97683Context()
    {
    }

    public If4101B97683Context(IConfiguration configuration)
    {
        _configuration = configuration;
    }



    public virtual DbSet<ContactInformation> ContactInformations { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<GetAllStudentsView> GetAllStudentsViews { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactInformation>(entity =>
        {
            entity.ToTable("ContactInformation");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<GetAllStudentsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetAllStudentsView");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NationalityId).HasColumnName("Nationality_Id");
            entity.Property(e => e.NationalityName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.ToTable("Nationality");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NationalityId).HasColumnName("Nationality_Id");
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.Nationality).WithMany(p => p.Students)
                .HasForeignKey(d => d.NationalityId)
                .HasConstraintName("FK_Student_Nationality");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
