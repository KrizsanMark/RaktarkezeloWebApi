using Microsoft.EntityFrameworkCore;
using RaktarkezeliWebApi.Models;

namespace RaktarkezeliWebApi.Data
{
    public class WareHouseDbContext : DbContext
    {
        public WareHouseDbContext(DbContextOptions<WareHouseDbContext> options) : base(options) { }

        public DbSet<Termekek> Products { get; set; }
        public DbSet<Beszallitok> Suppliers { get; set; }
    }
}
