﻿// <auto-generated />
using System;
using Data_Access_Layer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data_Access_Layer.Migrations.Application
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data_Access_Layer.Models.AvailableTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Recurring")
                        .HasColumnType("int");

                    b.Property<Guid?>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("AvailableTime");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.ReserveTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ReservationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("ReserveTime");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("strName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.AvailableTime", b =>
                {
                    b.HasOne("Data_Access_Layer.Models.Resource", null)
                        .WithMany("TimeSlot")
                        .HasForeignKey("ResourceId");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.Reservation", b =>
                {
                    b.HasOne("Data_Access_Layer.Models.Resource", null)
                        .WithMany("Reservations")
                        .HasForeignKey("ResourceId");
                });

            modelBuilder.Entity("Data_Access_Layer.Models.ReserveTime", b =>
                {
                    b.HasOne("Data_Access_Layer.Models.Reservation", null)
                        .WithMany("Timeslot")
                        .HasForeignKey("ReservationId");
                });
#pragma warning restore 612, 618
        }
    }
}
