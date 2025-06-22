using MiniMarket.Domain.CustomEnums;
using System.ComponentModel.DataAnnotations;

namespace MiniMarket.API.DTOs
{
    public class UtilisateurListDTO
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required DateOnly Birthdate { get; set; }
        public required UtilisateurRole Role { get; set; }
    }

    public class UtilisateurDTO
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required DateOnly Birthdate { get; set; }
        public required UtilisateurRole Role { get; set; }

        public List<OrderListDTO> Orders = [];
    }

    public class UtilisateurUpdateForm
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required DateOnly Birthdate { get; set; }
    }

    public class UpdatePasswordForm
    {
        [Required]
        [MinLength(3)]
        public required string Password { get; set; }
    }
}
