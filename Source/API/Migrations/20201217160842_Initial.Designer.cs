﻿// <auto-generated />
using System;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(NearbyProduceContext))]
    [Migration("20201217160842_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("API.Models.Marketplace", b =>
                {
                    b.Property<int>("MarketplaceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("MarketplaceID");

                    b.ToTable("Marketplace");

                    b.HasData(
                        new
                        {
                            MarketplaceID = 1,
                            EndDateTime = new DateTime(2020, 12, 26, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Location = "Heden",
                            Name = "Göteborgs Bakluckeloppis",
                            StartDateTime = new DateTime(2020, 12, 26, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("API.Models.MarketplaceSeller", b =>
                {
                    b.Property<int>("MarketplaceID")
                        .HasColumnType("int");

                    b.Property<int>("SellerID")
                        .HasColumnType("int");

                    b.HasKey("MarketplaceID", "SellerID");

                    b.HasIndex("SellerID");

                    b.ToTable("MarketplaceSeller");

                    b.HasData(
                        new
                        {
                            MarketplaceID = 1,
                            SellerID = 1
                        });
                });

            modelBuilder.Entity("API.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SellerPageID")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.HasIndex("SellerPageID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            Name = "Potatis"
                        });
                });

            modelBuilder.Entity("API.Models.SellerPage", b =>
                {
                    b.Property<int>("SellerPageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SellerUserID")
                        .HasColumnType("int");

                    b.HasKey("SellerPageID");

                    b.HasIndex("SellerUserID");

                    b.ToTable("SellerPage");

                    b.HasData(
                        new
                        {
                            SellerPageID = 1,
                            Name = "Arnes Online-Gård",
                            SellerUserID = 2
                        });
                });

            modelBuilder.Entity("API.Models.SellerPageProduct", b =>
                {
                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("SellerPageID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductID", "SellerPageID");

                    b.HasIndex("SellerPageID");

                    b.ToTable("SellerPageProduct");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            SellerPageID = 1,
                            Price = 2,
                            Stock = 10
                        });
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Password = "lösen123",
                            Type = 2,
                            Username = "JanneBonde07"
                        },
                        new
                        {
                            UserID = 2,
                            Password = "lösen123",
                            Type = 1,
                            Username = "Bengtan555"
                        });
                });

            modelBuilder.Entity("API.Models.UserProduct", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("UserProduct");

                    b.HasData(
                        new
                        {
                            UserID = 2,
                            ProductID = 1
                        });
                });

            modelBuilder.Entity("API.Models.MarketplaceSeller", b =>
                {
                    b.HasOne("API.Models.Marketplace", "Marketplace")
                        .WithMany("MarketplaceSellers")
                        .HasForeignKey("MarketplaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.User", "Seller")
                        .WithMany("MarketplaceSellers")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Marketplace");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("API.Models.Product", b =>
                {
                    b.HasOne("API.Models.SellerPage", null)
                        .WithMany("Products")
                        .HasForeignKey("SellerPageID");
                });

            modelBuilder.Entity("API.Models.SellerPage", b =>
                {
                    b.HasOne("API.Models.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("API.Models.SellerPageProduct", b =>
                {
                    b.HasOne("API.Models.Product", "product")
                        .WithMany("SellerPageProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("API.Models.SellerPage", "sellerPage")
                        .WithMany("SellerPageProducts")
                        .HasForeignKey("SellerPageID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("sellerPage");
                });

            modelBuilder.Entity("API.Models.UserProduct", b =>
                {
                    b.HasOne("API.Models.Product", "product")
                        .WithMany("UserProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("API.Models.User", "user")
                        .WithMany("UserProducts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("user");
                });

            modelBuilder.Entity("API.Models.Marketplace", b =>
                {
                    b.Navigation("MarketplaceSellers");
                });

            modelBuilder.Entity("API.Models.Product", b =>
                {
                    b.Navigation("SellerPageProducts");

                    b.Navigation("UserProducts");
                });

            modelBuilder.Entity("API.Models.SellerPage", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SellerPageProducts");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Navigation("MarketplaceSellers");

                    b.Navigation("UserProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
