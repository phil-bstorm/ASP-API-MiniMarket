using MiniMarket.Domain.CustomEnums;

namespace MiniMarket.Domain.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateOnly Birthdate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public UtilisateurRole Role { get; set; } = UtilisateurRole.Player; // Default role
    }
}
