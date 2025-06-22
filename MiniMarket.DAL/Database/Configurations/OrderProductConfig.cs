using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniMarket.Domain.Models;

namespace MiniMarket.DAL.Database.Configurations
{
    public class OrderProductConfig : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrderProduct");

            // Properties configuration
            builder.Property(op => op.Quantity)
                .IsRequired();

            builder.Property(op => op.Price)
                .IsRequired();

            builder.Property(op => op.Discount)
                .IsRequired();

            // Composite key configuration
            builder.HasKey(op => new { op.OrderId, op.ProductId });

            // Relationships configuration
            // relation 1 to many with Product
            builder.HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId) // Foreign key in OrderProduct
                .OnDelete(DeleteBehavior.Cascade); // Optional: define delete behavior
        }
    }
}
