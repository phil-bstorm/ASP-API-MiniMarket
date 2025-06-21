using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniMarket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.DAL.Database.Configurations
{
    public class ProductConfig: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) {
            builder.ToTable("Products");

            /*
             public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
            */

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasKey(p => p.Id);
        }
    }
}
