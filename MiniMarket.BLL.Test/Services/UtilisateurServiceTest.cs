using MiniMarket.BLL.CustomExceptions;
using MiniMarket.BLL.Services;
using MiniMarket.DAL.Repositories.Interfaces;
using MiniMarket.Domain.Models;
using Moq;

namespace MiniMarket.BLL.Test.Services
{
    public class UtilisateurServiceTest
    {
        private readonly Utilisateur _UserTest = new Utilisateur()
        {
            Id = 1,
            Email = "email@email.com",
            Password = "Test1234=",
            Username = "Test"
        };

        [Fact]
        public void Utilisateur_Create_Success()
        {
            Mock<IUtilisateurRepository> mockRepo = new Mock<IUtilisateurRepository>();
            mockRepo.Setup(repo => repo.GetByEmail(_UserTest.Email))
                    .Returns(() => null);
            mockRepo.Setup(repo => repo.GetByUsername(_UserTest.Username))
                .Returns(() => null);
            mockRepo.Setup(repo => repo.Create(_UserTest))
                .Returns(_UserTest);

            UtilisateurService utilisateurService = new UtilisateurService(mockRepo.Object);

            Utilisateur result = utilisateurService.Create(_UserTest);

            mockRepo.Verify(repo => repo.GetByEmail(_UserTest.Email), Times.Once);
            mockRepo.Verify(repo => repo.GetByUsername(_UserTest.Username), Times.Once);
            mockRepo.Verify(repo => repo.Create(_UserTest), Times.Once);
        }

        [Fact]
        public void Utilisateur_Create_Exception_EmailAlreadyUsed()
        {
            Mock<IUtilisateurRepository> mockRepo = new Mock<IUtilisateurRepository>();
            mockRepo.Setup(repo => repo.GetByEmail(_UserTest.Email))
                    .Returns(_UserTest);
            mockRepo.Setup(repo => repo.GetByUsername(_UserTest.Username))
               .Returns(() => null);
            mockRepo.Setup(repo => repo.Create(_UserTest))
                .Returns(_UserTest);
            UtilisateurService utilisateurService = new UtilisateurService(mockRepo.Object);
            Assert.Throws<EmailAlreadyUsedException>(() => utilisateurService.Create(_UserTest));
            
            mockRepo.Verify(repo => repo.GetByEmail(_UserTest.Email), Times.Once);
            mockRepo.Verify(repo => repo.GetByUsername(_UserTest.Username), Times.Never);
            mockRepo.Verify(repo => repo.Create(_UserTest), Times.Never);
        }

        [Fact]
        public void Utilisateur_Create_Exception_UsernameAlreadyUsed()
        {
            Mock<IUtilisateurRepository> mockRepo = new Mock<IUtilisateurRepository>();
            mockRepo.Setup(repo => repo.GetByEmail(_UserTest.Email))
                    .Returns(() => null);
            mockRepo.Setup(repo => repo.GetByUsername(_UserTest.Username))
               .Returns(_UserTest);
            mockRepo.Setup(repo => repo.Create(_UserTest))
                .Returns(_UserTest);
            UtilisateurService utilisateurService = new UtilisateurService(mockRepo.Object);
            Assert.Throws<UsernameAlreadyUsedException>(() => utilisateurService.Create(_UserTest));

            mockRepo.Verify(repo => repo.GetByEmail(_UserTest.Email), Times.Once);
            mockRepo.Verify(repo => repo.GetByUsername(_UserTest.Username), Times.Once);
            mockRepo.Verify(repo => repo.Create(_UserTest), Times.Never);
        }
    }
}
