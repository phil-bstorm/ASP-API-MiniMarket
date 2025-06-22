using Microsoft.EntityFrameworkCore;
using MiniMarket.DAL.Database;
using MiniMarket.DAL.Repositories.Interfaces;
using MiniMarket.Domain.Models;

namespace MiniMarket.DAL.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly MiniMarketContext _context;

        public UtilisateurRepository(MiniMarketContext context)
        {
            _context = context;
        }

        public Utilisateur Create(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            _context.SaveChanges();
            return utilisateur;
        }

        public bool Delete(Utilisateur entity)
        {
            bool isDeleted = _context.Utilisateurs.Remove(entity).State == EntityState.Deleted;
            _context.SaveChanges();
            return isDeleted;
        }

        public IEnumerable<Utilisateur> GetAll(int offset, int limit = 20)
        {
            return _context.Utilisateurs
                .Skip(offset)
                .Take(limit);
        }

        public Utilisateur? GetByEmail(string email)
        {
            return _context.Utilisateurs
                .FirstOrDefault(u => u.Email == email);
        }

        public Utilisateur? GetById(int key)
        {
            return _context.Utilisateurs
                .Include(u => u.Orders)
                .ThenInclude(o => o.Products)
                .FirstOrDefault(u => u.Id == key);
        }

        public IEnumerable<Utilisateur> GetByIds(List<int> keys)
        {
            return _context.Utilisateurs
                .Where(u => keys.Contains(u.Id))
                .Include(u => u.Orders)
                .ThenInclude(o => o.Products);
        }

        public Utilisateur? GetByUsername(string username)
        {
            return _context.Utilisateurs.FirstOrDefault(u => u.Username == username);
        }

        public Utilisateur Update(Utilisateur entity)
        {
            //_context.Utilisateurs.Update(entity); pas nécessaire car le "tracking" est activé
            _context.SaveChanges();
            return entity;
        }
    }
}
