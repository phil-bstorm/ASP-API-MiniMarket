using MiniMarket.API.DTOs;
using MiniMarket.Domain.Models;

namespace MiniMarket.API.Mappers
{
    public static class UtilisateurMappers
    {
        public static UtilisateurListDTO ToUtilisateurListDTO(this Utilisateur u)
        {
            return new UtilisateurListDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Birthdate = u.Birthdate,
                Role = u.Role
            };
        }

        public static UtilisateurDTO ToUtilisateurDTO(this Utilisateur u)
        {
            return new UtilisateurDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Birthdate = u.Birthdate,
                Role = u.Role,
                Orders = u.Orders.Select(o => o.ToOrderListDTO()).ToList()
            };
        }

        public static Utilisateur ToUtilisateur(this UtilisateurUpdateForm dto)
        {
            return new Utilisateur
            {
                Email = dto.Email,
                Username = dto.Username,
                Birthdate = dto.Birthdate,
                Password = string.Empty // Password should not be set here, it should be handled separately
            };
        }
    }
}
