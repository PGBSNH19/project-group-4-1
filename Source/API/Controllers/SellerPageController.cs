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
    public class SellerPageController : Controller
    {
        private readonly ISellerPageRepository _sellerPageRepository;
        private readonly IMapper _mapper;

        public SellerPageController(ISellerPageRepository sellerPageRepository, IMapper mapper)
        {
            _sellerPageRepository = sellerPageRepository;
            _mapper = mapper;
        }

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

        [HttpGet("GetSellerPageByUserID/{id}")]
                    return NotFound();
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
