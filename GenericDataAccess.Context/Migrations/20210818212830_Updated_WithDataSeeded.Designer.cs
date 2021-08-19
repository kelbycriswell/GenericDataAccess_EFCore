﻿// <auto-generated />
using System;
using GenericDataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GenericDataAccess.Context.Migrations
{
    [DbContext(typeof(TestDb))]
    [Migration("20210818212830_Updated_WithDataSeeded")]
    partial class Updated_WithDataSeeded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GenericDataAccess.Context.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PrimaryPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateAbbr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            City = "Layton",
                            DateOfBirth = new DateTimeOffset(new DateTime(2000, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)),
                            Deleted = false,
                            Email = "123@test.comg",
                            FName = "Test",
                            Gender = 0,
                            LName = "Person",
                            MI = "A",
                            ModifiedOn = new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 214, DateTimeKind.Unspecified).AddTicks(1746), new TimeSpan(0, -6, 0, 0, 0)),
                            PrimaryPhone = "801-336-3839",
                            StateAbbr = "UT",
                            StreetAddress = "437 North Wasatch Drive",
                            ZipCode = "84041"
                        });
                });

            modelBuilder.Entity("GenericDataAccess.Context.LineItem", b =>
                {
                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("LineNo")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.HasKey("OrderID", "LineNo");

                    b.HasIndex("ProductID");

                    b.ToTable("LineItems");

                    b.HasData(
                        new
                        {
                            OrderID = 1,
                            LineNo = 1,
                            Deleted = false,
                            ModifiedOn = new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 217, DateTimeKind.Unspecified).AddTicks(5483), new TimeSpan(0, -6, 0, 0, 0)),
                            ProductID = 1,
                            Qty = 3
                        },
                        new
                        {
                            OrderID = 1,
                            LineNo = 2,
                            Deleted = false,
                            ModifiedOn = new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 217, DateTimeKind.Unspecified).AddTicks(6025), new TimeSpan(0, -6, 0, 0, 0)),
                            ProductID = 2,
                            Qty = 1
                        });
                });

            modelBuilder.Entity("GenericDataAccess.Context.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CustomerID = 1,
                            Deleted = false,
                            ModifiedOn = new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 217, DateTimeKind.Unspecified).AddTicks(2295), new TimeSpan(0, -6, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("GenericDataAccess.Context.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CostPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OnHand")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CostPerUnit = 5.00m,
                            Deleted = false,
                            Description = "An Item to test with",
                            ModifiedOn = new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 216, DateTimeKind.Unspecified).AddTicks(8538), new TimeSpan(0, -6, 0, 0, 0)),
                            Name = "Test Item",
                            OnHand = 12
                        },
                        new
                        {
                            ID = 2,
                            CostPerUnit = 10.00m,
                            Deleted = false,
                            Description = "An Item to test with",
                            ModifiedOn = new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 216, DateTimeKind.Unspecified).AddTicks(9922), new TimeSpan(0, -6, 0, 0, 0)),
                            Name = "Another Test Item",
                            OnHand = 5
                        });
                });

            modelBuilder.Entity("GenericDataAccess.Context.LineItem", b =>
                {
                    b.HasOne("GenericDataAccess.Context.Order", null)
                        .WithMany("LineItems")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GenericDataAccess.Context.Product", "Item")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("GenericDataAccess.Context.Order", b =>
                {
                    b.HasOne("GenericDataAccess.Context.Customer", null)
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenericDataAccess.Context.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GenericDataAccess.Context.Order", b =>
                {
                    b.Navigation("LineItems");
                });
#pragma warning restore 612, 618
        }
    }
}