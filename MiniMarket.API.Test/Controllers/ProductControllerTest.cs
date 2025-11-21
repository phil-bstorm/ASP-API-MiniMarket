using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MiniMarket.API.Controllers;
using MiniMarket.API.DTOs;
using MiniMarket.BLL.CustomExceptions;
using MiniMarket.BLL.Services.Interfaces;
using MiniMarket.Domain.Models;
using Moq;

namespace MiniMarket.API.Test.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public void Product_GetById_Success() {
            // Mocking
            Mock<IProductService> mockService = new Mock<IProductService>();
            mockService.Setup(service => service.GetById(1))
                        .Returns(new Product() { 
                            Id=1,
                            Price=42,
                            Name="Test",
                            Description="TestDescr",
                            Discount=5
                        });

            // Arrange
            ProductController productController = new ProductController(mockService.Object);

            // Action
            ActionResult<ProductDTO> result = productController.GetById(1);

            // Assert
            mockService.Verify(service => service.GetById(1), Times.Once);

            OkObjectResult reponse = Assert.IsType<OkObjectResult>(result.Result);
            ProductDTO responseValue = Assert.IsType<ProductDTO>(reponse.Value);
            Assert.Equal(200, reponse.StatusCode);
            Assert.Equal(1, responseValue.Id);
            Assert.Equal(42, responseValue.Price);
            Assert.Equal("Test", responseValue.Name);
            Assert.Equal("TestDescr", responseValue.Description);
            Assert.Equal(5, responseValue.Discount);
        }
        
        
        [Fact]
        public void Product_GetById_NotFound()
        {
            Mock<IProductService> mockService = new Mock<IProductService>();
            mockService.Setup(service => service.GetById(42))
                        .Throws<NotFoundException>();

            // Arrange
            ProductController productController = new ProductController(mockService.Object);

            // Action
            Assert.Throws<NotFoundException>(() => productController.GetById(42));

            // Assert
            mockService.Verify(service => service.GetById(42), Times.Once);           
        }
    }
}
