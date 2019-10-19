﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pladeco.Web.Data;

namespace Pladeco.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Pladeco.Model.Area", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<decimal?>("Budget");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("create_date");

                    b.Property<int?>("create_uid");

                    b.Property<DateTime?>("write_date");

                    b.Property<int?>("write_uid");

                    b.HasKey("ID");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Pladeco.Model.DevAxis", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("create_date");

                    b.Property<int?>("create_uid");

                    b.Property<DateTime?>("write_date");

                    b.Property<int?>("write_uid");

                    b.HasKey("ID");

                    b.ToTable("DevAxes");
                });

            modelBuilder.Entity("Pladeco.Model.PaymentPlan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("ProjectID");

                    b.Property<string>("SolicitanteID");

                    b.Property<DateTime?>("create_date");

                    b.Property<int?>("create_uid");

                    b.Property<DateTime?>("write_date");

                    b.Property<int?>("write_uid");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("SolicitanteID");

                    b.ToTable("PaymentPlans");
                });

            modelBuilder.Entity("Pladeco.Model.Plan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Priority");

                    b.Property<int>("ProjectID");

                    b.Property<DateTime>("RealEndDate");

                    b.Property<DateTime>("RealStartDate");

                    b.Property<string>("ResponsableID");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("create_date");

                    b.Property<int?>("create_uid");

                    b.Property<DateTime?>("write_date");

                    b.Property<int?>("write_uid");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("ResponsableID");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("Pladeco.Model.PlanTask", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PlanID");

                    b.Property<int>("Priority");

                    b.Property<string>("ResponsableID");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("create_date");

                    b.Property<int?>("create_uid");

                    b.Property<DateTime?>("write_date");

                    b.Property<int?>("write_uid");

                    b.HasKey("ID");

                    b.HasIndex("PlanID");

                    b.HasIndex("ResponsableID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Pladeco.Model.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaID");

                    b.Property<string>("Description");

                    b.Property<int>("DevAxisID");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Priority");

                    b.Property<DateTime>("RealEndDate");

                    b.Property<DateTime>("RealStartDate");

                    b.Property<string>("ResponsableID");

                    b.Property<int>("ResponsableUnitID");

                    b.Property<int>("SectorID");

                    b.Property<string>("SolicitanteID");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime?>("create_date");

                    b.Property<int?>("create_uid");

                    b.Property<DateTime?>("write_date");

                    b.Property<int?>("write_uid");

                    b.HasKey("ID");

                    b.HasIndex("AreaID");

                    b.HasIndex("DevAxisID");

                    b.HasIndex("ResponsableID");

                    b.HasIndex("ResponsableUnitID");

                    b.HasIndex("SectorID");

                    b.HasIndex("SolicitanteID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Pladeco.Model.ResponsableUnit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("create_date");

                    b.Property<int?>("create_uid");

                    b.Property<DateTime?>("write_date");

                    b.Property<int?>("write_uid");

                    b.HasKey("ID");

                    b.ToTable("ResponsableUnits");
                });

            modelBuilder.Entity("Pladeco.Model.Sector", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("create_date");

                    b.Property<int?>("create_uid");

                    b.Property<DateTime?>("write_date");

                    b.Property<int?>("write_uid");

                    b.HasKey("ID");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("Pladeco.Model.Role", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<bool>("Active");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.ToTable("Role");

                    b.HasDiscriminator().HasValue("Role");
                });

            modelBuilder.Entity("Pladeco.Model.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("Active");

                    b.Property<int?>("AreaID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasIndex("AreaID");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pladeco.Model.PaymentPlan", b =>
                {
                    b.HasOne("Pladeco.Model.Project", "Project")
                        .WithMany("PaymentPlans")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pladeco.Model.User", "Solicitante")
                        .WithMany()
                        .HasForeignKey("SolicitanteID");
                });

            modelBuilder.Entity("Pladeco.Model.Plan", b =>
                {
                    b.HasOne("Pladeco.Model.Project", "Project")
                        .WithMany("Plans")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pladeco.Model.User", "Responsable")
                        .WithMany()
                        .HasForeignKey("ResponsableID");
                });

            modelBuilder.Entity("Pladeco.Model.PlanTask", b =>
                {
                    b.HasOne("Pladeco.Model.Plan", "Plan")
                        .WithMany("Tasks")
                        .HasForeignKey("PlanID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pladeco.Model.User", "Responsable")
                        .WithMany()
                        .HasForeignKey("ResponsableID");
                });

            modelBuilder.Entity("Pladeco.Model.Project", b =>
                {
                    b.HasOne("Pladeco.Model.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pladeco.Model.DevAxis", "DevAxis")
                        .WithMany()
                        .HasForeignKey("DevAxisID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pladeco.Model.User", "Responsable")
                        .WithMany()
                        .HasForeignKey("ResponsableID");

                    b.HasOne("Pladeco.Model.ResponsableUnit", "ResponsableUnit")
                        .WithMany()
                        .HasForeignKey("ResponsableUnitID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pladeco.Model.Sector", "Sector")
                        .WithMany()
                        .HasForeignKey("SectorID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pladeco.Model.User", "Solicitante")
                        .WithMany()
                        .HasForeignKey("SolicitanteID");
                });

            modelBuilder.Entity("Pladeco.Model.User", b =>
                {
                    b.HasOne("Pladeco.Model.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaID");
                });
#pragma warning restore 612, 618
        }
    }
}
