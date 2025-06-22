using MiniMarket.Domain.Models;

namespace MiniMarket.DAL.Repositories.Interfaces
{
    public interface IUtilisateurRepository : IRepository<int, Utilisateur>
    {
        public Utilisateur? GetByUsername(string username);
        public Utilisateur? GetByEmail(string email);
    }
}
