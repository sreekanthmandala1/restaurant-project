using Microsoft.EntityFrameworkCore;
using RestaurantMenuAPI.Models;
using System.Collections.Generic;

namespace RestaurantMenuAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MenuItem> MenuItems { get; set; }

    }
}