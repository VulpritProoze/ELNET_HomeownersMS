using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HomeownersMS.Models;
using System.Text.Json; // For JSON serialization

namespace HomeownersMS.Data
{
    public class HomeownersContext : DbContext
    {
        public HomeownersContext(DbContextOptions<HomeownersContext> options)
            : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Resident> Residents { get; set; } = default!;
        public DbSet<Staff> Staffs { get; set; } = default!;
        public DbSet<Admin> Admins { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<ServiceRequest> ServiceRequests { get; set; } = default!;
        public DbSet<Facility> Facilities { get; set; } = default!;
        public DbSet<FacilityReview> FacilityReviews { get; set; } = default!;
        public DbSet<FacilityRequest> FacilityRequests { get; set; } = default!;
        public DbSet<CommunityPost> CommunityPosts { get; set; } = default!;

        public DbSet<CommunityVote> CommunityVotes { get; set; } = default!;
        public DbSet<CommunityComment> CommunityComments { get; set; } = default!;
        public DbSet<Resource> Resources { get; set; } = default!;
        public DbSet<Event> Events { get; set; } = default!;

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
                .WithMany(st => st.Services)
                .HasForeignKey(s => s.StaffId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Resident)
                .WithMany(r => r.ServiceRequests)
                .HasForeignKey(sr => sr.RequestedBy)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Service)
                .WithMany(s => s.ServiceRequests)
                .HasForeignKey(sr => sr.ServiceId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Facility>()
                .HasMany(f => f.FacilityReviews)
                .WithOne(r => r.Facility)
                .HasForeignKey(r => r.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FacilityRequest>()
                .HasOne(fr => fr.Resident)
                .WithMany()
                .HasForeignKey(fr => fr.ResidentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<FacilityRequest>()
                .HasOne(fr => fr.Facility)
                .WithMany()
                .HasForeignKey(fr => fr.FacilityId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<FacilityReview>()
                .HasOne(fr => fr.Resident)
                .WithMany()
                .HasForeignKey(fr => fr.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure CommunityPost relationships
            modelBuilder.Entity<CommunityPost>()
                .HasOne(cp => cp.User)
                .WithMany(u => u.CommunityPosts)  // Assuming User has ICollection<CommunityPost> Posts
                .HasForeignKey(cp => cp.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure CommunityVote relationships
            modelBuilder.Entity<CommunityVote>(entity =>
            {
                // Primary key
                entity.HasKey(v => v.CommunityVoteId);

                // Relationship with CommunityPost
                entity.HasOne(v => v.Post)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(v => v.CommunityPostId)
                    .OnDelete(DeleteBehavior.Cascade); // Delete votes when post is deleted

                // Relationship with User
                entity.HasOne(v => v.User)
                    .WithMany(u => u.CommunityVotes)
                    .HasForeignKey(v => v.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent user deletion if they have votes

                // Add unique constraint to prevent duplicate votes
                entity.HasIndex(v => new { v.CommunityPostId, v.UserId })
                    .IsUnique();
            });

            // Configure CommunityComment relationships
            modelBuilder.Entity<CommunityComment>()
                .HasOne(cc => cc.CommunityPost)
                .WithMany(cp => cp.Comments)  // Links to the Comments collection in CommunityPost
                .HasForeignKey(cc => cc.CommunityPostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommunityComment>()
                .HasOne(cc => cc.User)
                .WithMany(u => u.CommunityComments)  // Assuming User has ICollection<CommunityComment> Comments
                .HasForeignKey(cc => cc.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Typically restrict delete for comments when user is deleted

            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Admin)
                .WithMany()
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.Admin)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);

            // Add Event configuration
            modelBuilder.Entity<Event>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.EventId);

                // Relationship with User
                entity.HasOne(e => e.User)
                    .WithMany(r => r.Events) // Assuming User has ICollection<Event> Events
                    .HasForeignKey(e => e.CreatedBy)
                    .OnDelete(DeleteBehavior.Cascade);

                // Configure the AdditionalServices dictionary to be stored as JSON
                entity.Property(e => e.AdditionalServices)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                        v => JsonSerializer.Deserialize<Dictionary<string, AdditionalServiceDetails>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, AdditionalServiceDetails>()
                    );
            });

            modelBuilder.Entity<Event>()
                .HasOne(e => e.FacilityRequest)
                .WithMany()
                .HasForeignKey(e => e.FacilityRequestId) // Specify the entity type explicitly
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Resident>().ToTable("Resident");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<ServiceRequest>().ToTable("ServiceRequest");
            modelBuilder.Entity<Facility>().ToTable("Facility");
            modelBuilder.Entity<FacilityRequest>().ToTable("FacilityRequest");
            modelBuilder.Entity<FacilityReview>().ToTable("FacilityReview");
            modelBuilder.Entity<CommunityPost>().ToTable("CommunityPost");
            modelBuilder.Entity<CommunityVote>().ToTable("CommunityVote");
            modelBuilder.Entity<CommunityComment>().ToTable("CommunityComment");
            modelBuilder.Entity<Announcement>().ToTable("Announcement");
            modelBuilder.Entity<Resource>().ToTable("Resource");
            modelBuilder.Entity<Event>().ToTable("Event");
        }
    }
}
