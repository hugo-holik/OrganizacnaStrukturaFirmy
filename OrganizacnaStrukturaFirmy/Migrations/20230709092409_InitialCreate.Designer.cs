﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrganizacnaStrukturaFirmy.Data;

#nullable disable

namespace OrganizacnaStrukturaFirmy.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230709092409_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("LeaderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("LeaderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("LeaderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("LeaderId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DivisionId")
                        .HasColumnType("int");

                    b.Property<int?>("LeaderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.HasIndex("LeaderId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Company", b =>
                {
                    b.HasOne("OrganizacnaStrukturaFirmy.Models.Employee", "Leader")
                        .WithMany("Companies")
                        .HasForeignKey("LeaderId");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Department", b =>
                {
                    b.HasOne("OrganizacnaStrukturaFirmy.Models.Employee", "Leader")
                        .WithMany("Departments")
                        .HasForeignKey("LeaderId");

                    b.HasOne("OrganizacnaStrukturaFirmy.Models.Project", "Project")
                        .WithMany("Departments")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Leader");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Division", b =>
                {
                    b.HasOne("OrganizacnaStrukturaFirmy.Models.Company", "Company")
                        .WithMany("Divisions")
                        .HasForeignKey("CompanyId");

                    b.HasOne("OrganizacnaStrukturaFirmy.Models.Employee", "Leader")
                        .WithMany("Divisions")
                        .HasForeignKey("LeaderId");

                    b.Navigation("Company");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Employee", b =>
                {
                    b.HasOne("OrganizacnaStrukturaFirmy.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Project", b =>
                {
                    b.HasOne("OrganizacnaStrukturaFirmy.Models.Division", "Division")
                        .WithMany("Projects")
                        .HasForeignKey("DivisionId");

                    b.HasOne("OrganizacnaStrukturaFirmy.Models.Employee", "Leader")
                        .WithMany("Projects")
                        .HasForeignKey("LeaderId");

                    b.Navigation("Division");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Company", b =>
                {
                    b.Navigation("Divisions");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Division", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Employee", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("Departments");

                    b.Navigation("Divisions");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("OrganizacnaStrukturaFirmy.Models.Project", b =>
                {
                    b.Navigation("Departments");
                });
#pragma warning restore 612, 618
        }
    }
}
