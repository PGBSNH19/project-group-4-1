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
    [Migration("20210112085309_Unique_username_and_email")]
    partial class Unique_username_and_email
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

                    b.Property<byte[]>("PictureBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("MarketplaceID");

                    b.ToTable("Marketplaces");

                    b.HasData(
                        new
                        {
                            MarketplaceID = 1,
                            EndDateTime = new DateTime(2020, 12, 26, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Location = "Heden",
                            Name = "Göteborgs Bakluckeloppis",
                            StartDateTime = new DateTime(2020, 12, 26, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            MarketplaceID = 2,
                            EndDateTime = new DateTime(2020, 12, 30, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Location = "Majorna",
                            Name = "Majornas Eko-Marknad",
                            StartDateTime = new DateTime(2020, 12, 30, 9, 30, 0, 0, DateTimeKind.Unspecified)
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

                    b.ToTable("MarketplaceSellers");

                    b.HasData(
                        new
                        {
                            MarketplaceID = 1,
                            SellerID = 1
                        },
                        new
                        {
                            MarketplaceID = 1,
                            SellerID = 3
                        },
                        new
                        {
                            MarketplaceID = 2,
                            SellerID = 5
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

                    b.Property<byte[]>("PictureBytes")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ProductID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            Name = "Potatis"
                        },
                        new
                        {
                            ProductID = 2,
                            Name = "Morot"
                        },
                        new
                        {
                            ProductID = 3,
                            Name = "Äpplen"
                        },
                        new
                        {
                            ProductID = 4,
                            Name = "Päron"
                        },
                        new
                        {
                            ProductID = 5,
                            Name = "Nöttfärs"
                        });
                });

            modelBuilder.Entity("API.Models.SellerPage", b =>
                {
                    b.Property<int>("SellerPageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR(150)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SellerUserID")
                        .HasColumnType("int");

                    b.HasKey("SellerPageID");

                    b.HasIndex("SellerUserID");

                    b.ToTable("SellerPages");

                    b.HasData(
                        new
                        {
                            SellerPageID = 1,
                            Name = "Jannes Online-Gård",
                            SellerUserID = 1
                        },
                        new
                        {
                            SellerPageID = 2,
                            Name = "Lisas Näroldat",
                            SellerUserID = 4
                        },
                        new
                        {
                            SellerPageID = 3,
                            Name = "Hannes eko-farm",
                            SellerUserID = 4
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

                    b.ToTable("SellerPageProducts");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            SellerPageID = 1,
                            Price = 2,
                            Stock = 10
                        },
                        new
                        {
                            ProductID = 5,
                            SellerPageID = 2,
                            Price = 55,
                            Stock = 25
                        },
                        new
                        {
                            ProductID = 3,
                            SellerPageID = 3,
                            Price = 10,
                            Stock = 100
                        });
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Email = "test@test.com",
                            Password = "lösen123",
                            Type = 1,
                            Username = "JanneBonde07"
                        },
                        new
                        {
                            UserID = 2,
                            Email = "test1@test.com",
                            Password = "lösen123",
                            Type = 0,
                            Username = "Bengtan555"
                        },
                        new
                        {
                            UserID = 3,
                            Email = "test2@test.com",
                            Password = "KlDioL123!",
                            Type = 0,
                            Username = "Henrik123"
                        },
                        new
                        {
                            UserID = 4,
                            Email = "test3@test.com",
                            Password = "lösen123",
                            Type = 1,
                            Username = "BondenLisa1"
                        },
                        new
                        {
                            UserID = 5,
                            Email = "test4@test.com",
                            Password = "lösen123",
                            Type = 1,
                            Username = "HannesFarm"
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

                    b.ToTable("UserProducts");

                    b.HasData(
                        new
                        {
                            UserID = 2,
                            ProductID = 1
                        },
                        new
                        {
                            UserID = 3,
                            ProductID = 5
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
