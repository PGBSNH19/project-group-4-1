﻿using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class MarketplaceController : Controller
    {
        private readonly IMarketplaceRepository _marketplaceRepository;

        public MarketplaceController(IMarketplaceRepository marketplaceRepository)
        {
            _marketplaceRepository = marketplaceRepository;
        }

        /// <summary>
        /// Gets all Marketplaces
        /// </summary>
        /// 
        [HttpGet("GetMarketplaces")]
        public async Task<ActionResult<Marketplace[]>> GetMarketplaces()
        {
            try
            {
                var results = await _marketplaceRepository.GetMarketplaces();

                if (results.Count == 0)
                {
                    return NotFound();
                }

                return Ok(results);
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
        public async Task<ActionResult<Marketplace>> GetMarketplaceById(int id)
        {
            try
            {
                var result = await _marketplaceRepository.GetMarketplaceById(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
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
        public async Task<ActionResult<Marketplace>> PostMarketplace(Marketplace marketplace)
        {
            try
            {
                _marketplaceRepository.Add(marketplace);
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
