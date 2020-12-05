using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pladeco.Domain;

namespace Pladeco.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Override default AspNet Identity table names
            modelBuilder.Entity<IdentityUser>(entity => { entity.ToTable(name: "Users"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Plans)
                .WithOne(p => p.Project)
                .HasForeignKey(p => p.ProjectID);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.PaymentPlans)
                .WithOne(p => p.Project)
                .HasForeignKey(p => p.ProjectID);

            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Tasks)
                .WithOne(p => p.Plan)
                .HasForeignKey(p => p.PlanID);

            modelBuilder.Entity<Project>()
                .HasOne<User>(s => s.Responsable)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne<Typology>(s => s.Typology)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne<Area>(s => s.Area)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne<User>(s => s.Solicitante)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne<Sector>(s => s.Sector)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne<ResponsableUnit>(s => s.ResponsableUnit)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne<DevAxis>(s => s.DevAxis)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne<User>(s => s.ResponsableBudget)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Plan>()
               .HasOne<User>(s => s.Responsable)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PlanTask>()
                .HasOne<User>(s => s.Responsable)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //var cascadeFKs = modelBuilder.Model
            //    .G­etEntityTypes()
            //    .SelectMany(t => t.GetForeignKeys())
            //    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            //foreach (var fk in cascadeFKs)
            //{
            //    fk.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            //modelBuilder.Entity<UserRole>(userRole =>
            //{
            //    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            //    userRole.HasOne(ur => ur.Role)
            //        .WithMany(r => r.UserRoles)
            //        .HasForeignKey(ur => ur.RoleId)
            //        .IsRequired();

            //    userRole.HasOne(ur => ur.User)
            //        .WithMany(r => r.UserRoles)
            //        .HasForeignKey(ur => ur.UserId)
            //        .IsRequired();
            //});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PlanTask> Tasks { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<PaymentPlan> PaymentPlans { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<ResponsableUnit> ResponsableUnits { get; set; }
        public DbSet<DevAxis> DevAxes { get; set; }
        public DbSet<Typology> Typologies { get; set; }
        public DbSet<TypologyStage> TypologyStages { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Budget> Budgets { get; set; }

    }
}
