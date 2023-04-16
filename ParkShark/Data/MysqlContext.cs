using Microsoft.EntityFrameworkCore;
using ParkShark.Models;
using ParkShark.Models.ViewModels;

namespace ParkShark.Data
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Parking> Parking { get; set; }
        public DbSet<TransportationType> TransportationTypes { get; set; }
        public DbSet<DetailParking> DetailParking { get; set; }
    }
}
