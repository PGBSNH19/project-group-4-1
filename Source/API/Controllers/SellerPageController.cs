using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1.0/[controller]")]
    public class SellerPageController : Controller
    {
        private readonly ISellerPageRepository _sellerPageRepository;

        public SellerPageController(ISellerPageRepository sellerPageRepository)
        {
            _sellerPageRepository = sellerPageRepository;
        }

        /// <summary>
        /// Gets a seller based on a userID 
        /// </summary>
        /// 

        [HttpGet("GetSellerPageByUserID/{id}")]
        public async Task<ActionResult<SellerPage>> GetSellerPageByUserId(int id)
        {
            try
            {
                var result = await _sellerPageRepository.GetSellerPageByUserID(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{exception.Message} ");
            }
        }

        /// <summary>
        /// Gets all Sellers
        /// </summary>
        /// 

        [HttpGet("GetSellerPages")]
        public async Task<ActionResult<SellerPage[]>> GetSellerPages()
        {
            try
            {
                var results = await _sellerPageRepository.GetSellerPages();
                if (results.Count == 0)
                {
                    return NotFound();
                }
                return Ok(results);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{exception.Message} ");
            }
        }

        /// <summary>
        /// Posts a user
        /// </summary>
        /// 

        [HttpPost]
        public async Task<ActionResult<SellerPage>> PostSellerPage(SellerPage sellerPage)
        {
            try
            {
                _sellerPageRepository.Add(sellerPage);
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
        /// Deletes a user
        /// </summary>
        ///

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
    }
}
