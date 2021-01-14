
using API.Configuration;
using API.Dtos;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        /// Gets all products
        /// </summary>

        [HttpGet("GetProducts")]
        public async Task<ActionResult<ProductDto[]>> GetProducts()
        {
            var results = await _productRepository.GetProducts();

            if (results.Count == 0)
                return NoContent();

            try
            {
                var mappedEntities = _mapper.Map<ProductDto[]>(results);
                var manualMapper = new ManualMapper();
                var manualObjects = new List<ProductDto>();
                for (int i = 0; i < results.Count; i++)
                {
                    var manualObject = manualMapper.ManualMapperPicturesReverse(results.ElementAt(i), mappedEntities[i]);
                    manualObjects.Add(manualObject);
                }

                return Ok(manualObjects);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {exception.Message}");
            }
        }

        /// <summary>
        /// Gets a product by its id 
        /// </summary>
        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var result = await _productRepository.GetProductById(id);
            if (result == null)
                return NoContent();
            try
            {
                var mappedEntity = _mapper.Map<ProductDto>(result);

                var manualMapper = new ManualMapper();
                var manualObject = manualMapper.ManualMapperPicturesReverse(result, mappedEntity);

                return Ok(manualObject);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }
        /// <summary>
        /// Posts a new product
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                if (productDto.Image != null)
                {
                    var manualMapper = new ManualMapper();
                    product = manualMapper.ManualMapperPictures(product, productDto);
                }
                _productRepository.Add(product);
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
        /// Puts a Product.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, [FromBody] ProductPutDto productDto)
        {
            var oldProduct = await _productRepository.GetProductById(id);
            if (oldProduct == null)
                return NotFound($"Can't find any product with id: {id}");

            try
            {
                var newProduct = _mapper.Map(productDto, oldProduct);
                var manualMapper = new ManualMapper();
                var manualObj = manualMapper.ManualMapperPutPictures(newProduct, productDto);
                _productRepository.Update(manualObj);
                if (await _productRepository.Save())
                    return Ok(manualObj);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);

            if (product == null)
                return NotFound();

            try
            {
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
