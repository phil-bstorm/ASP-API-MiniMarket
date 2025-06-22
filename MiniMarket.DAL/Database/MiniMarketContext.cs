using Microsoft.EntityFrameworkCore;
using MiniMarket.DAL.Database.Configurations;
using MiniMarket.Domain.Models;

namespace MiniMarket.DAL.Database
{
    public class MiniMarketContext : DbContext
    {
        public MiniMarketContext(DbContextOptions<MiniMarketContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<UtilisateurOrder> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UtilisateurConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new UtilisateurOrderConfig());
            modelBuilder.ApplyConfiguration(new OrderProductConfig());
        }
    }
}
