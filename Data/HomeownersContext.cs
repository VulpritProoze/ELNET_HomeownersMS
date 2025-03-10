using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HomeownersMS.Models;

namespace HomeownersMS.Data
{
    public class HomeownersContext : DbContext
    {
        public HomeownersContext(DbContextOptions<HomeownersContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Resident> Residents { get; set; } = default!;
        public DbSet<Staff> Staffs { get; set; } = default!;
        public DbSet<Admin> Admins { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;

        public DbSet<ServiceRequest> ServiceRequests { get; set; } = default!;

        public DbSet<Facility> Facilities { get; set; } = default!;
        public DbSet<FacilityRequest> FacilityRequests { get; set; } = default!;
        public DbSet<CommunityPost> CommunityPosts { get; set; } = default!;
        public DbSet<CommunityComment> CommunityComments { get; set; } = default!;
        public DbSet<Announcement> Announcements { get; set; } = default!;
        public DbSet<Resource> Resources { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Staff)
                .WithOne(s => s.User)
                .HasForeignKey<Staff>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Resident)
                .WithOne(r => r.User)
                .HasForeignKey<Resident>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Staff)
                .WithMany(st => st.Services) // Ensure Staff has a collection of Services
                .HasForeignKey(s => s.StaffId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Resident)
                .WithMany(r => r.ServiceRequests) // Ensure Resident has a collection of ServiceRequests
                .HasForeignKey(sr => sr.RequestedBy)
                .OnDelete(DeleteBehavior.Cascade); // If a Resident is deleted, delete their ServiceRequests

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Service)
                .WithMany(s => s.ServiceRequests) // Ensure Service has a collection of ServiceRequests
                .HasForeignKey(sr => sr.ServiceId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Facility>()
                .Property(f => f.PricePerHour)
                .HasColumnType("decimal(10,2)"); // Ensure proper decimal format

            modelBuilder.Entity<FacilityRequest>()
                .HasOne(fr => fr.Resident)
                .WithMany() // Assuming one resident can have multiple facility requests
                .HasForeignKey(fr => fr.ResidentId)
                .OnDelete(DeleteBehavior.SetNull); // Prevent cascade delete, keep history

            modelBuilder.Entity<FacilityRequest>()
                .HasOne(fr => fr.Facility)
                .WithMany() // Assuming one facility can have multiple requests
                .HasForeignKey(fr => fr.FacilityId)
                .OnDelete(DeleteBehavior.SetNull); // Prevent cascade delete, keep history

            modelBuilder.Entity<CommunityPost>()
                .HasOne(cp => cp.User)
                .WithMany() // Assuming a user can create multiple posts
                .HasForeignKey(cp => cp.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommunityComment>()
                .HasOne(cc => cc.CommunityPost)
                .WithMany() // Assuming a post can have multiple comments
                .HasForeignKey(cc => cc.CommunityPostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Admin)
                .WithMany() // Assuming an Admin can create multiple resources
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.Admin)
                .WithMany() // Assuming an Admin can create multiple announcements
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Resident>().ToTable("Resident");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<ServiceRequest>().ToTable("ServiceRequest");
            modelBuilder.Entity<Facility>().ToTable("Facility");
            modelBuilder.Entity<FacilityRequest>().ToTable("FacilityRequest");
            modelBuilder.Entity<CommunityPost>().ToTable("CommunityPost");
            modelBuilder.Entity<CommunityComment>().ToTable("CommunityComment");
            modelBuilder.Entity<Announcement>().ToTable("Announcement");
            modelBuilder.Entity<Resource>().ToTable("Resource");

        }

    }
}
