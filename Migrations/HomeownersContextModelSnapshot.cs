﻿// <auto-generated />
using System;
using HomeownersMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HomeownersMS.Migrations
{
    [DbContext(typeof(HomeownersContext))]
    partial class HomeownersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("HomeownersMS.Models.Admin", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactNo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Job")
                        .HasColumnType("TEXT");

                    b.Property<string>("LName")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.Announcement", b =>
                {
                    b.Property<int>("AnnouncementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("AnnouncementId");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Announcement", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.CommunityComment", b =>
                {
                    b.Property<int>("CommunityCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommunityPostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("CommunityCommentId");

                    b.HasIndex("CommunityPostId");

                    b.ToTable("CommunityComment", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.CommunityPost", b =>
                {
                    b.Property<int>("CommunityPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("CommunityPostId");

                    b.HasIndex("CreatedBy");

                    b.ToTable("CommunityPost", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.Facility", b =>
                {
                    b.Property<int>("FacilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("FacilityImage")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<float?>("PricePerHour")
                        .HasColumnType("REAL");

                    b.HasKey("FacilityId");

                    b.ToTable("Facility", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.FacilityRequest", b =>
                {
                    b.Property<int>("FacilityRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("FacilityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RequestCompletionDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReservationDate")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan?>("ReservationTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ResidentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("FacilityRequestId");

                    b.HasIndex("FacilityId");

                    b.HasIndex("ResidentId");

                    b.ToTable("FacilityRequest", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.FacilityReview", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FacilityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ReviewId");

                    b.HasIndex("FacilityId");

                    b.ToTable("FacilityReviews");
                });

            modelBuilder.Entity("HomeownersMS.Models.Resident", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactNo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("MoveInDate")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Resident", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.Resource", b =>
                {
                    b.Property<int>("ResourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ResourceId");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Resource", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("StaffId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ServiceId");

                    b.HasIndex("StaffId");

                    b.ToTable("Service", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.ServiceRequest", b =>
                {
                    b.Property<int>("ServiceRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RequestedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RequestedBy")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ServiceRequestId");

                    b.HasIndex("RequestedBy");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceRequest", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.Staff", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactNo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Job")
                        .HasColumnType("TEXT");

                    b.Property<string>("LName")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Staff", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Privilege")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("HomeownersMS.Models.Admin", b =>
                {
                    b.HasOne("HomeownersMS.Models.User", "User")
                        .WithOne("Admin")
                        .HasForeignKey("HomeownersMS.Models.Admin", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HomeownersMS.Models.Announcement", b =>
                {
                    b.HasOne("HomeownersMS.Models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("HomeownersMS.Models.CommunityComment", b =>
                {
                    b.HasOne("HomeownersMS.Models.CommunityPost", "CommunityPost")
                        .WithMany()
                        .HasForeignKey("CommunityPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommunityPost");
                });

            modelBuilder.Entity("HomeownersMS.Models.CommunityPost", b =>
                {
                    b.HasOne("HomeownersMS.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("HomeownersMS.Models.FacilityRequest", b =>
                {
                    b.HasOne("HomeownersMS.Models.Facility", "Facility")
                        .WithMany()
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("HomeownersMS.Models.Resident", "Resident")
                        .WithMany()
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Facility");

                    b.Navigation("Resident");
                });

            modelBuilder.Entity("HomeownersMS.Models.FacilityReview", b =>
                {
                    b.HasOne("HomeownersMS.Models.Facility", "Facility")
                        .WithMany("FacilityReviews")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facility");
                });

            modelBuilder.Entity("HomeownersMS.Models.Resident", b =>
                {
                    b.HasOne("HomeownersMS.Models.User", "User")
                        .WithOne("Resident")
                        .HasForeignKey("HomeownersMS.Models.Resident", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HomeownersMS.Models.Resource", b =>
                {
                    b.HasOne("HomeownersMS.Models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("HomeownersMS.Models.Service", b =>
                {
                    b.HasOne("HomeownersMS.Models.Staff", "Staff")
                        .WithMany("Services")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("HomeownersMS.Models.ServiceRequest", b =>
                {
                    b.HasOne("HomeownersMS.Models.Resident", "Resident")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("RequestedBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeownersMS.Models.Service", "Service")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Resident");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("HomeownersMS.Models.Staff", b =>
                {
                    b.HasOne("HomeownersMS.Models.User", "User")
                        .WithOne("Staff")
                        .HasForeignKey("HomeownersMS.Models.Staff", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HomeownersMS.Models.Facility", b =>
                {
                    b.Navigation("FacilityReviews");
                });

            modelBuilder.Entity("HomeownersMS.Models.Resident", b =>
                {
                    b.Navigation("ServiceRequests");
                });

            modelBuilder.Entity("HomeownersMS.Models.Service", b =>
                {
                    b.Navigation("ServiceRequests");
                });

            modelBuilder.Entity("HomeownersMS.Models.Staff", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("HomeownersMS.Models.User", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Resident");

                    b.Navigation("Staff");
                });
#pragma warning restore 612, 618
        }
    }
}
