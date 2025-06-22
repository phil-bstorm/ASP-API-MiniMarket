using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniMarket.Domain.Models;

namespace MiniMarket.DAL.Database.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            // Properties configuration
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>(); // Assuming OrderStatus is an enum

            // Relationships configuration
            builder.HasOne(o => o.Owner)
                .WithMany(u => u.Orders) // Assuming Utilisateur has a collection of Orders
                .HasForeignKey("OwnerId") // Assuming OwnerId is the foreign key in Order
                .IsRequired();

            builder.HasMany(o => o.Products)
                .WithOne(op => op.Order) // Assuming OrderProduct has a reference to Order
                .HasForeignKey(op => op.OrderId) // Foreign key in OrderProduct
                .OnDelete(DeleteBehavior.Cascade); // Optional: define delete behavior
        }
    }
}
