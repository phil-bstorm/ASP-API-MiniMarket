using Microsoft.EntityFrameworkCore;
using MiniMarket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
}
