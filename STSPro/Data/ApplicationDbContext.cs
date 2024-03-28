using Microsoft.EntityFrameworkCore;
using STSPro.Model;

namespace STSPro.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options) { }
        public DbSet<Devices> devices { get; set; }
        public DbSet<ProviderModel> providers { get; set; }
        public DbSet<SimCard> simCards { get; set; }
        public DbSet<UserModel> userModels { get; set; }
        public DbSet<Admin> admins { get; set; }
    }
}
