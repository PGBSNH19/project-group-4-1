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
        /// 
        [HttpGet("GetMarketplaces")]
        public async Task<ActionResult<MarketplaceDto[]>> GetMarketplaces()
        {
            try
            {
                var results = await _marketplaceRepository.GetMarketplaces();
                var mappedEntities = _mapper.Map<MarketplaceDto[]>(results);

                var manualMapper = new ManualMapper();
                var manualObjects = new List<MarketplaceDto>();
                for (int i = 0; i < results.Count; i++)
                {
                    var manualObject =
                        manualMapper.ManualMapperMarketplacePicturesReverse(results.ElementAt(i), mappedEntities[i]);
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
        /// Gets a Marketplace based on a id
        /// </summary>
        /// 

        [HttpGet("GetMarketplace/{id}")]
        public async Task<ActionResult<MarketplaceDto>> GetMarketplaceById(int id)
        {
            try
            {
                var result = await _marketplaceRepository.GetMarketplaceById(id);
                var mappedEntity = _mapper.Map<MarketplaceDto>(result);

                var manualMapper = new ManualMapper();
                var manualObject = manualMapper.ManualMapperMarketplacePicturesReverse(result, mappedEntity);

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
        /// Post a new Marketplaces
        /// </summary>
        ///
        [HttpPost]
        public async Task<ActionResult<Marketplace>> PostMarketplace([FromForm] MarketplaceDto marketplace)
        {
            try
            {
                var mappedEntity = _mapper.Map<Marketplace>(marketplace);
                if (marketplace.Picture != null)
                {
                    var manualMapper = new ManualMapper();
                    var manualObject = manualMapper.ManualMapperMarketplacePictures(mappedEntity, marketplace);
                    _marketplaceRepository.Add(manualObject);
                }
                _marketplaceRepository.Add(mappedEntity);
                if (await _marketplaceRepository.Save())
                {
                    return Created("/api/v1.0/[controller]/" + marketplace.MarketplaceID, new Marketplace { MarketplaceID = marketplace.MarketplaceID });
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        [HttpPut("{marketplaceId}")]
        public async Task<ActionResult<Marketplace>> PutMarketplace(int marketplaceId, [FromForm] MarketplaceDto marketplace)
        {
            try
            {
                var oldMarketplace = await _marketplaceRepository.GetMarketplaceById(marketplaceId);
                if (oldMarketplace == null)
                {
                    return NotFound($"Cant't find any marketplaces with id: {marketplaceId}");
                }

                var newMarketplace = _mapper.Map(marketplace, oldMarketplace);
                var manualMapper = new ManualMapper();
                var manualObject = manualMapper.ManualMapperMarketplacePictures(newMarketplace, marketplace);
                _marketplaceRepository.Update(manualObject);
                if (await _marketplaceRepository.Save())
                {
                    return Ok(manualObject);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
            return BadRequest();
        }
        /// <summary>
        /// Deletes a  Marketplace based on its id
        /// </summary>
        ///
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMarketplace(int id)
        {
            try
            {
                var marketplace = await _marketplaceRepository.GetMarketplaceById(id);

                if (marketplace == null)
                {
                    return NotFound();
                }

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
