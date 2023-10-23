using CastGroup.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CastGroup.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
    }
}
