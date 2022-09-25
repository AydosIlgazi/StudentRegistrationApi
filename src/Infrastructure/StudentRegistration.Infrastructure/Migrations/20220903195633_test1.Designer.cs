﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentRegistration.Infrastructure;

#nullable disable

namespace StudentRegistration.Infrastructure.Migrations
{
    [DbContext(typeof(StudentRegistrationContext))]
    [Migration("20220903195633_test1")]
    partial class test1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("StudentRegistration.Domain.Aggregates.Term", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnrollmentActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("StudentRegistration.Domain.Aggregates.Term", b =>
                {
                    b.OwnsOne("StudentRegistration.Domain.ValueObjects.Semester", "Semester", b1 =>
                        {
                            b1.Property<int>("TermId")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("SemesterType")
                                .HasColumnType("INTEGER")
                                .HasColumnName("SemesterType");

                            b1.Property<int>("Year")
                                .HasColumnType("INTEGER")
                                .HasColumnName("Year");

                            b1.HasKey("TermId");

                            b1.ToTable("Terms");

                            b1.WithOwner()
                                .HasForeignKey("TermId");
                        });

                    b.OwnsOne("StudentRegistration.Domain.ValueObjects.TermWeeklySlots", "LectureDaysAndSlots", b1 =>
                        {
                            b1.Property<int>("TermId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("TermId");

                            b1.ToTable("Terms");

                            b1.WithOwner()
                                .HasForeignKey("TermId");

                            b1.OwnsMany("StudentRegistration.Domain.ValueObjects.DailySlots", "DailySlots", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Day")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("TermWeeklySlotsTermId")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("Id");

                                    b2.HasIndex("TermWeeklySlotsTermId");

                                    b2.ToTable("DailySlots");

                                    b2.WithOwner()
                                        .HasForeignKey("TermWeeklySlotsTermId");

                                    b2.OwnsMany("StudentRegistration.Domain.ValueObjects.Slot", "Slots", b3 =>
                                        {
                                            b3.Property<int>("DailySlotsId")
                                                .HasColumnType("INTEGER");

                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("INTEGER");

                                            b3.Property<string>("SlotName")
                                                .IsRequired()
                                                .HasColumnType("TEXT");

                                            b3.HasKey("DailySlotsId", "Id");

                                            b3.ToTable("Slot");

                                            b3.WithOwner()
                                                .HasForeignKey("DailySlotsId");

                                            b3.OwnsOne("StudentRegistration.Domain.ValueObjects.SlotTime", "EndTime", b4 =>
                                                {
                                                    b4.Property<int>("SlotDailySlotsId")
                                                        .HasColumnType("INTEGER");

                                                    b4.Property<int>("SlotId")
                                                        .HasColumnType("INTEGER");

                                                    b4.Property<int>("Hour")
                                                        .HasColumnType("INTEGER");

                                                    b4.Property<int>("Miniute")
                                                        .HasColumnType("INTEGER");

                                                    b4.HasKey("SlotDailySlotsId", "SlotId");

                                                    b4.ToTable("Slot");

                                                    b4.WithOwner()
                                                        .HasForeignKey("SlotDailySlotsId", "SlotId");
                                                });

                                            b3.OwnsOne("StudentRegistration.Domain.ValueObjects.SlotTime", "StartTime", b4 =>
                                                {
                                                    b4.Property<int>("SlotDailySlotsId")
                                                        .HasColumnType("INTEGER");

                                                    b4.Property<int>("SlotId")
                                                        .HasColumnType("INTEGER");

                                                    b4.Property<int>("Hour")
                                                        .HasColumnType("INTEGER");

                                                    b4.Property<int>("Miniute")
                                                        .HasColumnType("INTEGER");

                                                    b4.HasKey("SlotDailySlotsId", "SlotId");

                                                    b4.ToTable("Slot");

                                                    b4.WithOwner()
                                                        .HasForeignKey("SlotDailySlotsId", "SlotId");
                                                });

                                            b3.Navigation("EndTime")
                                                .IsRequired();

                                            b3.Navigation("StartTime")
                                                .IsRequired();
                                        });

                                    b2.Navigation("Slots");
                                });

                            b1.Navigation("DailySlots");
                        });

                    b.Navigation("LectureDaysAndSlots")
                        .IsRequired();

                    b.Navigation("Semester")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
