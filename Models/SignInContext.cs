using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace SignIn.Models;

public partial class SignInContext : DbContext
{
    public SignInContext()
    {
    }

    public SignInContext(DbContextOptions<SignInContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeTable> EmployeeTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) { }
    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeTable>(entity =>
        {
            entity.HasKey(e => e.EmpId);

            entity.ToTable("EmployeeTable");

            entity.Property(e => e.EmpId)
                .ValueGeneratedNever()
                .HasColumnName("EmpID");
            entity.Property(e => e.CompId).HasColumnName("CompID");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
