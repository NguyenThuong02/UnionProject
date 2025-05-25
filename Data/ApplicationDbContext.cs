using Microsoft.EntityFrameworkCore;
using YouthUnionManagement.Models;

namespace YouthUnionManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityParticipation> ActivityParticipations { get; set; }
        public DbSet<TrainingPoint> TrainingPoints { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình quan hệ giữa các bảng
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<ActivityParticipation>()
                .HasKey(ap => new { ap.MemberId, ap.ActivityId });

            modelBuilder.Entity<ActivityParticipation>()
                .HasOne(ap => ap.Member)
                .WithMany(m => m.ActivityParticipations)
                .HasForeignKey(ap => ap.MemberId);

            modelBuilder.Entity<ActivityParticipation>()
                .HasOne(ap => ap.Activity)
                .WithMany(a => a.ActivityParticipations)
                .HasForeignKey(ap => ap.ActivityId);

            modelBuilder.Entity<TrainingPoint>()
                .HasOne(tp => tp.Member)
                .WithMany(m => m.TrainingPoints)
                .HasForeignKey(tp => tp.MemberId);

            modelBuilder.Entity<Reward>()
                .HasOne(r => r.Member)
                .WithMany(m => m.Rewards)
                .HasForeignKey(r => r.MemberId);

            // Seed dữ liệu ban đầu
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Manager" },
                new Role { Id = 3, Name = "User" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "admin123", RoleId = 1 }
            );
        }
    }
}