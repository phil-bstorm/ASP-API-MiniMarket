using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket.API.DTOs;
using MiniMarket.API.Mappers;
using MiniMarket.BLL.Services.Interfaces;
using MiniMarket.Domain.CustomEnums;
using MiniMarket.Domain.Models;

namespace MiniMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<ProductListDTO>> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 20)
        {
            List<Product> products = _productService.GetAll(offset, limit).ToList();
            List<ProductListDTO> productDTOs = products.Select(p => p.ToProductListDTO()).ToList();
            return Ok(productDTOs);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDTO> GetById([FromRoute] int id)
        {
            Product product = _productService.GetById(id);
            return Ok(product.ToProductDTO());
        }

        [HttpPost]
        [Authorize(Roles = nameof(UtilisateurRole.Admin))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDTO> Create([FromBody] ProductCreateDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = dto.ToProduct();
            Product createdProduct = _productService.Create(product);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct.ToProductDTO());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = nameof(UtilisateurRole.Admin))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDTO> Update([FromRoute] int id, [FromBody] ProductCreateDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product product = dto.ToProduct();
            product.Id = id; // Ensure the ID is set for the update
            Product updatedProduct = _productService.Update(product);

            return Ok(updatedProduct.ToProductDTO());
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = nameof(UtilisateurRole.Admin))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] int id)
        {
            _productService.Delete(id);
            return NoContent();
        }
    }
}
