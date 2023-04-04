using Microsoft.EntityFrameworkCore;
using ParkShark.Models;

namespace ParkShark.Data
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Parking> Parking { get; set; }
        public DbSet<TransportationType> TransportationTypes { get; set; }
    }
}
