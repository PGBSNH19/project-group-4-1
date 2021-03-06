﻿using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace API.Context
{
    public class NearbyProduceContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public NearbyProduceContext() { }
        AzureKeyvaultService _aKVService = new AzureKeyvaultService();
        public NearbyProduceContext(IConfiguration config, DbContextOptions options) : base(options)
        {
            _configuration = config;
        }
        public virtual DbSet<Marketplace> Marketplaces { get; set; }
        public virtual DbSet<MarketplaceSeller> MarketplaceSellers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SellerPage> SellerPages { get; set; }
        public virtual DbSet<SellerPageProduct> SellerPageProducts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProduct> UserProducts { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var azureDbCon = _aKVService.GetKeyVaultSecret("https://nearbyproducevault.vault.azure.net/secrets/NearByProduce-Connectionstring2/54a471f3aa5040508f39273f3ceb220c");
            var builder = new ConfigurationBuilder();
            if (string.IsNullOrEmpty(azureDbCon))
            {
                IConfigurationRoot configuration = builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.Development.json")
                    .Build();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
            else
            {
                builder.Build();
                optionsBuilder.UseSqlServer(azureDbCon);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Marketplace>().ToTable("Marketplaces");
            modelBuilder.Entity<Marketplace>().HasKey(x => x.MarketplaceID);
            modelBuilder.Entity<Marketplace>()
            .HasData(new
            {
                MarketplaceID = 1,
                Name = "Göteborgs Bakluckeloppis",
                Location = "Heden",
                StartDateTime = new DateTime(2020, 12, 26, 10, 30, 00),
                EndDateTime = new DateTime(2020, 12, 26, 16, 30, 00)
            }, new
            {
                MarketplaceID = 2,
                Name = "Majornas Eko-Marknad",
                Location = "Majorna",
                StartDateTime = new DateTime(2020, 12, 30, 09, 30, 00),
                EndDateTime = new DateTime(2020, 12, 30, 16, 30, 00)
            });


            modelBuilder.Entity<MarketplaceSeller>().ToTable("MarketplaceSellers");
            modelBuilder.Entity<MarketplaceSeller>().HasKey(hu => new { hu.MarketplaceID, hu.SellerID });
            modelBuilder.Entity<MarketplaceSeller>()
                .HasOne(m => m.Marketplace)
                .WithMany(ms => ms.MarketplaceSellers)
                .HasForeignKey(bc => bc.MarketplaceID);
            modelBuilder.Entity<MarketplaceSeller>()
                .HasOne(m => m.Seller)
                .WithMany(ms => ms.MarketplaceSellers)
                .HasForeignKey(bc => bc.SellerID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MarketplaceSeller>()
           .HasData(new
           {
               MarketplaceID = 1,
               SellerID = 1
           }, new
           {
               MarketplaceID = 1,
               SellerID = 3
           }, new
           {
               MarketplaceID = 2,
               SellerID = 5
           });


            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(k => k.ProductID);

            modelBuilder.Entity<Product>()
           .HasData(new
           {
               ProductID = 1,
               Name = "Potatis"
           }, new
           {
               ProductID = 2,
               Name = "Morot"
           }, new
           {
               ProductID = 3,
               Name = "Äpplen"
           }, new
           {
               ProductID = 4,
               Name = "Päron"
           }, new
           {
               ProductID = 5,
               Name = "Nöttfärs"
           });


            modelBuilder.Entity<UserProduct>().ToTable("UserProducts");
            modelBuilder.Entity<UserProduct>()
                .HasKey(k => new { k.UserID, k.ProductID });
            modelBuilder.Entity<UserProduct>()
                .HasOne(p => p.product)
                .WithMany(p => p.UserProducts)
                .HasForeignKey(k => k.ProductID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProduct>()
                .HasOne(m => m.user)
                .WithMany(k => k.UserProducts)
                .HasForeignKey(k => k.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProduct>()
           .HasData(new
           {
               UserID = 2,
               ProductID = 1
           }, new
           {
               UserID = 3,
               ProductID = 5
           });


            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(k => k.UserID);
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>()
           .HasData(new
           {
               UserID = 1,
               Username = "JanneBonde07",
               Password = "lösen123",
               Email = "test@test.com",
               Type = UserType.Seller
           }, new
           {
               UserID = 2,
               Username = "Bengtan555",
               Password = "lösen123",
               Email = "test1@test.com",
               Type = UserType.Buyer
           }, new
           {
               UserID = 3,
               Username = "Henrik123",
               Password = "KlDioL123!",
               Email = "test2@test.com",
               Type = UserType.Buyer
           }, new
           {
               UserID = 4,
               Username = "BondenLisa1",
               Password = "lösen123",
               Email = "test3@test.com",
               Type = UserType.Seller
           }, new
           {
               UserID = 5,
               Username = "HannesFarm",
               Password = "lösen123",
               Email = "test4@test.com",
               Type = UserType.Seller
           });


            modelBuilder.Entity<SellerPage>().ToTable("SellerPages");
            modelBuilder.Entity<SellerPage>().HasKey(k => k.SellerPageID);
            modelBuilder.Entity<SellerPage>()
             .HasData(new
             {

                 SellerPageID = 1,
                 Name = "Jannes Online-Gård",
                 SellerUserID = 1
             }, new
             {
                 SellerPageID = 2,
                 Name = "Lisas Näroldat",
                 SellerUserID = 4
             }, new
             {
                 SellerPageID = 3,
                 Name = "Hannes eko-farm",
                 SellerUserID = 4
             });


            modelBuilder.Entity<SellerPageProduct>().ToTable("SellerPageProducts");
            modelBuilder.Entity<SellerPageProduct>().HasKey(k => new { k.ProductID, k.SellerPageID });
            modelBuilder.Entity<SellerPageProduct>()
                .HasOne(s => s.sellerPage)
                .WithMany(s => s.SellerPageProducts)
                .HasForeignKey(k => k.SellerPageID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SellerPageProduct>()
                .HasOne(s => s.product)
                .WithMany(s => s.SellerPageProducts)
                .HasForeignKey(k => k.ProductID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SellerPageProduct>()
             .HasData(new
             {
                 SellerPageID = 1,
                 ProductID = 1,
                 Stock = 10,
                 Price = 2
             }, new
             {
                 SellerPageID = 2,
                 ProductID = 5,
                 Stock = 25,
                 Price = 55
             }, new
             {
                 SellerPageID = 3,
                 ProductID = 3,
                 Stock = 100,
                 Price = 10
             });
        }
    }
}
