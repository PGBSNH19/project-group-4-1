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
    public class MarketplaceController : Controller
    {
        private readonly IMarketplaceRepository _marketplaceRepository;
        private readonly IMapper _mapper;

        public MarketplaceController(IMarketplaceRepository marketplaceRepository, IMapper mapper)
        {
            _marketplaceRepository = marketplaceRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets all Marketplaces
        /// </summary>
        [HttpGet("GetMarketplaces")]
        public async Task<ActionResult<MarketplaceDto[]>> GetMarketplaces()
        {
            var results = await _marketplaceRepository.GetMarketplaces();
            if (results.Count == 0)
                return NoContent();

            var mappedEntities = _mapper.Map<MarketplaceDto[]>(results);
            var manualMapper = new ManualMapper();
            var manualObjects = new List<MarketplaceDto>();

            for (int i = 0; i < results.Count; i++)
            {
                var manualObject =
                    manualMapper.ManualMapperMarketplacePicturesReverse(results.ElementAt(i), mappedEntities[i]);
                manualObjects.Add(manualObject);
            }

            return Ok(manualObjects);
        }
        /// <summary>
        /// Gets a Marketplace by its id
        /// </summary>
        [HttpGet("GetMarketplace/{id}")]
        public async Task<ActionResult<MarketplaceDto>> GetMarketplaceById(int id)
        {
            var result = await _marketplaceRepository.GetMarketplaceById(id);

            if (result == null)
                return NoContent();

            var mappedEntity = _mapper.Map<MarketplaceDto>(result);

            var manualMapper = new ManualMapper();
            var manualObject = manualMapper.ManualMapperMarketplacePicturesReverse(result, mappedEntity);

            if (manualObject == null)
                return BadRequest();

            return Ok(manualObject);
        }
        /// <summary>
        /// post a new Marketplaces
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Marketplace>> PostMarketplace([FromForm] MarketplaceDto marketplace)
        {            
            try
            {
                var mappedEntity = _mapper.Map<Marketplace>(marketplace);

                if (!String.IsNullOrEmpty(marketplace.Image))
                {
                    var manualMapper = new ManualMapper();
                    mappedEntity = manualMapper.ManualMapperMarketplacePictures(mappedEntity, marketplace);
                }

                _marketplaceRepository.Add(mappedEntity);

                if (await _marketplaceRepository.Save())
                    return Created("/api/v1.0/[controller]/" + marketplace.MarketplaceID, new Marketplace { MarketplaceID = marketplace.MarketplaceID });

                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }
        /// <summary>
        /// Puts a  Marketplaces by its id
        /// </summary>
        [HttpPut("{marketplaceId}")]
        public async Task<ActionResult<Marketplace>> PutMarketplace(int marketplaceId, [FromForm] MarketplaceDto marketplace)
        {
            var currentMarketplace = await _marketplaceRepository.GetMarketplaceById(marketplaceId);

            if (currentMarketplace == null)
                return BadRequest($"Cant't find any marketplaces with id: {marketplaceId}");


            try
            {
                var updatedMarketplace = _mapper.Map(marketplace, currentMarketplace);
                
                if (!String.IsNullOrEmpty(marketplace.Image))
                {
                    var manualMapper = new ManualMapper();
                    updatedMarketplace = manualMapper.ManualMapperMarketplacePictures(updatedMarketplace, marketplace);
                }
                
                _marketplaceRepository.Update(updatedMarketplace);

                if (await _marketplaceRepository.Save())
                    return Ok(updatedMarketplace);

                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }
        /// <summary>
        /// Deletes a  Marketplace based on its id
        /// </summary>
        /// <remarks>      
        ///<param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMarketplace(int id)
        {

            var marketplace = await _marketplaceRepository.GetMarketplaceById(id);
            if (marketplace == null)
                return NotFound();
            
            try
            {
                _marketplaceRepository.Delete(marketplace);

                if (await _marketplaceRepository.Save())
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
