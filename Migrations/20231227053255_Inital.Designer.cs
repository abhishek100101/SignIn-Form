﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SignIn.Models;

#nullable disable

namespace SignIn.Migrations
{
    [DbContext(typeof(SignInContext))]
    [Migration("20231227053255_Inital")]
    partial class Inital
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SignIn.Models.EmployeeTable", b =>
                {
                    b.Property<int>("EmpId")
                        .HasColumnType("int")
                        .HasColumnName("EmpID");

                    b.Property<int>("CompId")
                        .HasColumnType("int")
                        .HasColumnName("CompID");

                    b.Property<string>("Password")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Username")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("EmpId");

                    b.ToTable("EmployeeTable", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}