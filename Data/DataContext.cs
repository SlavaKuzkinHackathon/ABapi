using Microsoft.EntityFrameworkCore;
using ABapi.Models;

namespace ABapi.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> opts) : base (opts) { }
        public DbSet<User> Users { get; set; }
    }
}