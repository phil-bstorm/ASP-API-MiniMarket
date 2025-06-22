using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniMarket.Domain.Models;

namespace MiniMarket.DAL.Database.Configurations
{
    public class UtilisateurConfig : IEntityTypeConfiguration<Utilisateur>
    {
        public void Configure(EntityTypeBuilder<Utilisateur> builder)
        {
            builder.ToTable("Utilisateurs");

            // Properties configuration
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Birthdate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<string>();

            // Constraints
            builder.HasKey(u => u.Id);
        }
    }
}
