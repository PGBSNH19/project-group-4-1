using API.Configuration;
using API.Dtos;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Gets all Products
        /// </summary>
        /// /// <remarks>
        /// Sample Request: 
        ///
        ///    Get /Product/GetProducts
        ///    
        ///    {
        ///    
        ///         "ProductID": 1,
        ///         
        ///         "Name": "Potato",
        ///         
        ///         "UserProducts": [],
        ///         
        ///         "SellerPageProducts": []
        ///         
        ///    },
        ///    
        ///     {
        ///     
        ///         "ProductID": 2,
        ///         
        ///         "Name": "Apple",
        ///         
        ///         "UserProducts": [],
        ///         
        ///         "SellerPageProducts": []
        ///         
        ///    }
        ///
        ///</remarks>
        /// 
        [HttpGet("GetProducts")]
        public async Task<ActionResult<ProductDto[]>> GetProducts()
        {
            try
            {
                var results = await _productRepository.GetProducts();
                var mappedEntities = _mapper.Map<ProductDto[]>(results);

                var manualMapper = new ManualMapper();
                var manualObjects = new List<ProductDto>();
                for (int i = 0; i < results.Count; i++)
                {
                    var manualObject = manualMapper.ManualMapperPicturesReverse(results.ElementAt(i), mappedEntities[i]);
                    manualObjects.Add(manualObject);
                }

                if (mappedEntities.Length == 0)
                {
                    return NotFound();
                }

                return Ok(manualObjects);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {exception.Message}");
            }
        }

        /// <summary>
        /// Gets a product with the specified id
        /// </summary>
        /// <remarks>
        /// Sample Request: 
        ///
        ///    Get /Product/GetProduct/1
        ///    
        ///    {
        ///    
        ///         "ProductID": 1,
        ///         
        ///         "Name": "Potato",
        ///         
        ///         "UserProducts": [],
        ///         
        ///         "SellerPageProducts": []
        ///         
        ///    }
        ///</remarks>
        /// <param name="id"></param>
        /// 
        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            try
            {
                var result = await _productRepository.GetProductById(id);
                var mappedEntity = _mapper.Map<ProductDto>(result);

                var manualMapper = new ManualMapper();
                var manualObject = manualMapper.ManualMapperPicturesReverse(result, mappedEntity);

                if (mappedEntity == null)
                {
                    return NotFound();
                }

                return Ok(manualObject);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <remarks>
        /// Sample Request: 
        ///
        ///    Post /Product
        ///    
        ///    {
        ///    
        ///         "ProductID": 1,
        ///         
        ///         "Name": "Potato",
        ///         
        ///         "UserProducts": [],
        ///         
        ///         "SellerPageProducts": []
        ///         
        ///    }
        ///</remarks>
        /// <param name="product"></param>
        /// 
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                if (productDto.Image != null)
                {
                    var manualMapper = new ManualMapper();
                    var manualObj = manualMapper.ManualMapperPictures(product, productDto);
                    _productRepository.Add(manualObj);
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
            try
            {
                var oldProduct = await _productRepository.GetProductById(id);
                if (oldProduct == null)
                    return NotFound($"Can't find any product with id: {id}");
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
