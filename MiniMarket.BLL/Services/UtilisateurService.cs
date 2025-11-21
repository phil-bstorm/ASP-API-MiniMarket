using Isopoh.Cryptography.Argon2;
using MiniMarket.BLL.CustomExceptions;
using MiniMarket.BLL.Services.Interfaces;
using MiniMarket.DAL.Repositories.Interfaces;
using MiniMarket.Domain.Models;

namespace MiniMarket.BLL.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _utilisateurRepository;

        public UtilisateurService(IUtilisateurRepository utilisateurRepository)
        {
            _utilisateurRepository = utilisateurRepository;
        }

        public Utilisateur Create(Utilisateur entity)
        {
            Utilisateur? existingUser = _utilisateurRepository.GetByEmail(entity.Email);
            if (existingUser is not null)
            {
                throw new EmailAlreadyUsedException();
            }

            existingUser = _utilisateurRepository.GetByUsername(entity.Username);
            if (existingUser is not null)
            {
                throw new UsernameAlreadyUsedException();
            }

            // hash the password
            entity.Password = Argon2.Hash(entity.Password);

            return _utilisateurRepository.Create(entity);
        }

        public bool Delete(int id)
        {
            Utilisateur? utilisateur = _utilisateurRepository.GetById(id);
            if (utilisateur == null)
            {
                throw new NotFoundException();
            }
            return _utilisateurRepository.Delete(utilisateur);
        }

        public IEnumerable<Utilisateur> GetAll(int offset, int limit = 20)
        {
            return _utilisateurRepository.GetAll(offset, limit);
        }

        public Utilisateur GetByEmail(string email)
        {
            Utilisateur? utilisateur = _utilisateurRepository.GetByEmail(email);
            if (utilisateur == null)
            {
                throw new NotFoundException();
            }
            return utilisateur;
        }

        public Utilisateur GetById(int key)
        {
            Utilisateur? utilisateur = _utilisateurRepository.GetById(key);
            if (utilisateur == null)
            {
                throw new NotFoundException();
            }
            return utilisateur;
        }

        public IEnumerable<Utilisateur> GetByIds(List<int> keys)
        {
            return _utilisateurRepository.GetByIds(keys);
        }

        public Utilisateur GetByUsername(string username)
        {
            Utilisateur? utilisateur = _utilisateurRepository.GetByUsername(username);
            if (utilisateur == null)
            {
                throw new NotFoundException();
            }
            return utilisateur;
        }

        public Utilisateur Login(string username, string password)
        {
            Utilisateur? utilisateur = _utilisateurRepository.GetByUsername(username);

            if (utilisateur is null || !Argon2.Verify(utilisateur.Password, password))
            {
                throw new InvalidLoginException();
            }

            return utilisateur;
        }

        public Utilisateur Update(Utilisateur entity)
        {
            Utilisateur? utilisateur = _utilisateurRepository.GetById(entity.Id);
            if (utilisateur == null)
            {
                throw new NotFoundException();
            }

            // place value in the objet from the repository
            if (utilisateur.Email != entity.Email)
            {
                // the user is trying to change his email
                Utilisateur? existingUser = _utilisateurRepository.GetByEmail(entity.Email);
                if (existingUser is not null)
                {
                    throw new EmailAlreadyUsedException();
                }
                utilisateur.Email = entity.Email;
            }

            if (utilisateur.Username != entity.Username)
            {
                // the user is trying to change his Username
                Utilisateur? existingUser = _utilisateurRepository.GetByUsername(entity.Username);
                if (existingUser is not null)
                {
                    throw new UsernameAlreadyUsedException();
                }
                utilisateur.Username = entity.Username;
            }

            utilisateur.Birthdate = entity.Birthdate;

            return _utilisateurRepository.Update(utilisateur);
        }

        public bool UpdatePassword(int id, string newPassowrd)
        {
            Utilisateur? utilisateur = _utilisateurRepository.GetById(id);
            if (utilisateur == null)
            {
                throw new NotFoundException();
            }

            utilisateur.Password = Argon2.Hash(newPassowrd);

            _utilisateurRepository.Update(utilisateur);
            return true;
        }
    }
}
