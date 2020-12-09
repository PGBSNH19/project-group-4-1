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
        public DbSet<Message> Messages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                          .AddJsonFile("appsettings.json")
                                                          .AddJsonFile("appsettings.Development.json")
                                                          .Build();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Text).IsRequired();
            });

            modelBuilder.Entity<Message>()
            .HasData(new
            {
                Id = 1,
                Text = "Hello Farmer World"
            });
        }
    }
}
