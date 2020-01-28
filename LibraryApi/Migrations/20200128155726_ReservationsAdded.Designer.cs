﻿// <auto-generated />
using System;
using LibraryApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryApi.Migrations
{
    [DbContext(typeof(LibraryDataContext))]
    [Migration("20200128155726_ReservationsAdded")]
    partial class ReservationsAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryApi.Domain.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InInventory")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Threau",
                            Genre = "Philosophy",
                            InInventory = true,
                            NumberOfPages = 322,
                            Title = "Walden"
                        },
                        new
                        {
                            Id = 2,
                            Author = "DJ Spooky That Subliminal Kid",
                            Genre = "Music",
                            InInventory = true,
                            NumberOfPages = 180,
                            Title = "Rythm Science"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Emerson",
                            Genre = "Philosophy",
                            InInventory = true,
                            NumberOfPages = 182,
                            Title = "Nature"
                        });
                });

            modelBuilder.Entity("LibraryApi.Domain.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Books")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("For")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReservationCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
