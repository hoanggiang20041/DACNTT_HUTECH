using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Chamsoc.Models;

namespace Chamsoc.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Caregiver> Caregivers { get; set; }
        public DbSet<Senior> Seniors { get; set; }
        public DbSet<CareJob> CareJobs { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Standardize table names (plural snake_case)
            builder.Entity<Caregiver>().ToTable("caregivers");
            builder.Entity<Senior>().ToTable("seniors");
            builder.Entity<CareJob>().ToTable("care_jobs");
            builder.Entity<Complaint>().ToTable("complaints");
            builder.Entity<Notification>().ToTable("notifications");
            builder.Entity<Payment>().ToTable("payments");
            builder.Entity<Rating>().ToTable("ratings");
            builder.Entity<Service>().ToTable("services");
            builder.Entity<Transaction>().ToTable("transactions");

            // Professional Identity table names
            builder.Entity<ApplicationUser>().ToTable("users");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().ToTable("roles");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().ToTable("user_roles");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>().ToTable("user_claims");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>().ToTable("user_logins");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>().ToTable("role_claims");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>().ToTable("user_tokens");

            // Configure ApplicationUser RoleId length and nullability
            builder.Entity<ApplicationUser>()
                .Property(u => u.RoleId)
                .HasMaxLength(36)
                .IsRequired(false);

            // Configure relationships
            builder.Entity<CareJob>()
                .HasOne(j => j.Senior)
                .WithMany(s => s.CareJobs)
                .HasForeignKey(j => j.SeniorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CareJob>()
                .HasOne(j => j.Caregiver)
                .WithMany(c => c.CareJobs)
                .HasForeignKey(j => j.CaregiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CareJob>()
                .HasOne(j => j.Service)
                .WithMany(s => s.CareJobs)
                .HasForeignKey(j => j.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Complaint>()
                .HasOne(c => c.Job)
                .WithMany(j => j.Complaints)
                .HasForeignKey(c => c.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Complaint>()
                .HasOne(c => c.Caregiver)
                .WithMany()
                .HasForeignKey(c => c.CaregiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Complaint>()
                .HasOne(c => c.Senior)
                .WithMany()
                .HasForeignKey(c => c.SeniorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rating>()
                .HasOne(r => r.Job)
                .WithMany()
                .HasForeignKey(r => r.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rating>()
                .HasOne(r => r.Caregiver)
                .WithMany()
                .HasForeignKey(r => r.CaregiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rating>()
                .HasOne(r => r.Senior)
                .WithMany()
                .HasForeignKey(r => r.SeniorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Notification>()
                .HasOne(n => n.Job)
                .WithMany(j => j.Notifications)
                .HasForeignKey(n => n.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình cho Payment
            builder.Entity<Payment>()
                .HasOne(p => p.Job)
                .WithMany()
                .HasForeignKey(p => p.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Payment>()
                .HasOne(p => p.Senior)
                .WithMany()
                .HasForeignKey(p => p.SeniorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Payment>()
                .HasOne(p => p.Caregiver)
                .WithMany()
                .HasForeignKey(p => p.CaregiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình cho Transaction
            builder.Entity<Transaction>()
                .HasOne(t => t.CareJob)
                .WithMany()
                .HasForeignKey(t => t.CareJobId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Caregiver>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Caregiver>(c => c.UserId);

            builder.Entity<Senior>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Senior>(s => s.UserId);
        }
    }
} 