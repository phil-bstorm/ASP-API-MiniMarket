using MiniMarket.BLL.CustomExceptions;
using MiniMarket.BLL.Services;
using MiniMarket.BLL.Services.Interfaces;
using MiniMarket.DAL.Repositories.Interfaces;
using MiniMarket.Domain.Models;
using Moq;

namespace MiniMarket.BLL.Test.Services
{
    public class ProductServiceTest
    {
        [Fact]
        public void Product_GetById_Success()
        {
            // Preparation
            Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetById(1))
                .Returns(new Product()
                {
                    Id = 1,
                    Price = 42,
                    Name = "Test",
                    Description = "TestDescr",
                    Discount = 5
                });

            // Arrange
            ProductService productService = new ProductService(mockRepo.Object);
            
            // Action
            Product result = productService.GetById(1);

            // Assert
            mockRepo.Verify(repo => repo.GetById(1), Times.Once);
            Assert.IsType<Product>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(42, result.Price);
            Assert.Equal("Test", result.Name);
            Assert.Equal("TestDescr", result.Description);
            Assert.Equal(5, result.Discount);
        }

        [Fact]
        public void Product_GetById_NotFound()
        {
            Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetById(404))
                .Returns(() => null);

            ProductService productService = new ProductService(mockRepo.Object);

            Assert.Throws<NotFoundException>(() =>
            {
                productService.GetById(404);
            });

            mockRepo.Verify(repo => repo.GetById(404), Times.Once);
        }
    }
}