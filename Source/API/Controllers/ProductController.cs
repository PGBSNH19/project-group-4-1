using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using API.Configuration;
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

        /// <summary>
        /// Gets all Products
        /// </summary>
        /// 
        [HttpGet("GetProducts")]
        public async Task<ActionResult<ProductDto[]>> GetProducts()
        {
            try
            {
                var results = await _productRepository.GetProducts();
                var mappedEntities = _mapper.Map<ProductDto[]>(results);

                var manualMapper = new ManualMapper();
                manualMapper.ManualMapperPicturesReverse(results, mappedEntities);

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

        /// <summary>
        /// Gets a product with the specified id
        /// </summary>
        /// 
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

        /// <summary>
        /// Posts a new products
        /// </summary>
        /// 
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                var manualMapper = new ManualMapper();
                var manualObj = manualMapper.ManualMapperPictures(product, productDto);
                _productRepository.Add(manualObj);
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

        /// <summary>
        /// Deletes a product based on its id 
        /// </summary>
        /// 

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
