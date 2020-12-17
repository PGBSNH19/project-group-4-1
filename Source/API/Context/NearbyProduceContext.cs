using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class NearbyProduceContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public NearbyProduceContext() { }
        public NearbyProduceContext(IConfiguration config, DbContextOptions options) : base(options)
        {
            _configuration = config;
        }
        public DbSet<Marketplace> Marketplaces { get; set; }
        public DbSet<MarketplaceSeller> MarketplaceSeller { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SellerPage> SellerPage { get; set; }
        public DbSet<SellerPageProduct> SellerPageProducts { get;  set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                          .AddJsonFile("appsettings.Development.json")
                                                          .Build();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var sellerToAdd = new User {

                UserID = 2,
                Username = "Bengtan555",
                Password = "lösen123",
                Type = UserType.Buyer
            };
            modelBuilder.Entity<Marketplace>().ToTable("Marketplace");
            modelBuilder.Entity<Marketplace>().HasKey(x  =>  x.MarketplaceID);
            modelBuilder.Entity<Marketplace>()
            .HasData(new
            {
                MarketplaceID = 1,
                Name = "Göteborgs Bakluckeloppis",
                Location = "Heden",
                StartDateTime = new DateTime(2020,12,26,10,30,00),
                EndDateTime = new DateTime(2020, 12,26,16,30,00)
            }
            );


            modelBuilder.Entity<MarketplaceSeller>().ToTable("MarketplaceSeller");
            modelBuilder.Entity<MarketplaceSeller>().HasKey(hu => new { hu.MarketplaceID, hu.SellerID});
            modelBuilder.Entity<MarketplaceSeller>()
                .HasOne(m => m.Marketplace)
                .WithMany(ms => ms.MarketplaceSellers)
                .HasForeignKey(bc => bc.MarketplaceID);
            modelBuilder.Entity<MarketplaceSeller>()
                .HasOne(m => m.Seller)
                .WithMany(ms => ms.MarketplaceSellers)
                .HasForeignKey(bc  => bc.SellerID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MarketplaceSeller>()
           .HasData(new
           {
               MarketplaceID = 1,
               SellerID = 1
           }
           );


            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(k  => k.ProductID);

            modelBuilder.Entity<Product>()
           .HasData(new
           {
               ProductID = 1,
               Name = "Potatis"
           }
           );


            modelBuilder.Entity<UserProduct>().ToTable("UserProduct");
            modelBuilder.Entity<UserProduct>()
                .HasKey(k => new {  k.UserID, k.ProductID });
            modelBuilder.Entity<UserProduct>()
                .HasOne(p  =>  p.product)
                .WithMany(p  =>  p.UserProducts)
                .HasForeignKey(k  =>  k.ProductID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProduct>()
                .HasOne(m => m.user)
                .WithMany(k => k.UserProducts)
                .HasForeignKey(k  =>  k.UserID)
                .OnDelete(DeleteBehavior.NoAction);




            modelBuilder.Entity<UserProduct>()
           .HasData(new
           {
               UserID = 2, 
               ProductID = 1
           }
           );


            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey(k => k.UserID);
            modelBuilder.Entity<User>()
           .HasData(new
           {
               UserID = 1,
               Username = "JanneBonde07",
               Password = "lösen123",
               Type = UserType.Seller
           }, sellerToAdd

           );


            modelBuilder.Entity<SellerPage>().ToTable("SellerPage");
            modelBuilder.Entity<SellerPage>().HasKey(k => k.SellerPageID);
            modelBuilder.Entity<SellerPage>()
             .HasData(new
             {

                 SellerPageID = 1,
                 Name = "Arnes Online-Gård",
                 SellerUserID = 2
             });


            modelBuilder.Entity<SellerPageProduct>().ToTable("SellerPageProduct");
            modelBuilder.Entity<SellerPageProduct>().HasKey(k  => new { k.ProductID, k.SellerPageID });
            modelBuilder.Entity<SellerPageProduct>()
                .HasOne(s => s.sellerPage)
                .WithMany(s => s.SellerPageProducts)
                .HasForeignKey(k=>k.SellerPageID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SellerPageProduct>()
                .HasOne(s => s.product)
                .WithMany(s => s.SellerPageProducts)
                .HasForeignKey(k  =>  k.ProductID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SellerPageProduct>()
             .HasData(new
             {
                 SellerPageID = 1,
                 ProductID = 1, 
                 Stock = 10,
                 Price = 2
             });
        }
    }
}
