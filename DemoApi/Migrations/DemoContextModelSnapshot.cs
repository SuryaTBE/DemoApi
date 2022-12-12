﻿// <auto-generated />
using System;
using DemoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoApi.Migrations
{
    [DbContext(typeof(DemoContext))]
    partial class DemoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DemoApi.Models.AdminTbl", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<string>("AdminName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("AdminTbls");
                });

            modelBuilder.Entity("DemoApi.Models.BookingTbl", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"), 1L, 1);

                    b.Property<int>("AmountTotal")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("MovieName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NoOfTickets")
                        .HasColumnType("int");

                    b.Property<string>("On")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeatNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("BookingTbl");
                });

            modelBuilder.Entity("DemoApi.Models.MovieTbl", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"), 1L, 1);

                    b.Property<int?>("Cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MovieName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("capacity")
                        .HasColumnType("int");

                    b.HasKey("MovieId");

                    b.ToTable("MovieTbls");
                });

            modelBuilder.Entity("DemoApi.Models.OrderDetailTbl", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"), 1L, 1);

                    b.Property<int?>("Cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("MovieDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("MovieName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoOfTickets")
                        .HasColumnType("int");

                    b.Property<int?>("OrderMasterId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderMasterTblOrderMasterId")
                        .HasColumnType("int");

                    b.Property<string>("SeatNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("bookingtblBookingId")
                        .HasColumnType("int");

                    b.Property<int?>("usertblUserId")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderMasterTblOrderMasterId");

                    b.HasIndex("bookingtblBookingId");

                    b.HasIndex("usertblUserId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("DemoApi.Models.OrderMasterTbl", b =>
                {
                    b.Property<int>("OrderMasterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderMasterId"), 1L, 1);

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("CardNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Paid")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderMasterId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderMasterTbls");
                });

            modelBuilder.Entity("DemoApi.Models.UserTbl", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserTbls");
                });

            modelBuilder.Entity("DemoApi.Models.BookingTbl", b =>
                {
                    b.HasOne("DemoApi.Models.MovieTbl", "Movie")
                        .WithMany("Bookings")
                        .HasForeignKey("MovieId");

                    b.HasOne("DemoApi.Models.UserTbl", "User")
                        .WithMany("BookingTbls")
                        .HasForeignKey("UserId");

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DemoApi.Models.OrderDetailTbl", b =>
                {
                    b.HasOne("DemoApi.Models.OrderMasterTbl", "OrderMasterTbl")
                        .WithMany("Details")
                        .HasForeignKey("OrderMasterTblOrderMasterId");

                    b.HasOne("DemoApi.Models.BookingTbl", "bookingtbl")
                        .WithMany("OrderDetails")
                        .HasForeignKey("bookingtblBookingId");

                    b.HasOne("DemoApi.Models.UserTbl", "usertbl")
                        .WithMany("Details")
                        .HasForeignKey("usertblUserId");

                    b.Navigation("OrderMasterTbl");

                    b.Navigation("bookingtbl");

                    b.Navigation("usertbl");
                });

            modelBuilder.Entity("DemoApi.Models.OrderMasterTbl", b =>
                {
                    b.HasOne("DemoApi.Models.UserTbl", "user")
                        .WithMany("OrderMasterTbls")
                        .HasForeignKey("UserId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("DemoApi.Models.BookingTbl", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("DemoApi.Models.MovieTbl", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("DemoApi.Models.OrderMasterTbl", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("DemoApi.Models.UserTbl", b =>
                {
                    b.Navigation("BookingTbls");

                    b.Navigation("Details");

                    b.Navigation("OrderMasterTbls");
                });
#pragma warning restore 612, 618
        }
    }
}
