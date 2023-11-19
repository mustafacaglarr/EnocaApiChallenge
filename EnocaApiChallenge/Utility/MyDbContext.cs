using EnocaApiChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EnocaApiChallenge.Utility
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
       
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<CarrierConfiguration> CarrierConfigurations { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
