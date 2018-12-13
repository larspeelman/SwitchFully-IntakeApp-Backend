﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SwitchFully.IntakeApp.Data;

namespace SwitchFully.IntakeApp.Data.Migrations
{
    [DbContext(typeof(SwitchFullyIntakeAppContext))]
    [Migration("20181212130042_refactored candidate")]
    partial class refactoredcandidate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SwitchFully.IntakeApp.Domain.Campaigns.Campaign", b =>
                {
                    b.Property<Guid>("CampaignId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Client");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("CampaignId");

                    b.ToTable("Campaign");
                });

            modelBuilder.Entity("SwitchFully.IntakeApp.Domain.Candidates.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("LinkedIn");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("SwitchFully.IntakeApp.Domain.JobApplications.JobApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CampaignId");

                    b.Property<Guid>("CandidateId");

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("StatusId");

                    b.ToTable("JobApplication");
                });

            modelBuilder.Entity("SwitchFully.IntakeApp.Domain.JobApplications.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("SwitchFully.IntakeApp.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<DateTime?>("LastLogon");

                    b.Property<string>("LastName");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SwitchFully.IntakeApp.Domain.Candidates.Candidate", b =>
                {
                    b.OwnsOne("System.Net.Mail.MailAddress", "Email", b1 =>
                        {
                            b1.Property<Guid>("CandidateId");

                            b1.Property<string>("Address")
                                .HasColumnName("Email");

                            b1.ToTable("Candidates");

                            b1.HasOne("SwitchFully.IntakeApp.Domain.Candidates.Candidate")
                                .WithOne("Email")
                                .HasForeignKey("System.Net.Mail.MailAddress", "CandidateId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("SwitchFully.IntakeApp.Domain.JobApplications.JobApplication", b =>
                {
                    b.HasOne("SwitchFully.IntakeApp.Domain.Campaigns.Campaign", "Campaign")
                        .WithMany()
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SwitchFully.IntakeApp.Domain.Candidates.Candidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SwitchFully.IntakeApp.Domain.JobApplications.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SwitchFully.IntakeApp.Domain.Users.User", b =>
                {
                    b.OwnsOne("SwitchFully.IntakeApp.Domain.Users.UserSecurity", "SecurePassword", b1 =>
                        {
                            b1.Property<Guid?>("UserId");

                            b1.Property<string>("PasswordHash")
                                .HasColumnName("PassWord");

                            b1.Property<string>("Salt")
                                .HasColumnName("SecPass");

                            b1.ToTable("Users");

                            b1.HasOne("SwitchFully.IntakeApp.Domain.Users.User")
                                .WithOne("SecurePassword")
                                .HasForeignKey("SwitchFully.IntakeApp.Domain.Users.UserSecurity", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("System.Net.Mail.MailAddress", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId");

                            b1.Property<string>("Address")
                                .HasColumnName("Email");

                            b1.ToTable("Users");

                            b1.HasOne("SwitchFully.IntakeApp.Domain.Users.User")
                                .WithOne("Email")
                                .HasForeignKey("System.Net.Mail.MailAddress", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
