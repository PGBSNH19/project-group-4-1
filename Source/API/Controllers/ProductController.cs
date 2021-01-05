using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<ProductDto[]>> GetProducts()
        {
            try
            {
                var results = await _productRepository.GetProducts();
                var mappedEntities = _mapper.Map<ProductDto[]>(results);

                if (mappedEntities.Length == 0)
                {
                    return NotFound();
                }

                return Ok(mappedEntities);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {exception.Message}");
            }
        }


        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            try
            {
                var result = await _productRepository.GetProductById(id);
                var mappedEntity = _mapper.Map<ProductDto>(result);

                if (mappedEntity == null)
                {
                    return NotFound();
                }

                return Ok(mappedEntity);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto product)
        {
            try
            {
                var _mappedEntity = _mapper.Map<Product>(product);
                _productRepository.Add(_mappedEntity);
                if (await _productRepository.Save())
                {
                    return Created("/api/v1.0/[controller]/" + product.ProductID, new Product { ProductID = product.ProductID });
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }

                _productRepository.Delete(product);

                if (await _productRepository.Save())
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}
