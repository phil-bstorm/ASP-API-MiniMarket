using MiniMarket.API.DTOs;
using MiniMarket.Domain.Models;

namespace MiniMarket.API.Mappers
{
    public static class AuthMappers
    {
        public static Utilisateur ToUtilisateur(this AuthRegisterForm form)
        {
            return new Utilisateur
            {
                Username = form.Username,
                Email = form.Email,
                Password = form.Password,
                Birthdate = form.Birthdate,
            };
        }
    }
}
