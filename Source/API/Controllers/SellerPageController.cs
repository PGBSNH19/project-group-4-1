using API.Configuration;
using API.Dtos;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1.0/[controller]")]
    public class SellerPageController : Controller
    {
        private readonly ISellerPageRepository _sellerPageRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public SellerPageController(ISellerPageRepository sellerPageRepository, IMapper mapper, IProductRepository productRepository)
        {
            _sellerPageRepository = sellerPageRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        /// <summary>
        /// Gets all sellerpages
        /// </summary>
        ///  <remarks>
        /// Sample Request: 
        ///
        ///    Get /SellerPage/GetSellerPages
        ///    
        ///    {
        ///    
        ///         "SellerPageID": 1,
        ///         
        ///         "Name": "Example Farm",
        ///         
        ///         "SellerUserID": 2,
        ///         
        ///         "Description": "A nice little farm",
        ///         
        ///         "Seller": [],
        ///         
        ///         "Products": [],
        ///         
        ///         "SellerPageProducts": []
        ///         
        ///    }, 
        ///    
        ///    {
        ///    
        ///         "SellerPageID": 2,
        ///         
        ///         "Name": "Example Farm number 2",
        ///         
        ///         "SellerUserID": 5,
        ///         
        ///         "Description": "A Cozy little farm",
        ///         
        ///         "Seller": [],
        ///         
        ///         "Products": [],
        ///         
        ///         "SellerPageProducts": []
        ///         
        ///    }
        ///</remarks>

        [HttpGet("GetSellerPages")]
        public async Task<ActionResult<SellerPageDto[]>> GetSellerPages()
        {
            try
            {
                var results = await _sellerPageRepository.GetSellerPages();
                var mappedEntities = _mapper.Map<SellerPageDto[]>(results);

                if (mappedEntities.Length == 0)
                {
                    return NotFound();
                }
                return Ok(mappedEntities);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{exception.Message} ");
            }
        }

        /// <summary>
        /// Gets a SellerPage by a user id
        /// </summary>
        ///  <remarks>
        /// Sample Request: 
        ///
        ///    Get /SellerPage/GetSellerPageByUserId/1
        ///    
        ///    {
        ///    
        ///         "SellerPageID": 1,
        ///         
        ///         "Name": "Example Farm",
        ///         
        ///         "SellerUserID": 2,
        ///         
        ///         "Description": "A nice little farm",
        ///         
        ///         "Seller": [],
        ///         
        ///         "Products": [],
        ///         
        ///         "SellerPageProducts": []
        ///         
        ///    }
        ///    
        ///</remarks>
        /// <param name="id"></param>
        [HttpGet("GetSellerPageByUserID/{id}")]
        public async Task<ActionResult<SellerPageDto>> GetSellerPageByUserId(int id)
        {
            try
            {
                var result = await _sellerPageRepository.GetSellerPageByUserID(id);
                var mappedEntity = _mapper.Map<SellerPageDto>(result);
                for (int i = 0; i < result.SellerPageProducts.Count(); i++)
                {
                    var products = result.SellerPageProducts.ElementAt(i).product;
                    var productDto = await GetProductByIdInternal(products.ProductID);
                    mappedEntity.SellerPageProducts.ElementAt(i).product = productDto;
                }

                if (mappedEntity == null)
                {
                    return NotFound();
                }
                return Ok(mappedEntity);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{exception.Message} ");
            }
        }
        /// <summary>
        /// Post a new SellerPage.
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<SellerPage>> PostSellerPage(SellerPageDto sellerPage)
        {
            try
            {
                var _mappedEntity = _mapper.Map<SellerPage>(sellerPage);
                _sellerPageRepository.Add(_mappedEntity);
                if (await _sellerPageRepository.Save())
                {
                    return Created("/api/v1.0/[controller]" + sellerPage.SellerPageID, new SellerPage { SellerPageID = sellerPage.SellerPageID });
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        /// <summary>
        /// Deletes a SellerPage
        /// </summary>
        /// <param name="id"></param>


        [HttpDelete]
        public async Task<ActionResult> DeleteSellerPage(int id)
        {
            try
            {
                var sellerPage = _sellerPageRepository.GetSellerPageByUserID(id);
                if (sellerPage == null)
                {
                    return NotFound();
                }

                _sellerPageRepository.Delete(sellerPage);
                if (await _sellerPageRepository.Save())
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ProductDto> GetProductByIdInternal(int id)
        {
            try
            {
                var result = await _productRepository.GetProductById(id);
                var mappedEntity = _mapper.Map<ProductDto>(result);

                var manualMapper = new ManualMapper();
                var manualObject = manualMapper.ManualMapperPicturesReverse(result, mappedEntity);

                if (mappedEntity == null)
                {
                    return null;
                }

                return manualObject;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"{e}");
            }
        }
    }
}
