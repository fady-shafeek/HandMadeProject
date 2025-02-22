﻿using Microsoft.EntityFrameworkCore;
using HandMadeApi.Models.StoreDatabase.Favourite;

namespace HandMadeApi.Models.StoreDatabase
{
    //Install the following Packages
    //Install-Package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
    //Install-Package Microsoft.EntityFrameworkCore.SqlServer
    public partial class StoreContext : DbContext
    {
        //Connection String => 
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<ProductRate> ProductRates { get; set; }
        public DbSet<Fav> Favs { get; set; }


        //To prevent Polarized table names
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>().ToTable("Store");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<OrderHeader>().ToTable("OrderHeader");
            modelBuilder.Entity<CartHeader>().ToTable("CartHeader");
            modelBuilder.Entity<ProductRate>().ToTable("ProductRate");
            modelBuilder.Entity<Fav>().ToTable("Favourite");
        }

    }
}
