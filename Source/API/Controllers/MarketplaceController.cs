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
    public class MarketplaceController : Controller
    {
        private readonly IMarketplaceRepository _marketplaceRepository;
        private readonly IMapper _mapper;

        public MarketplaceController(IMarketplaceRepository marketplaceRepository, IMapper mapper)
        {
            _marketplaceRepository = marketplaceRepository;
            _mapper = mapper;
        }

        [HttpGet("GetMarketplaces")]
        public async Task<ActionResult<MarketplaceDto[]>> GetMarketplaces()
        {
            try
            {
                var results = await _marketplaceRepository.GetMarketplaces();
                var mappedEntities = _mapper.Map<MarketplaceDto[]>(results);

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

        [HttpGet("GetMarketplace/{id}")]
        public async Task<ActionResult<MarketplaceDto>> GetMarketplaceById(int id)
        {
            try
            {
                var result = await _marketplaceRepository.GetMarketplaceById(id);
                var mappedEntity = _mapper.Map<MarketplaceDto>(result);

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

        public async Task<ActionResult<Marketplace>> PostMarketplace([FromBody] MarketplaceDto marketplace)
        {
            try
            {
                var mappedEntity = _mapper.Map<Marketplace>(marketplace);
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
